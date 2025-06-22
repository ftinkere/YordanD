<script setup lang="ts">

definePageMeta({
    layout: 'language',
})

const language = inject<Ref<Language>>('language')

const setButton = inject<Function>('setButton')

function toggleEdit() {
    useRouter().push(`/languages/${ language?.value.id }/edit`)
}

onMounted(() => {
    if (setButton) setButton('Редактировать', 'soft', 'primary', 'i-lucide-edit', toggleEdit)
})
onBeforeUnmount(() => {
    if (setButton) setButton()
})
</script>

<template>
    <div class="mx-auto max-w-4xl ProseMirror" v-html="language?.description"/>
</template>

<style scoped>

</style>