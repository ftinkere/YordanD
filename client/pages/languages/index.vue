<script setup lang="ts">
  import { z } from 'zod'
  import type { FormSubmitEvent } from '@nuxt/ui'
  import {useOffsetPagination} from "@vueuse/core";
  import { ref } from 'vue';

  const { data, pending, error, createLanguage } = useLanguages()

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

  const { currentPage, currentPageSize  } = useOffsetPagination({
      total: data.value?.items.length,
      page: 1,
      pageSize: 10,
  })
  const pageLanguages = computed(() => data.value?.items.slice((currentPage.value - 1) * currentPageSize.value, currentPage.value * currentPageSize.value) ?? [])

  const addModal = ref(false)

  const openAddModal = () => addModal.value = true

  const setButton = inject('setButton') as Function|undefined
  onMounted(() => {
    if (setButton) setButton('Создать язык', 'soft', 'success', 'i-lucide-plus', openAddModal)
  })
  onBeforeUnmount(() => {
      if (setButton) setButton()
  })

  async function onSubmit(event: FormSubmitEvent<Schema>) {
      createLanguage(event.data);
      addModal.value = false;
      state.name = '';
      state.autoName = '';
      state.autoNameTranscription = '';
      state.isPublished = false;
  }
</script>

<template>
  <div class="mx-auto max-w-4xl">
    <h3 class="text-2xl font-bold">Все языки</h3>

    <div v-if="!pending && !error" class="flex flex-col gap-2">
      <UCard v-for="language in pageLanguages" :key="language.id" variant="soft">
        <template #header>
          <UButton variant="link" :to="`/languages/${language.id}`" class="text-neutral hover:text-primary">
            <h5 class="text-lg">{{ language.name }} — {{ language.autoName }} /{{ language.autoNameTranscription }}/</h5>
          </UButton>
        </template>
        {{ language.description }}
      </UCard>

      <UPagination v-model:page="currentPage" :total="data?.items.length" :items-per-page="currentPageSize" show-edges variant="soft" active-variant="soft" />

      <UModal v-model:open="addModal" title="Создать язык">
        <template #body>
          <UCard>
            <UForm :state :schema @submit="onSubmit">
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
                <UFormField name="isPublished" >
                  <USwitch v-model="state.isPublished" label="Публично доступен" />
                </UFormField>

                <UButton type="submit" variant="soft" color="success" class="w-full justify-center cursor-pointer">Добавить</UButton>
              </div>
            </UForm>
          </UCard>
        </template>
      </UModal>
    </div>
    <div v-else-if="error">
      <UAlert color="error" title="Ошибка при загрузке языков" />
    </div>
  </div>
</template>

<style scoped>

</style>