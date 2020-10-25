<template>
  <b-row :class="rowColor(comment)">
    <b-col sm="2">
      <label>{{comment.author}}</label>
    </b-col>
    <b-col sm="5">
      <label v-if="comment.originAuthor">{{comment.originAuthor}}에 대한 댓글</label>
      <b-form-input :value="comment.content" :readonly="true"></b-form-input>
    </b-col>
    <b-col sm="3">
      <b-button variant="outline-primary" class="m-1" @click="onClickRecommend()">추천 {{comment.recommend}}</b-button>
      <b-button variant="outline-warning" class="m-1" @click="onClickNotRecommend()">비추천 {{comment.notRecommend}}</b-button>
      <b-button variant="outline-info" class="m-1" @click="onClickReply()">댓글</b-button>
      <b-button variant="outline-danger" class="m-1" @click="onClickDelete()">삭제</b-button>
    </b-col>
    <b-col>
      <label>{{toDateString()}}</label>
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
    onClickRecommend () {
      var vm = this
      this.$http.post(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${this.comment.id}/Recommend`)
        .then((result) => {
          if (result.data.data) {
            vm.onCommentUpdate(result.data.data)
          }
        })
    },
    onClickNotRecommend () {
      var vm = this
      this.$http.post(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${this.comment.id}/NotRecommend`)
        .then((result) => {
          vm.$emit('delete', this.comment)
        })
    },
    onClickReply () {
      this.$set(this.comment, 'reply', !this.comment.reply)
    },
    onClickDelete () {
      var vm = this
      this.$http.delete(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${this.comment.id}`)
        .then((result) => {
          if (result.data.data) {
            vm.$emit('delete', this.comment)
          }
        })
    },
    onCommentUpdate (newComment) {
      this.comment = newComment
    },
    rowColor () {
      return this.comment.id === this.comment.commentId ? 'border border-primary' : 'border border-warning'
    },
    toDateString () {
      return dayjs(this.comment.created).format('YYYY-MM-DD HH:mm:ss')
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
