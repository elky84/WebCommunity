<template>
  <b-card :title=title>
    <BoardWrite @refresh="onRefresh(... arguments)" @cancelWrite="onCancelWrite(... arguments)" :boardId="boardId" v-if="writeMode"/>

    <b-button variant="outline-primary" v-on:click="onClickWrite">{{buttonText}}</b-button>
    <b-button variant="outline-secondary" v-on:click="onClickRefresh">목록</b-button>

    <table class="table table-bordered">
      <thead class="thead-dark">
        <tr class="text-center">
          <th width="120px">
            <span class="header">분류</span>
          </th>
          <th width="200px">
            <span class="header">태그</span>
          </th>
          <th>
            <span class="header">제목</span>
          </th>
          <th width="150px">
            <span class="header">작성자</span>
          </th>
          <th width="150px">
            <span class="header">추천</span>
          </th>
          <th width="150px">
            <span class="header">비추천</span>
          </th>
          <th width="150px">
            <span class="header">시간</span>
          </th>
        </tr>
      </thead>
      <tbody>
        <template v-if="focusArticle">
            <tr class="cursor-pointer" :key="focusArticle.id + '_focus'" @click.prevent="onClickBoard(focusArticle)">
              <td align="center">{{focusArticle.category}}</td>
              <td align="left">
                <input-tag v-model="focusArticle.tags" :read-only=true v-if="hasTags(focusArticle)"></input-tag>
              </td>
              <td align="center">{{focusArticle.title}}</td>
              <td align="center">{{focusArticle.author}}</td>
              <td align="center">{{focusArticle.recommend}}</td>
              <td align="center">{{focusArticle.notRecommend}}</td>
              <td align="center">{{toDateString(focusArticle.created)}}</td>
            </tr>
            <BoardEdit @refresh="onRefresh(... arguments)" @close="onClose(... arguments)" @update="onUpdate(... arguments)" :boardId="boardId" :srcArticle="focusArticle" v-if="focusArticle.edit" :key="focusArticle.id + '_edit_focus'" />
        </template>
        <template v-for="(article) in articles">
          <tr class="cursor-pointer" :key="article.id" @click.prevent="onClickBoard(article)">
            <td align="center">{{article.category}}</td>
            <td align="left">
              <input-tag v-model="article.tags" :read-only=true v-if="hasTags(article)"></input-tag>
            </td>
            <td align="center">{{article.title}}</td>
            <td align="center">{{article.author}}</td>
            <td align="center">{{article.recommend}}</td>
            <td align="center">{{article.notRecommend}}</td>
            <td align="center">{{toDateString(article.created)}}</td>
          </tr>
          <BoardEdit @refresh="onRefresh(... arguments)" @close="onClose(... arguments)" @update="onUpdate(... arguments)" :boardId="boardId" :srcArticle="article" v-if="article.edit" :key="article.id + '_edit'" />
        </template>
      </tbody>
    </table>

    <BoardSearch @searching="parentSearching(... arguments)" @create="onClickCreate(... arguments)" ref="searchForm"/>
    <b-pagination-nav ref="pagination" align="center" size="md" :link-gen="linkGen" v-model="currentPage" :limit="limit" :number-of-pages="numberOfPages" :per-page="limit" @change="listing(... arguments)" />
  </b-card>
</template>

<script>
import BoardSearch from './BoardSearch'
import BoardWrite from './BoardWrite'
import BoardEdit from './BoardEdit'

import InputTag from 'vue-input-tag'
import * as _ from 'lodash'
import dayjs from 'dayjs'

import Qs from 'qs'
import {
  BOARD_GRADE,
  BOARD_STATE
} from '@/common/constant/types'

export default {
  name: 'BoardList',
  props: {
    boardId: String,
    title: String,
    category: String
  },
  components: {
    BoardSearch: BoardSearch,
    BoardWrite: BoardWrite,
    BoardEdit: BoardEdit,
    InputTag: InputTag
  },
  data () {
    return {
      articles: [],
      BOARD_GRADE: BOARD_GRADE,
      BOARD_STATE: BOARD_STATE,
      currentPage: 1,
      numberOfPages: 1,
      limit: 20,
      searchData: {},
      sort: 'Created',
      writeMode: false,
      articleId: null,
      focusArticle: null
    }
  },
  computed: {
    buttonText: function () {
      return this.writeMode ? '닫기' : '글쓰기'
    }
  },
  mounted () {
    this.listing(this.$route.query.page ? parseInt(this.$route.query.page) : 1)
    this.articleId = this.$route.query.articleId
    if (this.$route.query.boardId === this.boardId && this.articleId) {
      this.getFocusArticle()
    }
  },
  methods: {
    getArticles (searchData) {
      var vm = this
      this.$http.get(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}`, {
        params: {
          offset: this.limit * (this.currentPage - 1),
          limit: this.limit,
          sort: this.sort,
          asc: false
        },
        paramsSerializer (params) {
          return Qs.stringify($.extend(params, searchData), {
            skipNulls: true,
            allowDots: true,
            encode: false,
            arrayFormat: 'repeat'
          })
        }
      }).then((result) => {
        this.numberOfPages = Math.max(1, Math.ceil(result.data.total / this.limit))
        vm.articles = result.data.contents
      })
    },
    onUpdate (newArticle) {
      var vm = this
      this.articles = _.flatMap(this.articles, function (article) {
        if (article.id === newArticle.id) {
          vm.$set(newArticle, 'edit', article.edit)
          return newArticle
        } else {
          return article
        }
      })
    },
    parentSearching (searchData) {
      this.getArticles(searchData)
    },
    listing (page) {
      this.currentPage = page
      this.getArticles(this.searchData)
    },
    onClickWrite () {
      this.writeMode = !this.writeMode
    },
    onClickRefresh () {
      this.focusArticle = null
      this.onRefresh()
    },
    onClickBoard (article) {
      this.$http.get(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${article.id}`)
        .then((result) => {
          article.content = result.data.data.content
          this.$set(article, 'edit', !article.edit)

          if (this.$route.query.articleId !== article.id) {
            history.pushState({}, null, `/?boardId=${this.boardId}&articleId=${article.id}`)
          }
        })
    },
    getFocusArticle () {
      this.$http.get(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${this.articleId}`)
        .then((result) => {
          this.focusArticle = result.data.data
          if (this.focusArticle) {
            this.$set(this.focusArticle, 'edit', !this.focusArticle.edit)
          }
        })
    },
    onRefresh () {
      this.getArticles(this.searchData)
    },
    onClose (article) {
      var origin = _.find(this.articles, { id: article.id })
      this.$set(origin, 'edit', !origin.edit)
    },
    onCancelWrite () {
      this.writeMode = false
    },
    toDateString (date) {
      return dayjs(date).format('YYYY-MM-DD HH:mm:ss')
    },
    hasTags (article) {
      return !_.isEmpty(article.tags)
    },
    linkGen (pageNum) {
      return {
        path: '/Community',
        query: {
          page: pageNum,
          boardId: this.boardId,
          articleId: this.articleId
        }
      }
    }
  }
}
</script>

<style scoped>
div {
  word-break: break-word;
}
.arrow {
    display: inline-block;
    vertical-align: middle;
    width: 0;
    height: 0;
    margin-left: 5px;
    opacity: 0.66;
}
.arrow.asc {
    display: inline-block;
    border-left: 3px solid transparent;
    border-right: 3px solid transparent;
    border-bottom: 3px solid #FFFFFF;
}
.arrow.desc {
    display: inline-block;
    border-left: 3px solid transparent;
    border-right: 3px solid transparent;
    border-top: 3px solid #FFFFFF;
}
.badge {
  font-size: 1em !important;
  font-family: Arial !important;
}

.btn {
  margin: 5px;
}

</style>
