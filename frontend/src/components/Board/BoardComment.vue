<template>
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
</template>

<script>
import dayjs from 'dayjs'

export default {
  name: 'BoardComment',
  props: {
    comment: {
      type: Object,
      default: null
    },
    boardId: {
      type: String
    }
  },
  methods: {
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
    onCommentUpdate (newComment) {
      this.comment = newComment
    },
    rowColor (comment) {
      return comment.id === comment.commentId ? 'border border-primary' : 'border border-warning'
    },
    toDateString (date) {
      return dayjs(date).format('YYYY-MM-DD HH:mm:ss')
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
