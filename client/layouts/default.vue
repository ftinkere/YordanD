<script setup lang="ts">
import AppAside from "~/layouts/components/AppAside.vue";
import AppHeader from "~/layouts/components/AppHeader.vue";
import type { NavigationMenuItem } from "#ui/components/NavigationMenu.vue";

const compact = ref(false);

const button = reactive({
    label: undefined as string | undefined,
    variant: undefined as "link" | "ghost" | "solid" | "outline" | "soft" | "subtle" | undefined,
    color: undefined as "primary" | "secondary" | "success" | "info" | "warning" | "error" | "neutral" | undefined,
    icon: undefined as string | undefined,
    action: undefined as Function | undefined,
})

function setButton(label?: string, variant?: "link" | "ghost" | "solid" | "outline" | "soft" | "subtle" , color?: "primary" | "secondary" | "success" | "info" | "warning" | "error" | "neutral", icon?: string, action?: Function) {
    button.label = label
    button.variant = variant
    button.color = color
    button.icon = icon
    button.action = action
}

provide('setButton', setButton)

const language = inject<Ref<Language>>('language')

const addItems = computed(() => [
    { label: 'О языке', icon: 'i-lucide-info', to: `/languages/${language?.value.id}` } as NavigationMenuItem,
    { label: 'Словарь', icon: 'i-lucide-languages', to: `/languages/${language?.value.id}/dictionary` } as NavigationMenuItem,
    { label: 'Документы', icon: 'i-lucide-file', to: `/languages/${language?.value.id}/documents` } as NavigationMenuItem,
    { label: 'Грамматика', icon: 'i-lucide-square-code', to: `/languages/${language?.value.id}/grammatic` } as NavigationMenuItem,
    { label: 'Фонетика', icon: 'i-lucide-speech', to: `/languages/${language?.value.id}/phonetic` } as NavigationMenuItem,
    { label: 'Графика', icon: 'i-lucide-pencil-line', to: `/languages/${language?.value.id}/graphic` } as NavigationMenuItem,
])
</script>

<template>
  <UApp>
    <div class="grid grid-cols-[auto_minmax(0,100%)]">
      <AppAside :compact="compact" :add-items />
      <div class="flex flex-col gap-2">
        <AppHeader :compact="compact" @compact="(value: boolean) => compact = value" :button />
        <div class="mx-auto px-2 w-full">
          <slot />
        </div>
      </div>
    </div>
  </UApp>
</template>

<style scoped>

</style>