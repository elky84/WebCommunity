<template>
  <div class="editor">
    <Modal ref="ytmodal" @onConfirm="addCommand" />
    <editor-menu-bar :editor="editor" v-slot="{ commands, isActive }" v-if="editable">
      <div class="menubar">
        <button
          class="menubar__button"
          :class="{ 'is-active': isActive.bold() }"
          @click="commands.bold"
        >
          <font-awesome-icon :icon="['fas', 'bold']" />
        </button>

        <button
          class="menubar__button"
          :class="{ 'is-active': isActive.italic() }"
          @click="commands.italic"
        >
          <font-awesome-icon :icon="['fas', 'italic']" />
        </button>

        <button
          class="menubar__button"
          :class="{ 'is-active': isActive.strike() }"
          @click="commands.strike"
        >
          <font-awesome-icon :icon="['fas', 'strikethrough']" />

        </button>

        <button
          class="menubar__button"
          :class="{ 'is-active': isActive.underline() }"
          @click="commands.underline"
        >
          <font-awesome-icon :icon="['fas', 'underline']" />

        </button>

        <button
          class="menubar__button"
          :class="{ 'is-active': isActive.code() }"
          @click="commands.code"
        >
          <font-awesome-icon :icon="['fas', 'code']" />

        </button>

        <button
          class="menubar__button"
          :class="{ 'is-active': isActive.paragraph() }"
          @click="commands.paragraph"
        >
          <font-awesome-icon :icon="['fas', 'paragraph']" />

        </button>

        <button
          class="menubar__button"
          :class="{ 'is-active': isActive.heading({ level: 1 }) }"
          @click="commands.heading({ level: 1 })"
        >
          H1
        </button>

        <button
          class="menubar__button"
          :class="{ 'is-active': isActive.heading({ level: 2 }) }"
          @click="commands.heading({ level: 2 })"
        >
          H2
        </button>

        <button
          class="menubar__button"
          :class="{ 'is-active': isActive.heading({ level: 3 }) }"
          @click="commands.heading({ level: 3 })"
        >
          H3
        </button>

        <button
          class="menubar__button"
          :class="{ 'is-active': isActive.bullet_list() }"
          @click="commands.bullet_list"
        >
          <font-awesome-icon :icon="['fas', 'list-ul']" />

        </button>

        <button
          class="menubar__button"
          :class="{ 'is-active': isActive.ordered_list() }"
          @click="commands.ordered_list"
        >
          <font-awesome-icon :icon="['fas', 'list-ol']" />

        </button>

        <button
          class="menubar__button"
          :class="{ 'is-active': isActive.blockquote() }"
          @click="commands.blockquote"
        >
          <font-awesome-icon :icon="['fas', 'quote-left']" />

        </button>

        <button
          class="menubar__button"
          :class="{ 'is-active': isActive.code_block() }"
          @click="commands.code_block"
        >
          <font-awesome-icon :icon="['fas', 'code']" />
        </button>

        <button
          class="menubar__button"
          @click="commands.horizontal_rule"
        >
          <font-awesome-icon :icon="['fas', 'grip-lines']" />
        </button>

        <button
          class="menubar__button"
          @click="commands.undo"
        >
          <font-awesome-icon :icon="['fas', 'undo']" />

        </button>

        <button
          class="menubar__button"
          @click="commands.redo"
        >
          <font-awesome-icon :icon="['fas', 'redo']" />

        </button>

        <button class="menubar__button" @click="openModal(commands.image);">
          <font-awesome-icon :icon="['fas', 'image']" />
        </button>

      </div>
    </editor-menu-bar>

    <editor-content class="editor__content" :editor="editor" />
  </div>
</template>

<script>
import { Editor, EditorContent, EditorMenuBar } from 'tiptap'
import Modal from './Modal'

import {
  Blockquote,
  CodeBlock,
  HardBreak,
  Heading,
  HorizontalRule,
  OrderedList,
  BulletList,
  ListItem,
  TodoItem,
  TodoList,
  Bold,
  Code,
  Italic,
  Link,
  Strike,
  Underline,
  History,
  Image
} from 'tiptap-extensions'

export default {
  components: {
    EditorContent,
    EditorMenuBar,
    Modal
  },
  props: {
    originContent: {
      type: String,
      default: ''
    },
    originEditable: {
      type: Boolean,
      default: true
    }
  },
  data () {
    return {
      editor: new Editor({
        extensions: [
          new Blockquote(),
          new BulletList(),
          new CodeBlock(),
          new HardBreak(),
          new Heading({ levels: [1, 2, 3] }),
          new HorizontalRule(),
          new ListItem(),
          new OrderedList(),
          new TodoItem(),
          new TodoList(),
          new Link(),
          new Bold(),
          new Code(),
          new Italic(),
          new Strike(),
          new Underline(),
          new History(),
          new Image()
        ],
        content: this.originContent
      }),
      editable: this.originEditable
    }
  },
  mounted () {
    this.editable = this.originEditable
  },
  watch: {
    editable () {
      this.editor.setOptions({
        editable: this.editable
      })
    }
  },
  methods: {
    openModal (command) {
      this.$refs.ytmodal.showModal(command)
    },
    addCommand (data) {
      if (data.command !== null) {
        data.command(data.data)
      }
    },
    setContent () {
      this.editor.setContent(this.content)
    },
    getHTML () {
      this.content = this.editor.getHTML()
      console.log(this.content)
      this.$emit('onEditorContent', this.content)
    },
    setEditMode (editMode) {
      this.editable = editMode
    }
  },
  beforeDestroy () {
    this.editor.destroy()
  }
}
</script>

<style lang="css" src="../../css/tiptap.css"></style>
