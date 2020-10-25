<template>
  <tr class="cursor-pointer" @click.prevent="onClickBoard(srcArticle)">
    <td align="center">{{srcArticle.category}}</td>
    <td align="left">
      <input-tag v-model="srcArticle.tags" :read-only=true v-if="hasTags(srcArticle)"/>
    </td>
    <td align="center">{{srcArticle.title}}</td>
    <td align="center">{{srcArticle.author}}</td>
    <td align="center">{{srcArticle.recommend}}</td>
    <td align="center">{{srcArticle.notRecommend}}</td>
    <td align="center">{{toDateString(srcArticle.created)}}</td>
  </tr>
</template>

<script>
import dayjs from 'dayjs'
import InputTag from 'vue-input-tag'

import * as _ from 'lodash'

export default {
  name: 'BoardListArticle',
  props: {
    srcArticle: {
      type: Object,
      default: null
    },
    boardId: {
      type: String
    },
    postFix: {
      type: String
    }
  },
  components: {
    InputTag: InputTag
  },
  data () {
    return {
      article: null
    }
  },
  mounted () {
    this.article = this.srcArticle
  },
  methods: {
    toDateString (date) {
      return dayjs(date).format('YYYY-MM-DD HH:mm:ss')
    },
    hasTags (article) {
      return !_.isEmpty(article.tags)
    },
    onClickBoard () {
      this.$http.get(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${this.article.id}`)
        .then((result) => {
          this.article.content = result.data.data.content
          this.$set(this.article, 'edit', !this.article.edit)

          if (this.$route.query.articleId !== this.article.id) {
            history.pushState({}, null, `/?boardId=${this.boardId}&articleId=${this.article.id}`)
          }
        })
    }
  }
}
</script>
