<template>
  <tr>
    <td colspan="7">
      <div v-if="editMode">
        <b-form-input v-model="article.title" placeholder="Enter Title"></b-form-input>
        <b-form-input v-model="article.author" placeholder="Enter Author"></b-form-input>
        <b-form-input v-model="article.category" placeholder="Enter Category"></b-form-input>
        <input-tag v-model="article.tags"></input-tag>
      </div>

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

      <div v-for="(comment) in comments" :key="comment.id">
        <b-row :class="rowColor(comment)">
          <b-col sm="2">
            <label>{{comment.author}}</label>
          </b-col>
          <b-col sm="6">
            <label v-if="comment.originAuthor">{{comment.originAuthor}}에 대한 댓글</label>
            <b-form-input :value="comment.content" :readonly="true"></b-form-input>
          </b-col>
          <b-col>
            <b-button variant="outline-primary" @click="onClickCommentRecommend(comment)">추천 {{comment.recommend}}</b-button>
            <b-button variant="outline-warning" @click="onClickCommentNotRecommend(comment)">비추천 {{comment.notRecommend}}</b-button>
            <b-button variant="outline-info" @click="onClickCommentReply(comment)">댓글</b-button>
          </b-col>
          <b-col>
            <label>{{toDateString(comment.created)}}</label>
          </b-col>
        </b-row>
        <BoardCommentEdit v-if="comment.reply" :srcComment="comment" :boardId="boardId" :srcArticle="srcArticle" @refreshComments="refreshComments(... arguments)" />
      </div>

      <BoardCommentEdit :boardId="boardId" :srcArticle="srcArticle" @refreshComments="refreshComments(... arguments)" />
    </td>
  </tr>
</template>

<script>
import BoardEditor from './BoardEditor'
import BoardCommentEdit from './BoardCommentEdit'

import _ from 'lodash'
import dayjs from 'dayjs'

export default {
  name: 'BoardEdit',
  props: {
    boardId: String,
    srcArticle: Object
  },
  components: {
    BoardEditor: BoardEditor,
    BoardCommentEdit: BoardCommentEdit
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
    onClickCommentReply (comment) {
      this.$set(comment, 'reply', !comment.reply)
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
    },
    toDateString (date) {
      return dayjs(date).format('YYYY-MM-DD HH:mm:ss')
    },
    rowColor (comment) {
      return comment.id === comment.commentId ? 'bg-dark' : 'bg-secondary'
    }
  }
}
</script>

<style scoped>

</style>
