<template>
  <b-card :title=BoardId>
    <BoardSearch @searching="parentSearching(... arguments)" @create="onClickCreate(... arguments)" ref="searchForm"/>
    <BoardEdit @create="onCreate(... arguments)" :srcBoard="null" ref="editForm" id="editForm" v-if="editMode"/>

    <table class="table table-bordered">
      <thead class="thead-dark">
        <tr class="text-center">
          <th width="120px" v-on:click="sortBy('cateogry')">
            <span class="header">분류</span>
            <span class="arrow" :class="toArrow('cateogry')"/>
          </th>
          <th width="200px" v-on:click="sortBy('tags')">
            <span class="header">태그</span>
            <span class="arrow" :class="toArrow('tags')"/>
          </th>
          <th v-on:click="sortBy('title')">
            <span class="header">제목</span>
            <span class="arrow" :class="toArrow('title')"/>
          </th>
          <th width="150px" v-on:click="sortBy('author')">
            <span class="header">작성자</span>
            <span class="arrow" :class="toArrow('author')"/>
          </th>
          <th width="150px" v-on:click="sortBy('recommend')">
            <span class="header">추천</span>
            <span class="arrow" :class="toArrow('recommend')"/>
          </th>
          <th width="150px" v-on:click="sortBy('notRecommend')">
            <span class="header">비추천</span>
            <span class="arrow" :class="toArrow('notRecommend')"/>
          </th>
          <th width="150px" v-on:click="sortBy('created')">
            <span class="header">시간</span>
            <span class="arrow" :class="toArrow('created')"/>
          </th>
        </tr>
      </thead>
      <tbody>
        <template v-for="(Board) in Boards">
          <tr class="cursor-pointer" :key="Board.id">
            <td align="center">{{Board.category}}</td>
            <td align="left">{{Board.tags}}</td>
            <td align="center">{{Board.title}}</td>
            <td align="center">{{Board.author}}</td>
            <td align="center">{{Board.recommend}}</td>
            <td align="center">{{Board.notRecommend}}</td>
            <td align="center">{{Board.created}}</td>
          </tr>
        </template>
      </tbody>
    </table>

    <b-pagination align="center" size="md" v-model="currentPage" :limit="limit" :total-rows="totalItems" :per-page="limit" @change="listing(... arguments)" />
  </b-card>
</template>

<script>
import BoardSearch from './BoardSearch'

import * as _ from 'lodash'

import Qs from 'qs'
import {
  BOARD_GRADE,
  BOARD_STATE
} from '@/common/constant/types'

export default {
  name: 'BoardList',
  props: {
    BoardId: String
  },
  components: {
    BoardSearch: BoardSearch
  },
  data () {
    return {
      Boards: [],
      BOARD_GRADE: BOARD_GRADE,
      BOARD_STATE: BOARD_STATE,
      currentPage: 1,
      viewPageCount: 1,
      totalItems: 0,
      limit: 20,
      searchData: {},
      sort: null,
      orderState: {
        id: null,
        userId: null,
        token: null,
        grade: null,
        state: null
      },
      tempGrade: 'Admin',
      tempState: 'Enable',
      editMode: false
    }
  },
  mounted () {
    this.getBoards(this.searchData)
  },
  methods: {
    getBoards (searchData) {
      var vm = this
      this.$http.get(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.BoardId}`, {
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
        vm.Boards = result.data.contents
      })
    },
    onUpdate (newBoard) {
      this.Boards = _.flatMap(this.Boards, function (Board) {
        if (Board.id === newBoard.id) {
          return newBoard
        } else {
          return Board
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
    sortBy (key) {
      this.orderState[key] = this.orderState[key] == null ? 'desc' : this.orderState[key] === 'asc' ? null : 'asc'
      for (var orderKey in this.orderState) {
        if (key !== orderKey) {
          this.orderState[orderKey] = null
        }
      }

      if (this.orderState[key] != null) {
        this.sort = key + ':' + this.orderState[key]
      } else {
        this.sort = null
      }

      this.getBoards(this.searchData)
    },
    toArrow (key) {
      if (this.orderState[key] == null) {
        return 'none'
      }
      return this.orderState[key]
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
</style>
