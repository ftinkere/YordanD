import { defineEventHandler, readRawBody } from 'h3'

export default defineEventHandler(async (event) => {
    const runtimeConfig = useRuntimeConfig()
    const backendBase = (runtimeConfig.public.apiBase || 'http://localhost:5045') as string

    // Собираем путь, который надо проксировать
    const path = event.context.params?.path
        ? Array.isArray(event.context.params.path)
            ? event.context.params.path.join('/')
            : event.context.params.path
        : ''
    const query = event.req.url?.split('?')[1]
    const url = `${backendBase}/api/v1/${path}${query ? `?${query}` : ''}`

    // Собираем заголовки (пробрасываем куки, убираем host)
    const incomingHeaders = event.node.req.headers
    const headers: Record<string, string> = {}
    for (const [k, v] of Object.entries(incomingHeaders)) {
        // Обрезаем host/h2-specific
        if (!['host', 'connection', 'content-length'].includes(k.toLowerCase()) && typeof v === 'string') {
            headers[k] = v
        }
    }

    // Подготавливаем тело для всех методов кроме GET/HEAD
    let body: Buffer | undefined
    if (!['GET', 'HEAD'].includes(event.node.req.method || 'GET')) {
        const rawBody = await readRawBody(event)
        body = rawBody ? Buffer.from(rawBody) : undefined
    }

    // Запрос к backend
    const response = await fetch(url, {
        method: event.node.req.method,
        headers,
        body,
        redirect: 'manual', // не следуем автоматом за редиректами
        credentials: 'include', // передаем куки
    })

    // Проксируем заголовки
    for (const [key, value] of response.headers.entries()) {
        if (key.toLowerCase() !== 'transfer-encoding') {
            event.node.res.setHeader(key, value)
        }
    }

    // Ставим статус
    event.node.res.statusCode = response.status

    // Для "json" контента возвращаем объект, для других — как текст/буфер
    const contentType = response.headers.get('content-type') || ''
    if (contentType.includes('application/json')) {
        return await response.json()
    } else if (contentType.startsWith('text/')) {
        return await response.text()
    } else {
        return Buffer.from(await response.arrayBuffer())
    }
})