import {Temporal} from '@js-temporal/polyfill';
import { useState, useFetch } from '#app'

export interface User {
    id: string;
    userName: string;
    displayName: null|string;
    avatar: string | undefined;
}

/**
 * Checks if a timestamp is in the future
 */
function isInFuture(expiresAt: string): boolean {
    const input = Temporal.Instant.from(expiresAt);
    const now = Temporal.Now.instant();
    return input.epochMilliseconds > now.epochMilliseconds;
}

export const useAuth = () => {
    const user = useState<User | null>('auth:user', () => null)
    const expiresAt = useState<string | null>('auth:expires-at', () => null)
    const isAuthenticated = useState<boolean>('auth:is-authenticated', () => false)

    const isExpired = (expiresAt: string | null) => {
        if (!expiresAt) return true
        return !isInFuture(expiresAt)
    }

    // Обновить пользователя
    const fetchUser = async (force = false) => {
        // Если есть пользователь и не force, и кука ещё жива — не обновляем
        if (
            user.value !== null &&
            !force &&
            !isExpired(expiresAt.value) &&
            isAuthenticated.value
        ) return user.value

    const { data, error } = await useFetch<{ user: User, expiresAt: string, avatar?: string }>('/api/v1/me', {
            watch: false,
            headers: useRequestHeaders(['cookie']),
            credentials: 'include',
        })

        if (error.value || !data.value || isExpired(data.value?.expiresAt)) {
            user.value = null
            isAuthenticated.value = false
        } else {
            user.value = data.value.user
            user.value.avatar = data.value.avatar
            expiresAt.value = data.value.expiresAt
            isAuthenticated.value = true
            setupAutoLogout() // автоматический логаут при истечении времени
        }
        return user.value
    }

    const login = async (username: string, password: string) => {
        const { error } = await useFetch('/api/v1/login', {
            method: 'POST',
            body: JSON.stringify({ userName: username, password }),
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
            watch: false,
        })

        if (!error.value) {
            await fetchUser(true)
            return true
        }
        return false
    }

    const register = async (data: { userName: string, displayName: string, email: string, password: string }) => {
        const { error } = await useFetch('/api/v1/register', {
            method: 'POST',
            body: JSON.stringify(data),
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
            watch: false,
        })

        if (!error.value) {
            await fetchUser(true)
            return true
        }
        return false
    }

    // Сброс пользователя
    const logout = async () => {
        const { error } = await useFetch('/api/v1/logout', {
            method: 'POST',
            watch: false,
            headers: useRequestHeaders(['cookie']),
            credentials: 'include',
        })
        if (!error.value) {
            user.value = null
            expiresAt.value = null
            isAuthenticated.value = false
            if (autoLogoutTimeout) {
                clearTimeout(autoLogoutTimeout)
                autoLogoutTimeout = null
            }
        }
    }

    // Автоматическое обновление user при старте на клиенте
    if (! isAuthenticated.value) {
        fetchUser()
    }

    // --------- Поддержка авто-логаута при истечении expiresAt ---------
    let autoLogoutTimeout: ReturnType<typeof setTimeout> | null = null
    function setupAutoLogout() {
        if (!import.meta.client) return
        if (autoLogoutTimeout) clearTimeout(autoLogoutTimeout)
        if (expiresAt.value === null) return

        const expires = Temporal.Instant.from(expiresAt.value)
        const delay = expires.epochMilliseconds - Temporal.Now.instant().epochMilliseconds
        if (delay > 0) {
            autoLogoutTimeout = setTimeout(() => {
                logout()
            }, delay)
        }
    }
    if (import.meta.client && expiresAt.value) {
        setupAutoLogout()
    }
    // ---------------------------------------------------------------

    return {
        user,
        isAuthenticated,
        fetchUser,
        login,
        register,
        logout,
    }
}
