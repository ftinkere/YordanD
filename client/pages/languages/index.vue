<script setup lang="ts">
  import { useOffsetPagination } from "@vueuse/core";
  import { ref } from 'vue'
  import AddLanguageModal from '~/components/languages/AddLanguageModal.vue'
  import LanguageCard from '~/components/languages/LanguageCard.vue'

  const { data, pending, error, createLanguage, refreshLanguages } = useLanguages()

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

</script>

<template>
  <div class="mx-auto max-w-4xl">
    <h3 class="text-2xl font-bold">Все языки</h3>

    <div v-if="!pending && !error" class="flex flex-col gap-2">
      <LanguageCard v-for="language in pageLanguages" :key="language.id" :language="language" />

      <UPagination v-model:page="currentPage" :total="data?.items.length" :items-per-page="currentPageSize" show-edges variant="soft" active-variant="soft" />

      <AddLanguageModal v-model="addModal" :create-language="createLanguage" @created="refreshLanguages" />
    </div>
    <div v-else-if="error">
      <UAlert color="error" title="Ошибка при загрузке языков" />
    </div>
  </div>
</template>

<style scoped>

</style>