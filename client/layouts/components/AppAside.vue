<script setup lang="ts">
  import type { NavigationMenuItem } from "#ui/components/NavigationMenu.vue";

  const { compact, addItems } = defineProps<{
      compact: boolean;
      addItems: NavigationMenuItem[]
    }>()

  const items = computed<NavigationMenuItem[][]>(() => [
      [
        { label: 'Главная', to: '/', icon: 'i-lucide-home' },
        { label: 'Языки', to: '/languages', icon: 'i-lucide-languages' },
      ],
      [
        { label: 'Контакты', to: '/contact', icon: 'i-lucide-mail', disabled: true },
        { label: 'Помощь', to: '/help', icon: 'i-lucide-circle-help', disabled: true },
      ],
      addItems,
  ])

  const { user, isAuthenticated, logout } = useAuth()
</script>

<template>
    <aside
      class="flex flex-col justify-between
       h-full min-h-[100dvh] w-[200px] [.compact]:w-[50px]
       bg-header
       transition-all duration-300 ease-in-out
       shadow-md"
      :class="{ 'compact': compact }"
    >
      <div class="flex flex-col gap-2 items-center-safe">
        <UButton variant="ghost" class="pt-2 w-full yordan-font text-3xl justify-center transition">
          <span>Ë<Transition name="span"><span v-show="!compact" :class="{ 'opacity-0': compact }">рдан</span></Transition></span>
        </UButton>

        <UNavigationMenu
          :tooltip="compact"
          :collapsed="compact"
          orientation="vertical"
          :items="items"
          class="pb-6 data-[orientation=vertical]:w-full **:[.iconify]:text-2xl"
          :class="{'**:[.iconify]:mx-auto': compact }"
        />
      </div>
      <!-- Up -->
      <!-- Down -->
      <div>
        <div v-if="isAuthenticated && user" class="mx-auto w-full flex flex-row gap-1 justify-center items-center">
          <UAvatar :src="user.avatar" :alt="user.displayName ?? undefined" class="bg-black/20" />
          <span v-show="!compact">{{ user?.displayName ?? user?.userName }}</span>
        </div>
        <div v-show="isAuthenticated">
          <UButton
            variant="ghost"
            icon="i-lucide-log-out"
            class="py-4 w-full justify-center text-lg cursor-pointer transition"
            @click="logout"
          ><Transition name="span"><span v-show="!compact" :class="{ 'opacity-0': compact }">Выйти</span></Transition></UButton>
        </div>
        <div v-show="!isAuthenticated">
          <UButton
            variant="ghost"
            icon="i-lucide-log-in"
            class="py-4 w-full justify-center text-lg cursor-pointer transition"
            to="/login"
          ><Transition name="span"><span v-show="!compact" :class="{ 'opacity-0': compact }">Войти</span></Transition></UButton>
        </div>
      </div>
    </aside>
</template>

<style scoped>
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