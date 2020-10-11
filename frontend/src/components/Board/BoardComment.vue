<template v-for="(comment) in comments">
  <tr :v-if="comment">
    <td colspan="7">
      {{comment.content}}
    </td>
  </tr>
</template>

<script>

export default {
  name: 'BoardComment',
  props: {
    boardId: String,
    articleId: String
  },
  components: {
  },
  data () {
    return {
      comments: []
    }
  },
  mounted () {
    console.log(this.boardId)
    console.log(this.articleId)
    console.log(this.comments)

    var vm = this
    this.$http.get(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${this.articleId}`)
      .then((result) => {
        console.log(result)
        vm.comments = result.data.datas
      })
  }
}
</script>
