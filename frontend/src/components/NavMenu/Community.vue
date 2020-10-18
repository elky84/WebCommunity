<template>
  <div>
    <b-card no-body>
      <b-tabs pills card vertical>
        <b-tab title="Free" v-bind:active="activeState('Free')" v-on:click="onClick('Free')">
          <BoardList boardId="Free" v-if="activeState('Free')"/>
        </b-tab>
        <b-tab title="Humor" v-bind:active="activeState('Humor')" v-on:click="onClick('Humor')">
          <BoardList boardId="Humor" v-if="activeState('Humor')"/>
        </b-tab>
      </b-tabs>
    </b-card>
  </div>
</template>

<script>
import BoardList from '../Board/BoardList'

export default {
  name: 'Community',
  data () {
    return {
      boardId: null
    }
  },
  components: {
    BoardList: BoardList
  },
  mounted () {
    this.boardId = this.$route.query.boardId
  },
  methods: {
    activeState (boardId) {
      return this.boardId === boardId
    },
    onClick (boardId) {
      this.$route.query.boardId = boardId
      this.boardId = boardId
    }
  }
}
</script>
