<script setup lang="ts">
import type { Language } from '~/composables/useLanguages'

const props = defineProps<{ language: Language }>()
</script>

<template>
  <UCard variant="soft">
    <template #header>
      <UButton variant="link" :to="`/languages/${props.language.id}`" class="text-neutral hover:text-primary">
        <h5 class="text-lg">{{ props.language.name }} — {{ props.language.autoName }} /{{ props.language.autoNameTranscription }}/</h5>
      </UButton>
    </template>
    {{ props.language.description }}
  </UCard>
</template>

<style scoped></style>
