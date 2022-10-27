<template>
  <div class="editor">
    <b-form-input v-model="title" placeholder="Enter Title"></b-form-input>
    <b-form-input v-model="category" placeholder="Enter Category"></b-form-input>
    <input-tag v-model="tags"></input-tag>
    <b-form-input v-model="source" placeholder="Enter Source"></b-form-input>
    <BoardEditor ref="editor" @onEditorContent="onEditorContent(... arguments)" />
    <b-btn-group>
      <b-button variant="outline-primary" v-on:click="onClickConfirm">등록</b-button>
      <b-button variant="outline-warning" v-on:click="onClickCancel">취소</b-button>
    </b-btn-group>
  </div>
</template>

<script>
import BoardEditor from './BoardEditor'
import InputTag from 'vue-input-tag'

export default {
  name: 'BoardWrite',
  props: {
    boardId: String
  },
  data () {
    return {
      editorContent: '',
      title: '',
      author: '',
      category: '',
      tags: [],
      source: ''
    }
  },
  components: {
    BoardEditor: BoardEditor,
    InputTag: InputTag
  },
  methods: {
    onClickConfirm () {
      var vm = this
      this.$refs.editor.getHTML()

      this.$axios.post(`/Board/${this.boardId}`,
        {
          title: this.title,
          content: this.editorContent,
          category: this.category,
          tags: this.tags,
          source: this.source
        })
        .then((result) => {
          vm.$emit('new', result.data.data)
          vm.$emit('cancelWrite')
        })
    },
    onClickCancel () {
      this.$emit('cancelWrite')
    },
    onEditorContent (editorContent) {
      this.editorContent = editorContent
    }
  }
}
</script>
