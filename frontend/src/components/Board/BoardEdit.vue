<template>
  <tr>
    <td colspan="7">
      <BoardEditor ref="editor" @onEditorContent="onEditorContent(... arguments)"
        :originEditable="editMode" :originContent=srcArticle.content />
      <b-btn-group>
        <b-button variant="outline-primary" v-on:click="onClickUpdate" v-if="!editMode">수정</b-button>
        <b-button variant="outline-primary" v-on:click="onClickUpdateConfirm" v-if="editMode">수정완료</b-button>
        <b-button variant="outline-warning" v-on:click="onClickDelete">삭제</b-button>
        <b-button variant="outline-info" v-on:click="onClickCancel">닫기</b-button>
      </b-btn-group>
    </td>
  </tr>
</template>

<script>
import BoardEditor from './BoardEditor'

export default {
  name: 'BoardEdit',
  props: {
    boardId: String,
    srcArticle: Object
  },
  components: {
    BoardEditor: BoardEditor
  },
  data () {
    return {
      editMode: false,
      editorContent: ''
    }
  },
  watch: {
    editMode () {
      this.$refs.editor.setEditMode(this.editMode)
    }
  },
  methods: {
    onClickUpdate () {
      this.editMode = true
    },
    onClickUpdateConfirm () {
      this.editMode = false
      this.$refs.editor.getHTML()
      console.log(this.editorContent)

      this.$http.put(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${this.srcArticle.id}`,
        {
          id: this.srcArticle.id,
          author: this.srcArticle.author,
          title: this.srcArticle.title,
          content: this.editorContent,
          category: this.srcArticle.category,
          tags: this.srcArticle.tags
        })
        .then((result) => {
          console.log(result)
        })
    },
    onClickDelete () {
      this.$http.delete(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${this.srcArticle.id}`,
        {
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
