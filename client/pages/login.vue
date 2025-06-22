<script setup lang="ts">
import * as z from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'

const schema = z.object({
    userName: z.string({ message: '' }),
    password: z.string({ message: '' }).min(8, 'Минимум 8 символов'),
})

type Schema = z.output<typeof schema>

const state = reactive<Partial<Schema>>({
    userName: undefined,
    password: undefined
})

const formError = ref<string|null>(null)

async function login(username: string, password: string): Promise<boolean> {
    const { error } = await useFetch('/api/v1/login', {
        method: 'POST',
        body: JSON.stringify({ username, password }),
        headers: { 'Content-Type': 'application/json' },
        credentials: 'include',
        watch: false,
    })
    if (error.value) {
        formError.value = "Неправильный логин или пароль"
        setTimeout(() => {
            formError.value = null
        }, 5000)
        return false
    }
    formError.value = null;

    const { fetchUser } = useAuth()
    await fetchUser()

    return true
}

async function onSubmit(event: FormSubmitEvent<Schema>) {
    if (await login(event.data.userName, event.data.password)) {
        await useRouter().push('/')
    }
}

</script>

<template>
  <div class="mx-auto max-w-sm">
    <h4 class="text-3xl font-bold yordan-font text-center">Войти</h4>
    <UForm :schema="schema" :state="state" class="mx-auto space-y-4 w-full" @submit="onSubmit">
      <UFormField label="Имя пользователя" name="userName">
        <UInput v-model="state.userName" class="w-full" />
      </UFormField>

      <UFormField label="Пароль" name="password" :error="formError">
        <UInput v-model="state.password" type="password" class="w-full" />
      </UFormField>

      <UButton type="submit" class="w-full justify-center">
        Войти
      </UButton>

      <div class="text-center text-sm">
        Нет аккаунта?
        <NuxtLink to="/register" class="text-primary hover:underline">
          Зарегистрироваться
        </NuxtLink>
      </div>
    </UForm>
  </div>
</template>

<style scoped>

</style>