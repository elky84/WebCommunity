<template>
  <tr class="cursor-pointer" @click.prevent="onClickBoard(srcArticle)">
    <td align="center" :class="borderClass">{{srcArticle.category}}</td>
    <td align="left" :class="borderClass">
      <input-tag v-model="srcArticle.tags" :read-only=true v-if="hasTags(srcArticle)"/>
    </td>
    <td align="center" :class="borderClass">{{srcArticle.title}}</td>
    <td align="center" :class="borderClass">{{srcArticle.author}}</td>
    <td align="center" :class="borderClass">{{srcArticle.recommend}}/{{srcArticle.notRecommend}}</td>
    <td align="center" :class="borderClass">{{srcArticle.hit}}</td>
    <td align="center" :class="borderClass">{{toDateString(srcArticle.created)}}</td>
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
    category: {
      type: String
    },
    borderClass: {
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
      if (this.article.edit) {
        this.$set(this.article, 'edit', !this.article.edit)
        return
      }

      this.$axios.post(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${this.article.id}/Read`)
        .then((result) => {
          this.article.content = result.data.data.content
          this.article.source = result.data.data.source
          this.article.hit = result.data.data.hit
          this.$set(this.article, 'edit', !this.article.edit)

          if (this.$route.query.articleId !== this.article.id) {
            history.pushState({}, null, `#/${this.category}?boardId=${this.boardId}&articleId=${this.article.id}`)
          }
        })
    }
  }
}
</script>
