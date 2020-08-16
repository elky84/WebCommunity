<template>
  <b-card title="계정">
    <BoardSearch @searching="parentSearching(... arguments)" @create="onClickCreate(... arguments)" ref="searchForm"/>
    <BoardEdit @create="onCreate(... arguments)" :srcBoard="null" ref="editForm" id="editForm" v-if="editMode"/>

    <table class="table table-bordered">
      <thead class="thead-dark">
        <tr class="text-center">
          <th width="120px" v-on:click="sortBy('id')">
            <span class="header">Id</span>
            <span class="arrow" :class="toArrow('id')"/>
          </th>
          <th width="200px" v-on:click="sortBy('userId')">
            <span class="header">UserId</span>
            <span class="arrow" :class="toArrow('userId')"/>
          </th>
          <th v-on:click="sortBy('token')">
            <span class="header">Token</span>
            <span class="arrow" :class="toArrow('token')"/>
          </th>
          <th width="150px" v-on:click="sortBy('grade')">
            <span class="header">Grade</span>
            <span class="arrow" :class="toArrow('grade')"/>
          </th>
          <th width="150px" v-on:click="sortBy('state')">
            <span class="header">State</span>
            <span class="arrow" :class="toArrow('state')"/>
          </th>
          <th width="180px">
            <span class="header">Action</span>
          </th>
        </tr>
      </thead>
      <tbody>
        <template v-for="(Board) in Boards">
          <tr class="cursor-pointer" :key="Board.id">
            <td align="center">{{Board.id}}</td>
            <td align="left">{{Board.userId}}</td>
            <td align="center">{{Board.token}}</td>
            <td align="center">
              <span class="badge badge-pill" v-bind:class=Board_GRADE[Board.grade].label>
                {{Board_GRADE[Board.grade].text}}
              </span>
            </td>
            <td align="center">
              <span class="badge badge-pill" v-bind:class=Board_STATE[Board.state].label>
                {{Board_STATE[Board.state].text}}
              </span>
            </td>
            <td align="center">
              <button style="margin-right:5px" type="button" class="btn btn-primary" @click.prevent="onClickEdit(Board)">편집</button>
              <button style="margin-right:5px" type="button" class="btn btn-danger" @click.prevent="onClickDelete(Board)">삭제</button>
            </td>
          </tr>
          <BoardEdit @update="onUpdate(... arguments)" :srcBoard="Board" v-if="Board.edit" :key="Board.id + '_edit'"/>
        </template>
      </tbody>
    </table>

    <b-pagination align="center" size="md" v-model="currentPage" :limit="limit" :total-rows="totalItems" :per-page="limit" @change="listing(... arguments)" />
  </b-card>
</template>

<script>
import BoardSearch from './BoardSearch'
import BoardEdit from './BoardEdit'
import * as _ from 'lodash'

import Qs from 'qs'
import {
  Board_GRADE,
  Board_STATE
} from '@/common/constant/types'
export default {
  name: 'BoardList',
  components: {
    BoardSearch: BoardSearch,
    BoardEdit: BoardEdit
  },
  data () {
    return {
      Boards: [],
      Board_GRADE: Board_GRADE,
      Board_STATE: Board_STATE,
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
      this.$http.get(`${process.env.VUE_APP_URL_BACKEND}/auth/admin/Boards`, {
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
    },
    onClickEdit (Board) {
      this.$set(Board, 'edit', !Board.edit)
    },
    onClickDelete (Board) {
      this.$bvModal.msgBoxConfirm('계정을 삭제하시겠습니까? 영구적으로 삭제되어 복구가 불가능합니다. 임시적인 제제를 원하실 경우, 삭제 대신 상태를 바꿔주세요.')
        .then(value => {
          if (value) {
            this.delete(Board)
          }
        })
        .catch(err => {
          // An error occurred
          this.$bvModal.msgBoxOk('오류가 발생했습니다. ' + err)
        })
    },
    delete (Board) {
      var vm = this
      this.$http.delete(`${process.env.VUE_APP_URL_BACKEND}/auth/admin/Boards/${Board.id}`).then((result) => {
        _.remove(vm.Boards, function (a) {
          return a.id === Board.id
        })

        vm.Boards = Object.assign({}, vm.Boards)
        this.$bvModal.msgBoxOk('계정이 삭제되었습니다')
      })
    },
    onClickCreate () {
      this.editMode = !this.editMode
    },
    onCreate (Board) {
      this.$http.post(`${process.env.VUE_APP_URL_BACKEND}/auth/admin/Boards`, Board).then((result) => {
        this.$bvModal.msgBoxOk('계정이 추가 되었습니다.')
        this.parentSearching(null)
      }).catch(error => {
        this.$bvModal.msgBoxOk(`계정 추가에 실패했습니다. [${error.response.data.ResultCode}] Detail [${error.response.data.Detail}]`)
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
</style>
