<template>
  <tr>
    <td colspan="7">
      <BoardEditor ref="editor" @onEditorContent="onEditorContent(... arguments)"
        :originEditable="editMode" :originContent=srcArticle.content />

      <b-btn-group>
        <b-button variant="outline-primary" @click="onClickRecommend()">추천 {{srcArticle.recommend}}</b-button>
        <b-button variant="outline-warning" @click="onClickNotRecommend()">비추천 {{srcArticle.notRecommend}}</b-button>
        <b-button variant="outline-primary" v-on:click="onClickUpdate" v-if="!editMode">수정</b-button>
        <b-button variant="outline-primary" v-on:click="onClickUpdateConfirm" v-if="editMode">수정완료</b-button>
        <b-button variant="outline-warning" v-on:click="onClickDelete">삭제</b-button>
        <b-button variant="outline-info" v-on:click="onClickCancel">닫기</b-button>
      </b-btn-group>

      <b-row v-for="(comment) in comments" :key="comment.id">
        <b-col sm="2">
          <label>{{comment.author}}</label>
        </b-col>
        <b-col sm="6">
          <b-form-input :value="comment.content" :readonly="true"></b-form-input>
        </b-col>
        <b-col>
          <b-button variant="outline-primary" @click="onClickCommentRecommend(comment)">추천 {{comment.recommend}}</b-button>
          <b-button variant="outline-warning" @click="onClickCommentNotRecommend(comment)">비추천 {{comment.notRecommend}}</b-button>
        </b-col>
      </b-row>

      <b-row>
        <b-col sm="2">
          <b-form-input v-model="commentAuthor" placeholder="Enter Author"></b-form-input>
        </b-col>
        <b-col sm="6">
          <b-form-input v-model="comment" placeholder="Enter Comment"></b-form-input>
        </b-col>
        <b-col>
          <b-button variant="outline-primary" v-on:click="onClickComment">댓글 쓰기</b-button>
        </b-col>
      </b-row>
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
      editorContent: '',
      comment: '',
      commentAuthor: '',
      comments: []
    }
  },
  watch: {
    editMode () {
      this.$refs.editor.setEditMode(this.editMode)
    }
  },
  mounted () {
    var vm = this
    this.$http.get(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${this.srcArticle.id}`,
      {
      })
      .then((result) => {
        console.log(result)
        vm.comments = result.data.datas
      })
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
    },
    onClickComment () {
      this.$http.post(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${this.srcArticle.id}`,
        {
          author: this.commentAuthor,
          content: this.comment
        })
        .then((result) => {
          console.log(result)
        })
    },
    onClickRecommend () {
      this.$http.post(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${this.srcArticle.id}/Recommend`)
        .then((result) => {
          console.log(result)
        })
    },
    onClickNotRecommend () {
      this.$http.post(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${this.srcArticle.id}/NotRecommend`)
        .then((result) => {
          console.log(result)
        })
    },
    onClickCommentRecommend (comment) {
      this.$http.post(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${comment.id}/Recommend`)
        .then((result) => {
          console.log(result)
        })
    },
    onClickCommentNotRecommend (comment) {
      this.$http.post(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${comment.id}/NotRecommend`)
        .then((result) => {
          console.log(result)
        })
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
