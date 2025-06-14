<script setup lang="ts">
import { useEditor, EditorContent } from '@tiptap/vue-3'
import StarterKit from '@tiptap/starter-kit'
import { defineModel, watch } from "vue";
import GhostButton from "@/Components/ui/button/GhostButton.vue";
import { BoldIcon, ItalicIcon, StrikethroughIcon, CodeIcon, 
  Code2Icon, BrushCleaningIcon, ListChecksIcon, ListOrderedIcon, 
  ListIcon, LinkIcon, HeadingIcon, Heading1Icon, Heading2Icon,
  Heading3Icon, Heading4Icon, Heading5Icon } from "lucide-vue-next";
import { Separator } from "@/Components/ui/separator";
import { TaskList, TaskItem } from '@tiptap/extension-list'
import { DropdownMenu, DropdownMenuItem, DropdownMenuTrigger, DropdownMenuContent, DropdownMenuGroup } from "@/Components/ui/dropdown-menu";

const model = defineModel<string>()

const editor = useEditor({
  extensions: [StarterKit, TaskItem, TaskList],
  onUpdate: () => {
    model.value = editor.value?.getHTML()
  }
})

watch(model, (newVal: string|undefined) => {
  const isSame = editor.value?.getHTML() === newVal

  if (isSame) {
    return
  }

  editor.value?.commands.setContent(newVal ?? '')
})

function setLink() {
  const previousUrl = editor.value?.getAttributes('link').href
  const url = window.prompt('URL', previousUrl)

  // cancelled
  if (url === null) {
    return
  }

  // empty
  if (url === '') {
    editor.value?.chain().focus().extendMarkRange('link').unsetLink().run()

    return
  }

  // update link
  editor.value?.chain().focus().extendMarkRange('link').setLink({ href: url }).run()
}
</script>

<template>
  <div class="grid gap-1">
    <div v-if="editor" class="sticky top-0 py-1 px-2 flex flex-row gap-1 rounded-lg bg-card/70 backdrop-blur-xl *:size-8 *:bg-zinc-700/30 **:[&.is-active]:bg-zinc-400/30 **:[&.is-active]:hover:bg-zinc-400/50 z-50">
      
      <DropdownMenu>
        <DropdownMenuTrigger as-child>
          <GhostButton color="zinc">
            <HeadingIcon />
          </GhostButton>
        </DropdownMenuTrigger>
        <DropdownMenuContent class="z-1000">
          <DropdownMenuGroup>
            <DropdownMenuItem @click="editor.chain().focus().toggleHeading({ level: 1 }).run()"
                              :class="{ 'is-active': editor.isActive('heading', { level: 1 }) }"
            >
              <GhostButton>
                <Heading1Icon />
                Заголовок 1
              </GhostButton>
            </DropdownMenuItem>
            <DropdownMenuItem @click="editor.chain().focus().toggleHeading({ level: 2 }).run()"
                              :class="{ 'is-active': editor.isActive('heading', { level: 2 }) }"
            >
              <GhostButton>
                <Heading2Icon />
                Заголовок 2
              </GhostButton>
            </DropdownMenuItem>
            <DropdownMenuItem @click="editor.chain().focus().toggleHeading({ level: 3 }).run()"
                              :class="{ 'is-active': editor.isActive('heading', { level: 3 }) }"
            >
              <GhostButton>
                <Heading3Icon />
                Заголовок 3
              </GhostButton>
            </DropdownMenuItem>
            <DropdownMenuItem @click="editor.chain().focus().toggleHeading({ level: 4 }).run()"
                              :class="{ 'is-active': editor.isActive('heading', { level: 4 }) }"
            >
              <GhostButton>
                <Heading4Icon />
                Заголовок 4
              </GhostButton>
            </DropdownMenuItem>
            <DropdownMenuItem @click="editor.chain().focus().toggleHeading({ level: 5 }).run()"
                              :class="{ 'is-active': editor.isActive('heading', { level: 5 }) }"
            >
              <GhostButton>
                <Heading5Icon />
                Заголовок 5
              </GhostButton>
            </DropdownMenuItem>
          </DropdownMenuGroup>
        </DropdownMenuContent>
      </DropdownMenu>
      
      <GhostButton color="zinc"
                   @click.prevent="editor.chain().focus().toggleBold().run()"
                   :class="{ 'is-active': editor.isActive('bold') }">
        <BoldIcon />
      </GhostButton>
      <GhostButton color="zinc"
                   @click.prevent="editor.chain().focus().toggleItalic().run()"
                   :class="{ 'is-active': editor.isActive('italic') }"
      >
        <ItalicIcon />
      </GhostButton>
      <GhostButton color="zinc"
                   @click.prevent="editor.chain().focus().toggleStrike().run()"
                   :class="{ 'is-active': editor.isActive('strike') }"
      >
        <StrikethroughIcon />
      </GhostButton>
      <GhostButton color="zinc"
                   @click.prevent="setLink"
                   :class="{ 'is-active': editor.isActive('link') }"
      >
        <LinkIcon />
      </GhostButton>
      <GhostButton color="zinc"
                   @click.prevent="editor.chain().focus().toggleCode().run()"
                   :class="{ 'is-active': editor.isActive('code') }"
      >
        <CodeIcon />
      </GhostButton>
      <GhostButton color="zinc"
                   @click.prevent="editor.chain().focus().toggleCodeBlock().run()"
                   :class="{ 'is-active': editor.isActive('codeBlock') }"
      >
        <Code2Icon />
      </GhostButton>
      <GhostButton color="zinc"
                   @click.prevent="editor.chain().focus().unsetAllMarks().run()"
      >
        <BrushCleaningIcon />
      </GhostButton>
      
      <Separator orientation="vertical" />

      <GhostButton color="zinc"
                   @click.prevent="editor.chain().focus().toggleBulletList().run()"
                   :class="{ 'is-active': editor.isActive('bulletList') }"
      >
        <ListIcon />
      </GhostButton>
      <GhostButton color="zinc"
                   @click.prevent="editor.chain().focus().toggleOrderedList().run()"
                   :class="{ 'is-active': editor.isActive('orderedList') }"
      >
        <ListOrderedIcon />
      </GhostButton>
      <GhostButton color="zinc"
                   @click.prevent="editor.chain().focus().toggleTaskList().run()"
                   :class="{ 'is-active': editor.isActive('taskList') }"
      >
        <ListChecksIcon />
      </GhostButton>

    </div>
    <EditorContent :editor="editor" class="p-4" />
  </div>
</template>

<style scoped>
</style>