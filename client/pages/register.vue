<script setup lang="ts">
import * as z from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'

const schema = z.object({
    userName: z.string({ message: 'Имя пользователя обязательно' }),
    displayName: z.string({ message: 'Отображаемое имя обязательно' }),
    email: z.string({ message: 'Почта обязательна' }).email('Почта должна быть почтой'),
    password: z.string({ message: 'Пароль обязателен' }).min(8, 'Минимум 8 символов'),
    confirmPassword: z.string({ message: 'Подтверждение пароля обязательно' }).min(8, 'Минимум 8 символов'),
})

type Schema = z.output<typeof schema>

const state = reactive<Partial<Schema>>({
    userName: undefined,
    displayName: undefined,
    email: undefined,
    password: undefined,
    confirmPassword: undefined,
})

const { register } = useAuth()
const formError = ref('')
const isSubmitting = ref(false)

async function onSubmit(event: FormSubmitEvent<Schema>) {
    formError.value = ''
    isSubmitting.value = true

    try {
        if (event.data.password !== event.data.confirmPassword) {
            formError.value = 'Пароли не совпадают'
            return
        }

        const success = await register(event.data)

        if (success) {
            await useRouter().push('/')
        } else {
            formError.value = 'Не удалось зарегистрироваться. Возможно, имя пользователя уже занято.'
        }
    } catch (error) {
        console.error('Registration error:', error)
        formError.value = 'Произошла ошибка при регистрации. Пожалуйста, попробуйте позже.'
    } finally {
        isSubmitting.value = false
    }
}

</script>

<template>
  <div class="mx-auto max-w-sm">
    <h4 class="text-3xl font-bold yordan-font text-center">Регистрация</h4>

    <UAlert v-if="formError" class="mt-4" color="error" variant="soft" icon="i-lucide-alert-circle">
      {{ formError }}
    </UAlert>

    <UForm :schema="schema" :state="state" class="mx-auto space-y-4 w-full mt-4" @submit="onSubmit">
      <UFormField label="Имя пользователя" name="userName">
        <UInput v-model="state.userName" class="w-full" placeholder="username" />
      </UFormField>

      <UFormField label="Отображаемое имя" name="displayName">
        <UInput v-model="state.displayName" class="w-full" placeholder="Иван Иванов" />
      </UFormField>

      <UFormField label="Почта" name="email">
        <UInput v-model="state.email" class="w-full" placeholder="user@yordan.ru" />
      </UFormField>

      <UFormField label="Пароль" name="password">
        <UInput v-model="state.password" type="password" class="w-full" />
      </UFormField>

      <UFormField label="Подтверждение пароля" name="passwordConfirm">
        <UInput v-model="state.confirmPassword" type="password" class="w-full" />
      </UFormField>

      <UButton type="submit" class="w-full justify-center" :loading="isSubmitting" :disabled="isSubmitting">
        Зарегистрироваться
      </UButton>

      <div class="text-center text-sm">
        Уже есть аккаунт?
        <NuxtLink to="/login" class="text-primary hover:underline">
          Войти
        </NuxtLink>
      </div>
    </UForm>
  </div>
</template>

<style scoped>

</style>