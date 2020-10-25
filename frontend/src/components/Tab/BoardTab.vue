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
  watch: {
    editMode () {
      this.$refs.editor.setEditMode(this.editMode)
    }
  },
  methods: {
    activeState () {
      return _.isEqual(this.boardId, this.$route.query.boardId)
    },
    onClick () {
      this.$router.push(`/${this.category}?boardId=${this.boardId}`)
    }
  }
}
</script>
