<script setup lang="ts">
import type { HTMLAttributes } from 'vue'
import { cn } from '@/lib/utils'

const props = defineProps<{
  class?: HTMLAttributes['class']
}>()
</script>

<template>
  <div
    data-slot="card"
    :class="
      cn(
        'bg-card/20 text-card-foreground flex flex-col gap-2 rounded-lg border py-2 shadow-sm',
        props.class,
      )
    "
  >
    <slot />
  </div>
</template>
