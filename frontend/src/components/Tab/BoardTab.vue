<template>
  <b-tab v-bind:active="activeState()" v-on:click="onClick()">
    <template #title>
      <div style="font-size: smaller;">{{boardTitle}}</div>
    </template>

    <BoardList :category=category :title=boardTitle :boardId=boardId v-if="activeState()" />
  </b-tab>
</template>

<script>
import BoardList from '../Board/BoardList'
import _ from 'lodash'

export default {
  name: 'BoardTab',
  props: {
    boardTitle: {
      type: String,
      default: ''
    },
    boardId: {
      type: String,
      default: ''
    },
    category: {
      type: String,
      default: ''
    }
  },
  components: {
    BoardList: BoardList
  },
  methods: {
    activeState () {
      return _.isEqual(this.boardId, this.$route.query.boardId)
    },
    onClick () {
      var path = `/${this.category}?boardId=${this.boardId}`
      if (this.$router.currentRoute.fullPath !== path) {
        this.$router.push(path)
      }
    }
  }
}
</script>
