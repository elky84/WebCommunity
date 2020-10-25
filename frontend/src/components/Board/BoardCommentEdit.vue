<template>
  <b-row class="m-1">
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
</template>

<script>

export default {
  name: 'BoardCommentEdit',
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
      commentAuthor: ''
    }
  },
  methods: {
    onClickComment () {
      var vm = this
      this.$http.post(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${this.srcArticle.id}`,
        {
          originCommentId: this.srcComment === null ? null : this.srcComment.id,
          author: this.commentAuthor,
          content: this.comment
        })
        .then((result) => {
          vm.$emit('refreshComments')
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
