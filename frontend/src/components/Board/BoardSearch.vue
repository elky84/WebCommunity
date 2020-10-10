<template>
  <div>
    <form @submit.stop.prevent="search">
        <div class="form-inline">
          <input type="text" class="col-md-1 form-control form-control-sm" v-model="searchProtocol.userId" placeholder="UserId (like 검색)">
          <select class="col-md-1 form-control form-control-sm" v-model="searchProtocol.grade" name="type">
            <option :value=null>등급 (전체)</option>
            <option v-for="(value, key) in BOARD_GRADE" :value="key" :key="value.text">{{value.text}}</option>
          </select>
          <select class="col-md-1 form-control form-control-sm" v-model="searchProtocol.state" name="type">
            <option :value=null>상태 (전체)</option>
            <option v-for="(value, key) in BOARD_STATE" :value="key" :key="value.text">{{value.text}}</option>
          </select>
          <button type="submit" class="btn btn-sm btn-primary" style="margin-right: 5px">검색<i class="fa fa-sm fa-search"></i></button>&nbsp;
          <button type="button" class="btn btn-sm btn-secondary" style="margin-right: 5px" @click.prevent="reset">초기화 <i class="fa fa-sm fa-eraser"></i></button>
        </div>
    </form>
  </div>
</template>

<script>
import {
  BOARD_GRADE,
  BOARD_STATE
} from '@/common/constant/types'
const SEARCH_PROTOCOL = {
  userId: null,
  grade: null,
  state: null
}
Object.freeze(SEARCH_PROTOCOL)
export default {
  data () {
    return {
      searchProtocol: Object.assign({}, SEARCH_PROTOCOL),
      BOARD_GRADE: BOARD_GRADE,
      BOARD_STATE: BOARD_STATE
    }
  },
  methods: {
    search (e) {
      this.$emit('searching', this.searchProtocol)
    },
    reset () {
      this.searchProtocol = Object.assign({}, SEARCH_PROTOCOL)
      this.$emit('searching', this.searchProtocol)
    },
    create () {
      this.$emit('create')
    }
  }
}
</script>

<style scoped>
.form-control {
  margin-right: 3px;
  margin-bottom: 5px;
}
.form-inline {
  margin-bottom: 5px;
}
</style>
