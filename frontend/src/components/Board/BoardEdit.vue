<template>
  <tr>
    <td colspan="7">
      <BoardEditor ref="editor" @onEditorContent="onEditorContent(... arguments)"
        :originEditable="editMode" :originContent=article.content />

      <b-btn-group>
        <b-button variant="outline-primary" @click="onClickRecommend()">추천 {{article.recommend}}</b-button>
        <b-button variant="outline-warning" @click="onClickNotRecommend()">비추천 {{article.notRecommend}}</b-button>
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
import _ from 'lodash'

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
      comments: [],
      article: this.srcArticle
    }
  },
  watch: {
    editMode () {
      this.$refs.editor.setEditMode(this.editMode)
    }
  },
  mounted () {
    this.refreshComments()
  },
  methods: {
    refreshComments () {
      var vm = this
      this.$http.get(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${this.article.id}`,
        {
        })
        .then((result) => {
          vm.comments = result.data.datas
        })
    },
    onClickUpdate () {
      this.editMode = true
    },
    onClickUpdateConfirm () {
      this.editMode = false
      this.$refs.editor.getHTML()

      var vm = this
      this.$http.put(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${this.article.id}`,
        {
          id: this.article.id,
          author: this.article.author,
          title: this.article.title,
          content: this.editorContent,
          category: this.article.category,
          tags: this.article.tags
        })
        .then((result) => {
          if (result.data.data) {
            vm.onUpdate(result.data.data)
          }
        })
    },
    onClickDelete () {
      var vm = this
      this.$http.delete(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${this.article.id}`)
        .then((result) => {
          vm.$emit('refresh')
        })
    },
    onClickCancel () {
      this.$emit('close', this.article)
    },
    onEditorContent (editorContent) {
      this.editorContent = editorContent
    },
    onClickComment () {
      var vm = this
      this.$http.post(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${this.article.id}`,
        {
          author: this.commentAuthor,
          content: this.comment
        })
        .then((result) => {
          vm.refreshComments()
        })
    },
    onClickRecommend () {
      var vm = this
      this.$http.post(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${this.article.id}/Recommend`)
        .then((result) => {
          vm.onUpdate(result.data.data)
        })
    },
    onClickNotRecommend () {
      var vm = this
      this.$http.post(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${this.article.id}/NotRecommend`)
        .then((result) => {
          if (result.data.data) {
            vm.onUpdate(result.data.data)
          }
        })
    },
    onClickCommentRecommend (comment) {
      var vm = this
      this.$http.post(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${comment.id}/Recommend`)
        .then((result) => {
          if (result.data.data) {
            vm.onCommentUpdate(result.data.data)
          }
        })
    },
    onClickCommentNotRecommend (comment) {
      var vm = this
      this.$http.post(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${comment.id}/NotRecommend`)
        .then((result) => {
          if (result.data.data) {
            vm.onCommentUpdate(result.data.data)
          }
        })
    },
    onUpdate (newArticle) {
      this.article = newArticle
      this.$emit('update', this.article)
    },
    onCommentUpdate (newComment) {
      this.comments = _.flatMap(this.comments, function (comment) {
        if (comment.id === newComment.id) {
          return newComment
        } else {
          return comment
        }
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
