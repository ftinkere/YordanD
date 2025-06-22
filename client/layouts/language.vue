<script setup lang="ts">
import { useLanguage } from "#imports";

const id = useRoute().params.id;

const { data: language, pending, error, refresh } = useLanguage(id)

provide('language', language)
provide('refreshLanguage', refresh)

</script>

<template>
    <NuxtLayout name="default">
        <div v-if="!error && !pending" class="flex flex-col gap-2">
            <h5 class="w-full text-2xl font-bold text-center">{{ language?.name }} — {{ language?.autoName }}
                /{{ language?.autoNameTranscription }}/</h5>
            <slot/>
        </div>
        <div v-else-if="error">
            <UAlert :title="error" variant="soft" color="error"/>
        </div>
    </NuxtLayout>
</template>

<style scoped>

</style>