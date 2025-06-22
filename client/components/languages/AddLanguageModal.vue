<script setup lang="ts">
import { z } from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'

const props = defineProps<{
  modelValue: boolean
  createLanguage: (language: { name: string; autoName?: string; autoNameTranscription?: string; isPublished: boolean }) => Promise<boolean> | boolean
}>()
const emit = defineEmits(['update:modelValue','created'])

const schema = z.object({
  name: z.string().nonempty('Название языка не может быть пустым'),
  autoName: z.string().optional(),
  autoNameTranscription: z.string().optional(),
  isPublished: z.boolean().default(false),
})

type Schema = z.output<typeof schema>

const state = reactive<Schema>({
  name: '',
  autoName: '',
  autoNameTranscription: '',
  isPublished: false,
})

async function onSubmit(event: FormSubmitEvent<Schema>) {
  await props.createLanguage(event.data)
  emit('created')
  emit('update:modelValue', false)
  state.name = ''
  state.autoName = ''
  state.autoNameTranscription = ''
  state.isPublished = false
}
</script>

<template>
  <UModal :model-value="modelValue" @update:model-value="emit('update:modelValue', $event)" title="Создать язык">
    <template #body>
      <UCard>
        <UForm :state="state" :schema="schema" @submit="onSubmit">
          <div class="flex flex-col gap-2 w-full">
            <UFormField name="name" label="Название языка (понятное)" required>
              <UInput v-model="state.name" class="w-full" />
            </UFormField>
            <UFormField name="autoName" label="Самоназвание языка">
              <UInput v-model="state.autoName" class="w-full" />
            </UFormField>
            <UFormField name="autoNameTranscription" label="Транскрипция самоназвания языка">
              <UInput v-model="state.autoNameTranscription" class="w-full">
                <template #trailing>
                  <XsampaToIpaButton v-model="state.autoNameTranscription" />
                </template>
              </UInput>
            </UFormField>
            <UFormField name="isPublished">
              <USwitch v-model="state.isPublished" label="Публично доступен" />
            </UFormField>
            <UButton type="submit" variant="soft" color="success" class="w-full justify-center cursor-pointer">Добавить</UButton>
          </div>
        </UForm>
      </UCard>
    </template>
  </UModal>
</template>

<style scoped>
</style>
