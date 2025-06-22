<script setup lang="ts">
definePageMeta({
    layout: 'language',
})

const language = inject('language') as Ref<Language> | undefined

const setButton = inject('setButton') as Function | undefined

function addArticle() {}

onMounted(() => {
    if (setButton) setButton('Добавить статью', 'soft', 'success', 'i-lucide-plus', addArticle)
})
onUnmounted(() => {
    if (setButton) setButton()
})
</script>

<template>
  <div class="mx-auto max-w-4xl">
    Словарь {{ language?.name }}
  </div>
</template>

<style scoped>

</style>