<template>
  <b-row class="m-1">
    <b-col sm="6">
      <b-form-textarea v-model="comment" placeholder="Enter Comment"
        rows="3" max-rows="10"></b-form-textarea>
    </b-col>
    <b-col>
      <b-button variant="outline-primary" v-on:click="onClickComment">{{commentText}}</b-button>
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
      comment: ''
    }
  },
  computed: {
    commentText: function () {
      return this.srcComment != null ? `${this.srcComment.author}에 대한 댓글 쓰기` : '댓글 쓰기'
    }
  },
  methods: {
    onClickComment () {
      var vm = this
      this.$axios.post(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${this.srcArticle.id}`,
        {
          originCommentId: this.srcComment === null ? null : this.srcComment.id,
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
