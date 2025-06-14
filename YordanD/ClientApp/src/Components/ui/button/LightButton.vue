<script setup lang="ts">
import type { HTMLAttributes } from 'vue'
import { cn } from '@/lib/utils'
import { Primitive, type PrimitiveProps } from 'reka-ui'
import {type LightButtonVariants, lightButtonVariants} from '.'

interface Props extends PrimitiveProps {
  size?: LightButtonVariants['size']
  color?: LightButtonVariants['color']
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
    :class="cn(lightButtonVariants({ size, color }), props.class)"
  >
    <slot />
  </Primitive>
</template>
