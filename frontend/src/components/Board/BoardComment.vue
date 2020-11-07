<template>
  <b-row :class="rowColor(comment)">
    <b-col sm="2">
      <p>{{`작성자: ${comment.author}`}}</p>
    </b-col>
    <b-col fluid="lg">
      <label v-if="comment.originAuthor">{{comment.originAuthor}}에 대한 댓글</label>
      <BoardEditor ref="editor" @onEditorContent="onEditorContent(... arguments)"
        :originEditable="false" :originContent=comment.content />
      </b-col>
    <b-col sm="4">
      <b-row v-if="comment.status === 'Normal'">
        <b-button variant="outline-primary" class="m-1" @click="onClickRecommend()">추천 {{comment.recommend}}</b-button>
        <b-button variant="outline-warning" class="m-1" @click="onClickNotRecommend()">비추천 {{comment.notRecommend}}</b-button>
        <b-button variant="outline-info" class="m-1" @click="onClickReply()">댓글</b-button>
        <template v-if="getProfile() && getProfile().userId == comment.userId">
          <b-button variant="outline-danger" class="m-1" @click="onClickDelete()">삭제</b-button>
        </template>
      </b-row>
      <b-row>
        <p>{{toDateString()}}</p>
      </b-row>
    </b-col>
  </b-row>
</template>

<script>
import dayjs from 'dayjs'
import BoardEditor from './BoardEditor'

export default {
  name: 'BoardComment',
  components: {
    BoardEditor: BoardEditor
  },
  props: {
    comment: {
      type: Object,
      default: null
    },
    boardId: {
      type: String
    }
  },
  data () {
    return {
      editorContent: ''
    }
  },
  methods: {
    onClickRecommend () {
      var vm = this
      this.$axios.post(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${this.comment.id}/Recommend`)
        .then((result) => {
          if (result.data.data) {
            vm.onCommentUpdate(result.data.data)
          }
        })
    },
    onClickNotRecommend () {
      var vm = this
      this.$axios.post(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${this.comment.id}/NotRecommend`)
        .then((result) => {
          vm.onCommentUpdate(result.data.data)
        })
    },
    onClickReply () {
      this.$set(this.comment, 'reply', !this.comment.reply)
    },
    onClickDelete () {
      var vm = this
      this.$axios.delete(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${this.comment.id}`)
        .then((result) => {
          if (result.data.data) {
            vm.onCommentUpdate(result.data.data)
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
