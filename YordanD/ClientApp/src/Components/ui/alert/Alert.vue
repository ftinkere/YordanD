<script setup lang="ts">
import type { HTMLAttributes } from 'vue'
import { cn } from '@/lib/utils'
import { type AlertVariants, alertVariants } from '.'

const props = defineProps<{
  class?: HTMLAttributes['class']
  variant?: AlertVariants['variant']
}>()
</script>

<template>
    <Transition name="alert">
        <div
        data-slot="alert"
        :class="cn(alertVariants({ variant }), props.class)"
        role="alert"
        >
            <slot />
        </div>
    </Transition>
</template>

<style scoped>
.alert-enter-active,
.alert-leave-active {
  transition: transform 0.4s ease;
}

.alert-enter-from,
.alert-leave-to {
  transform: translateY(-40px);
}
</style>
