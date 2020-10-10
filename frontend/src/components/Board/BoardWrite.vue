<template>
  <div class="editor">
    <b-form-input v-model="title" placeholder="Enter Title"></b-form-input>
    <b-form-input v-model="author" placeholder="Enter Author"></b-form-input>
    <b-form-input v-model="category" placeholder="Enter Category"></b-form-input>
    <BoardEditor ref="editor" @onEditorContent="onEditorContent(... arguments)" />
    <b-btn-group>
      <b-button variant="outline-primary" v-on:click="onClickConfirm">등록</b-button>
      <b-button variant="outline-warning" v-on:click="onClickCancel">취소</b-button>
    </b-btn-group>
  </div>
</template>

<script>
import BoardEditor from './BoardEditor'

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
      category: ''
    }
  },
  components: {
    BoardEditor: BoardEditor
  },
  methods: {
    onClickConfirm () {
      this.$refs.editor.getHTML()
      console.log(this.editorContent)

      this.$http.post(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}`,
        {
          author: this.author,
          title: this.title,
          content: this.editorContent,
          category: this.category,
          tags: []
        })
        .then((result) => {
          console.log(result)
        })
    },
    onClickCancel () {
      console.log(this.editorContent)
    },
    onEditorContent (editorContent) {
      this.editorContent = editorContent
    }
  }
}
</script>
