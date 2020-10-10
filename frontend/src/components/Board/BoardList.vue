<template>
  <b-card :title=boardId>
    <BoardWrite @create="onCreate(... arguments)" :boardId="boardId" v-if="editMode"/>

    <b-button variant="outline-primary" v-on:click="onClickWrite">{{buttonText}}</b-button>
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
        <template v-for="(article) in articles">
          <tr class="cursor-pointer" :key="article.id" @click.prevent="onClickBoard(article)">
            <td align="center">{{article.category}}</td>
            <td align="left">{{article.tags}}</td>
            <td align="center">{{article.title}}</td>
            <td align="center">{{article.author}}</td>
            <td align="center">{{article.recommend}}</td>
            <td align="center">{{article.notRecommend}}</td>
            <td align="center">{{article.created}}</td>
          </tr>
          <BoardEdit :boardId="boardId" :srcArticle="article" v-if="article.edit" :key="article.id + '_edit'" />
        </template>
      </tbody>
    </table>

    <BoardSearch @searching="parentSearching(... arguments)" @create="onClickCreate(... arguments)" ref="searchForm"/>
    <b-pagination align="center" size="md" v-model="currentPage" :limit="limit" :total-rows="totalItems" :per-page="limit" @change="listing(... arguments)" />
  </b-card>
</template>

<script>
import BoardSearch from './BoardSearch'
import BoardWrite from './BoardWrite'
import BoardEdit from './BoardEdit'

import * as _ from 'lodash'

import Qs from 'qs'
import {
  BOARD_GRADE,
  BOARD_STATE
} from '@/common/constant/types'

export default {
  name: 'BoardList',
  props: {
    boardId: String
  },
  components: {
    BoardSearch: BoardSearch,
    BoardWrite: BoardWrite,
    BoardEdit: BoardEdit
  },
  data () {
    return {
      articles: [],
      BOARD_GRADE: BOARD_GRADE,
      BOARD_STATE: BOARD_STATE,
      currentPage: 1,
      viewPageCount: 1,
      totalItems: 0,
      limit: 20,
      searchData: {},
      sort: 'created',
      editMode: false
    }
  },
  mounted () {
    this.getBoards(this.searchData)
  },
  computed: {
    buttonText: function () {
      return this.editMode ? '닫기' : '글쓰기'
    }
  },
  methods: {
    getBoards (searchData) {
      var vm = this
      this.$http.get(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}`, {
        params: {
          offset: this.limit * (this.currentPage - 1),
          limit: this.limit,
          sort: this.sort
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
        this.viewPageCount = Math.ceil(result.data.total / this.limit)
        this.totalItems = result.data.total
        vm.articles = result.data.contents
      })
    },
    onUpdate (newBoard) {
      this.articles = _.flatMap(this.articles, function (article) {
        if (article.id === newBoard.id) {
          return newBoard
        } else {
          return article
        }
      })
    },
    parentSearching (searchData) {
      this.getBoards(searchData)
    },
    listing (page) {
      this.currentPage = page
      this.getBoards(this.searchData)
    },
    onClickWrite () {
      this.editMode = !this.editMode
    },
    onClickBoard (article) {
      this.$http.get(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${article.id}`, {})
        .then((result) => {
          article.content = result.data.data.content
          this.$set(article, 'edit', !article.edit)
        })
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
