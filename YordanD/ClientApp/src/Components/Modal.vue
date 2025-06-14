<script setup lang="ts">
import { VueFinalModal } from "vue-final-modal";
import { XCircleIcon } from "lucide-vue-next";
import { useVfm } from 'vue-final-modal';
import { Separator } from "@/Components/ui/separator";
import { ScrollArea } from "@/Components/ui/scroll-area";
import { cn } from "@/lib/utils.ts";


const { title, modalId, ...props } = defineProps<{
  title?: string,
  modalId: number|string|symbol,
  class?: string,
  contentClass?: string,
}>()

const vfm = useVfm()

defineExpose({
  open: () => vfm.open(modalId),
  close: () => vfm.close(modalId),
})

</script>

<template>
  <VueFinalModal
      :modal-id="modalId"
      :class="cn('flex justify-center items-center', props.class)"
      :content-class="cn('relative flex flex-col max-w-[90dvw] w-full max-h-[95dvh] mx-4 p-4 bg-white/60 dark:bg-zinc-900/60 backdrop-blur shadow-lg border-card rounded-lg space-y-2', contentClass)"
      overlay-class="!bg-black/10"
  >
    <h5 class="text-lg"><span v-if="title">{{ title }}</span></h5>
    <Separator class="bg-zinc-600" />
    <XCircleIcon class="absolute top-2 end-2 hover:text-destructive cursor-pointer" @click="() => vfm.close(modalId)" />
    <ScrollArea class="max-h-[85dvh] w-full">
      <slot />
    </ScrollArea>
  </VueFinalModal>
</template>

<style scoped>

</style>