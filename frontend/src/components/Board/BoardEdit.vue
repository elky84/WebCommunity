<template>
  <tr class="edit-row">
    <td align="center" v-if="srcBoard">{{Board.id}}</td>

    <td align="left" v-if="srcBoard">{{Board.userId}}</td>
    <td v-else>
      <b-form-input id="userId" placeholder="userId" v-model="Board.userId"/>
    </td>

    <td align="center" v-if="srcBoard">{{Board.token}}</td>

    <td align="center">
      <select class="form-control form-control-sm" v-model="Board.grade" name="type">
        <option :value=null>등급 (전체)</option>
        <option v-for="(value, key) in Board_GRADE" :value="key" :key="value.text">{{value.text}}</option>
      </select>
    </td>
    <td align="center">
      <select class="form-control form-control-sm" v-model="Board.state" name="type">
        <option :value=null>상태 (전체)</option>
        <option v-for="(value, key) in Board_STATE" :value="key" :key="value.text">{{value.text}}</option>
      </select>
    </td>
    <td align="center">
      <div v-if="srcBoard">
        <button type="button" class="btn btn-success" @click.prevent="onClickUpdate(Board)">업데이트</button>
        <button type="button" class="btn btn-dark" @click.prevent="onClickTokenRefresh(Board)">토큰 재발급</button>
      </div>
      <div v-else>
        <button type="button" class="btn btn-primary" @click.prevent="onClickCreate(Board)">등록</button>
      </div>
    </td>
  </tr>
</template>

<script>
import * as _ from 'lodash'

import {
  Board_GRADE,
  Board_STATE
} from '@/common/constant/types'

export default {
  props: ['srcBoard'],
  data () {
    return {
      Board_GRADE: Board_GRADE,
      Board_STATE: Board_STATE,
      Board: this.srcBoard != null ? _.cloneDeep(this.srcBoard) : { userId: '', grade: 'Admin', state: 'Enable' }
    }
  },
  methods: {
    onClickUpdate (Board) {
      var vm = this
      this.$http.put(`${process.env.VUE_APP_URL_BACKEND}/auth/admin/Boards/${Board.id}`, Board).then((result) => {
        vm.$emit('update', _.cloneDeep(Board))
      })
    },
    onClickTokenRefresh (Board) {
      this.$bvModal.msgBoxConfirm('토큰을 재발급 하시겠습니까? 재발급 할 경우 기존의 플레이중이던 계정이, 접속해제됩니다.')
        .then(value => {
          if (value) {
            this.refreshToken(Board)
          }
        })
        .catch(err => {
          // An error occurred
          this.$bvModal.msgBoxOk('오류가 발생했습니다. ' + err)
        })
    },
    refreshToken (Board) {
      var vm = this
      this.$http.post(`${process.env.VUE_APP_URL_BACKEND}/auth/admin/Boards/refresh-token`, Board).then((result) => {
        Board = result.data
        vm.$emit('update', _.cloneDeep(Board))
      })
    },
    onClickCreate (Board) {
      this.$emit('create', Board)
    }
  }
}
</script>

<style scoped>
.edit-row {
  background: #EEEEEE;
}
.btn {
  margin-right: 5px;
  margin-bottom: 5px;
}
</style>
