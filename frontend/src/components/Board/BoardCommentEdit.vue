<template>
  <b-row class="m-1">
    <b-col sm="9">
      <BoardEditor ref="editor" @onEditorContent="onEditorContent(... arguments)"
        :originEditable="true" :originContent=comment />
    </b-col>
    <b-col>
      <b-button variant="outline-primary" v-on:click="onClickComment">{{commentText}}</b-button>
    </b-col>
  </b-row>
</template>

<script>
import BoardEditor from './BoardEditor'

export default {
  name: 'BoardCommentEdit',
  components: {
    BoardEditor: BoardEditor
  },
  props: {
    boardId: String,
    srcComment: {
      type: Object,
      default: null
    },
    srcArticle: {
      type: Object
    }
  },
  data () {
    return {
      comment: '',
      editorContent: ''
    }
  },
  mounted () {
    this.comment = this.srcComment != null ? this.srcComment.content : ''
  },
  computed: {
    commentText: function () {
      return this.srcComment != null ? `${this.srcComment.author}에 대한 댓글 쓰기` : '댓글 쓰기'
    }
  },
  methods: {
    onClickComment () {
      this.$refs.editor.getHTML()

      var vm = this
      this.$axios.post(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${this.srcArticle.id}`,
        {
          originCommentId: this.srcComment === null ? null : this.srcComment.id,
          content: this.editorContent
        })
        .then((result) => {
          vm.$emit('refreshComments')
        })
    },
    onEditorContent (editorContent) {
      console.log(editorContent)
      this.editorContent = editorContent
    }
  }
}
</script>

<style scoped>

label {
  display: inline-block;
  width: 140px;
  text-align: right;
}

</style>
