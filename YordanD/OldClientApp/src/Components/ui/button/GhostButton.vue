<script setup lang="ts">
import type { HTMLAttributes } from 'vue'
import { cn } from '@/lib/utils'
import { Primitive, type PrimitiveProps } from 'reka-ui'
import {type GhostButtonVariants, ghostButtonVariants} from '.'

interface Props extends PrimitiveProps {
  size?: GhostButtonVariants['size']
  color?: GhostButtonVariants['color']
  class?: HTMLAttributes['class']
}

const props = withDefaults(defineProps<Props>(), {
  as: 'button',
})
</script>

<template>
  <Primitive
    data-slot="button"
    :as="as"
    :as-child="asChild"
    :class="cn(ghostButtonVariants({ size, color }), props.class)"
  >
    <slot />
  </Primitive>
</template>
