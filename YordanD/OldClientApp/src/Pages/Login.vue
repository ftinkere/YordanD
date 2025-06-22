<script setup lang="ts">
  import App from "../App.vue";
  import { Form, type SubmissionContext } from "vee-validate";
  import FormField from "@/Components/ui/form/FormField.vue";
  import LightButton from "@/Components/ui/button/LightButton.vue";
  import { toTypedSchema } from "@vee-validate/zod";
  import * as zod from "zod";
  import { Link, router } from "@inertiajs/vue3";
  import { ref, watch } from "vue";
  import GhostButton from "@/Components/ui/button/GhostButton.vue";
  
  defineOptions({
    layout: App
  })
  
  const formSchema = toTypedSchema(zod.object({
    username: zod.string({ message: "" }).min(2, "Не меньше 2 символов").max(255, "Не больше 255 символов"),
    password: zod.string({ message: "" }).min(8, "Не меньше 8 символов").max(255, "Не больше 255 символов"),
  }))

  const ctxRef = ref<SubmissionContext<any>|null>(null)
  
  function onSubmit(values: any, ctx: SubmissionContext<any>) {
    ctxRef.value = ctx;
    
    router.post("/login", values)
  }

  const { errors } = defineProps<{ errors: Record<string, string> }>()
  watch(() => errors, (newValues) => {
    if (ctxRef.value) {
      ctxRef.value.setErrors(newValues)
    }
  })
</script>

<template>
  <div class="flex flex-col gap-2">
    <h1 class="text-2xl text-center">Войти в Храм Себека</h1>
    <Form class="py-10 flex flex-col gap-2 w-full max-w-sm mx-auto"
          :validation-schema="formSchema"
          :on-submit="onSubmit"
    >
      <FormField name="username" label="Имя пользователя" />
      <FormField name="password" label="Пароль" type="password" />
      
      <LightButton type="submit" class="mt-2" color="amber">Войти</LightButton>
    </Form>
    <Link href="/register" prefetch class="mx-auto">
      <GhostButton color="amber">Нет аккаунта? Зарегестрироваться</GhostButton>
    </Link>
  </div>
</template>

<style scoped>

</style>