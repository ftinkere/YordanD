<script setup lang="ts">
import { defineProps, provide, reactive, ref, watch } from "vue";
import { watchDebounced } from "@vueuse/core";
import { useWindowSize } from '@vueuse/core'
import LightButton from "@/Components/ui/button/LightButton.vue";
import GhostButton from "@/Components/ui/button/GhostButton.vue";
import { ChevronLeftIcon, ChevronRightIcon, HouseIcon, LogInIcon, LogOutIcon, LanguagesIcon } from "lucide-vue-next";
import { Link } from "@inertiajs/vue3";
import { Avatar, AvatarFallback, AvatarImage } from "@/Components/ui/avatar";
import { useUserStore } from "@/Stores/useUserStore.ts";
import { ModalsContainer } from "vue-final-modal";
import type { User } from "@/types";

const isCompact = ref(false);
const { width } = useWindowSize({ type: "inner" })

watchDebounced(width, (newWidth) => {
  const compactNow = newWidth < 700
  if (compactNow !== isCompact.value) {
    isCompact.value = compactNow
  }
}, { debounce: 200 })

const { auth } = defineProps<{
  auth?: {
    user?: User,
  }
}>();

const userStore = useUserStore()

if (!auth?.user) {
  userStore.setUser(null)
} else {
  userStore.setUser(auth?.user)
}
watch(() => auth?.user, (user: User|undefined) => {
  if (!user) {
    userStore.setUser(null)
  } else {
    userStore.setUser(user)
  }
})


function initials(name: string): string {
  // Разбиваем по пробелам, убираем пустые части
  const parts = name.trim().split(/\s+/).filter(Boolean);

  if (parts.length >= 2) {
    // Если два или больше: берём первые символы двух первых слов
    return (Array.from(parts[0])[0] + Array.from(parts[1])[0])
        .toUpperCase();
  } else if (parts.length === 1) {
    // Если одно слово: первые две буквы
    const chars = Array.from(parts[0]);
    return (chars[0] ?? '' + chars[1] ?? '')
        .toUpperCase();
  }
  // Если строка пустая
  return '';
}

const topRightButton = reactive({
  name: null as string|null,
  color: null as "zinc" | "red" | "orange" | "amber" | "yellow" | "lime" | "green" | "emerald" | "teal" | "cyan" | "sky" | "blue" | "indigo" | "violet" | "purple" | "fuchsia" | "pink" | "rose" | null,
  action: null as Function|null,
})

const setButton = (name: string, color: "zinc" | "red" | "orange" | "amber" | "yellow" | "lime" | "green" | "emerald" | "teal" | "cyan" | "sky" | "blue" | "indigo" | "violet" | "purple" | "fuchsia" | "pink" | "rose" | null, action: Function) => {
  topRightButton.name = name
  topRightButton.color = color
  topRightButton.action = action

  console.log(name);
}

provide('setButton', setButton)

</script>

<template>
  <div class="grid" :class="{ 'compact': isCompact }">
    <aside class="flex flex-col justify-between h-full min-h-[100dvh] w-full bg-zinc-200 dark:bg-zinc-800 transition-all duration-300 ease-in-out">
      <!-- Up -->
      <div>
        <GhostButton color="amber" class="w-full h-12 yordan-font align-baseline">
          <span v-show="isCompact" class="text-3xl underline text-amber-500">Ë</span>
          <Transition name="span">
          <span v-show="! isCompact" class="text-3xl whitespace-nowrap duration-300 ease-in-out text-amber-500">
            <span class="underline">Ë</span>рдан
          </span>
          </Transition>
        </GhostButton>
        
        <Link href="/" prefetch>
          <GhostButton color="amber" class="w-full">
            <HouseIcon class="!size-5 text-foreground" />
            <Transition name="span">
            <span v-show="! isCompact" class="text-foreground whitespace-nowrap duration-300 ease-in-out">
              Главная
            </span>
            </Transition>
          </GhostButton>
        </Link>
        <Link href="/languages" prefetch>
          <GhostButton color="amber" class="w-full">
            <LanguagesIcon class="!size-5 text-foreground" />
            <Transition name="span">
            <span v-show="! isCompact" class="text-foreground whitespace-nowrap duration-300 ease-in-out">
              Все языки
            </span>
            </Transition>
          </GhostButton>
        </Link>
      </div>
      <!-- Down -->
      <div class="pb-2">
        <Link v-if="userStore.user != null" href="/profile" prefetch>
          <GhostButton color="amber" class="w-full mb-4">
            <Avatar>
              <AvatarImage :src="`/avatar/${userStore.user.id}`" alt="@unovue" />
              <AvatarFallback>{{ initials(userStore.user.displayName) }}</AvatarFallback>
            </Avatar>
            <Transition name="span">
            <span v-show="! isCompact" class="text-foreground whitespace-nowrap duration-300 ease-in-out">
              {{ userStore.user.displayName }}
            </span>
            </Transition>
          </GhostButton>
        </Link>
        
        <Link v-if="userStore.user == null" href="/login" prefetch>
          <GhostButton color="amber" class="w-full">
            <LogInIcon class="!size-5 text-foreground" />
            <Transition name="span">
            <span v-show="! isCompact" class="text-foreground whitespace-nowrap duration-300 ease-in-out">
              Войти
            </span>
            </Transition>
          </GhostButton>
        </Link>
        <Link v-else href="/logout" prefetch>
          <GhostButton color="amber" class="w-full">
            <LogOutIcon class="!size-5 text-foreground" />
            <Transition name="span">
            <span v-show="! isCompact" class="text-foreground whitespace-nowrap duration-300 ease-in-out">
              Выйти
            </span>
            </Transition>
          </GhostButton>
        </Link>
      </div>
    </aside>
    <div class="flex flex-col gap-4">
      <header class="flex flex-row gap-1 items-center pe-2 h-12 w-full bg-zinc-200 dark:bg-zinc-800">
        <GhostButton class="h-12 aspect-square" @click="isCompact = true" v-show="! isCompact">
          <ChevronLeftIcon class="!w-6 !h-8" />
        </GhostButton>
        <GhostButton class="h-12 aspect-square" @click="isCompact = false" v-show="isCompact">
          <ChevronRightIcon class="!w-6 !h-8" />
        </GhostButton>
        
        <div class="ms-auto" v-if="topRightButton.name">
          <LightButton :color="topRightButton.color ?? 'zinc'" @click="() => topRightButton.action ? topRightButton.action() : ''">
            {{ topRightButton.name }}
          </LightButton>
        </div>
      </header>
      <main class="p-2">
        <slot />
      </main>
    </div>

    <ModalsContainer />
  </div>
</template>

<style scoped>
  div.grid {
    grid-template-columns: var(--sidebar-width, 200px) 1fr;
    transition: grid-template-columns 0.25s ease-in-out;
  }
  
  div.grid.compact {
    --sidebar-width: 50px;
  }
  
  .span-enter-active {
    transition: all 0.6s ease-out;
  }
  
  .span-leave-active {
    transition: all 0s;
  }
  
  .span-leave-to,
  .span-enter-from {
    opacity: 0;
  }
</style>
