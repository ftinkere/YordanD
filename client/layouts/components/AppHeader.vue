<script setup lang="ts">
  const emit = defineEmits<{
      compact: [boolean],
  }>()
  const { compact } = defineProps<{
      compact: boolean,
      button: { label: string | undefined ; variant: "link" | "soft" | "outline" | "solid" | "subtle" | "ghost" | undefined; color: "primary" | "secondary" | "success" | "info" | "warning" | "error" | "neutral" | undefined; icon: string | undefined, action: Function | undefined },
  }>()
</script>

<template>
  <header
    class="flex flex-row gap-1 items-center
     pe-2 h-12
     w-full
     bg-header
     shadow-md"
  >
    <UButton
      v-if="!compact"
      icon="i-lucide-chevron-left"
      square
      variant="ghost"
      color="neutral"
      class="text-2xl cursor-pointer hover:bg-neutral-700"
      @click="emit('compact', true)"
    />
    <UButton
      v-else
      icon="i-lucide-chevron-right"
      square
      variant="ghost"
      color="neutral"
      class="text-2xl cursor-pointer hover:bg-neutral-700"
      @click="emit('compact', false)"
    />
    <!-- Left -->
    <!-- Right -->
    <div class="ms-auto flex flex-row gap-1 items-center">
      <UButton v-if="button.label" :label="button.label" :variant="button.variant" :color="button.color" :icon="button.icon" class="justify-center cursor-pointer" @click="button.action ? button.action() : ''" />
    </div>
  </header>
</template>

<style scoped>

</style>