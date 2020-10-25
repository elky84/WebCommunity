<template>
  <div>
    <form @submit.stop.prevent="search">
        <div class="form-inline">
          <input type="text" class="col-md-2 form-control form-control-sm" v-model="searchProtocol.author" placeholder="작성자 (like 검색)">
          <input type="text" class="col-md-2 form-control form-control-sm" v-model="searchProtocol.title" placeholder="제목 (like 검색)">
          <input type="text" class="col-md-2 form-control form-control-sm" v-model="searchProtocol.content" placeholder="내용 (like 검색)">
          <button type="submit" class="btn btn-sm btn-primary" style="margin-right: 5px">검색<i class="fa fa-sm fa-search"></i></button>&nbsp;
        </div>
    </form>
  </div>
</template>

<script>
const SEARCH_PROTOCOL = {
  author: null,
  title: null,
  content: null
}
Object.freeze(SEARCH_PROTOCOL)
export default {
  data () {
    return {
      searchProtocol: Object.assign({}, SEARCH_PROTOCOL)
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
