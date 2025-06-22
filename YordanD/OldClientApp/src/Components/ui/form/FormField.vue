<script lang="ts" setup>
import { type HTMLAttributes } from 'vue'
import { cn } from '@/lib/utils'
import { FormControl, FormItem, FormLabel, FormMessage } from "@/Components/ui/form/index.ts";
import { Input } from "@/Components/ui/input";
import { Field } from "vee-validate";

const { name, label, placeholder, type = 'text', ...props } = defineProps<{
  name: string,
  label?: string,
  placeholder?: string,
  type?: string,
  class?: HTMLAttributes['class']
}>()

</script>

<template>
  <Field :name v-slot="{ errorMessage, componentField }" :class="cn(props.class)">
    <FormItem>
      <FormLabel v-if="label">{{ label }}</FormLabel>
      <FormControl>
        <Input :placeholder :type v-bind="componentField" :aria-invalid="!!errorMessage" />
      </FormControl>
      <FormMessage :name />
    </FormItem>
  </Field>
</template>
