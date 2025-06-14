<script setup lang="ts">
import type { HTMLAttributes } from 'vue'
import { reactiveOmit } from '@vueuse/core'
import { AvatarFallback, type AvatarFallbackProps } from 'reka-ui'
import { cn } from '@/lib/utils'

const props = defineProps<AvatarFallbackProps & { class?: HTMLAttributes['class'] }>()

const delegatedProps = reactiveOmit(props, 'class')
</script>

<template>
  <AvatarFallback
    data-slot="avatar-fallback"
    v-bind="delegatedProps"
    :class="cn('bg-zinc-800 text-zinc-200 dark:bg-zinc-200 dark:text-zinc-900 flex size-full items-center justify-center rounded-full', props.class)"
  >
    <slot />
  </AvatarFallback>
</template>
