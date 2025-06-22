<script setup lang="ts">
import { z } from 'zod';
import type { FormSubmitEvent } from '@nuxt/ui'

definePageMeta({
    layout: 'language',
})

const language = inject<Ref<Language>>('language')

const schema = z.object({
    description: z.string().optional(),
    name: z.string({ message: 'Название обязательно' }),
    autoName: z.string().optional(),
    autoNameTranscription: z.string().optional(),
    isPublished: z.boolean(),
})

type Schema = z.output<typeof schema>

const state = reactive<Schema>({
    description: language?.value.description,
    name: language?.value.name ?? '',
    autoName: language?.value.autoName,
    autoNameTranscription: language?.value.autoNameTranscription,
    isPublished: language?.value.isPublished ?? false,
})

const setButton = inject('setButton') as Function | undefined

function toggleEdit() {
    useRouter().push(`/languages/${ language?.value.id }/`)
}

onMounted(() => {
    if (setButton) setButton('Отменить', 'soft', 'error', 'i-lucide-x', toggleEdit)
})
onBeforeUnmount(() => {
    if (setButton) setButton()
})

const refreshLanguage = inject<Function>('refreshLanguage')

async function saveEdit(event: FormSubmitEvent<typeof state>) {
    const { data, error } = await useFetch(`/api/v1/languages/${ language?.value.id }`, {
        method: 'PUT',
        body: JSON.stringify(event.data),
        headers: { 'Content-Type': 'application/json' },
        credentials: 'include',
        watch: false,
    })

    if (!error.value && language) {
        language.value = data.value as Language
        if (refreshLanguage) refreshLanguage()
        toggleEdit()
    }
}
</script>

<template>
    <div v-if="language" class="mx-auto max-w-4xl">
        <UForm class="w-full flex flex-col gap-2" :state :schema @submit="saveEdit">
            <UFormField label="Название" name="name">
                <UInput v-model="state.name" class="w-full" placeholder="Название"/>
            </UFormField>
            <div class="grid grid-cols-2 gap-2">
                <UFormField label="Самоназвание" name="autoName">
                    <UInput v-model="state.autoName" class="w-full" placeholder="Название"/>
                </UFormField>
                <UFormField label="Транскрипция" name="autoNameTranscription">
                    <UInput v-model="state.autoNameTranscription" class="w-full" placeholder="Название">
                        <template #trailing>
                            <XsampaToIpaButton v-model="state.autoNameTranscription"/>
                        </template>
                    </UInput>
                </UFormField>
            </div>
            <UFormField name="isPublished">
                <USwitch v-model="state.isPublished" label="Опубликовать"/>
            </UFormField>
            <UFormField label="Описание" name="description">
                <Editor v-model="state.description"/>
            </UFormField>
            <UButton type="submit" class="justify-center w-full cursor-pointer" variant="soft" color="success">
                Сохранить
            </UButton>
        </UForm>
    </div>
</template>

<style scoped>

</style>