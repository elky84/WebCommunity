<template>
  <b-tab v-bind:active="activeState()" v-on:click="onClick()">
    <template #title>
      <strong>{{title}}</strong>
    </template>
    <b-card :title=title>
    </b-card>
  </b-tab>
</template>

<script>
import _ from 'lodash'

export default {
  name: 'CategoryTab',
  props: {
    title: {
      type: String,
      default: ''
    },
    category: {
      type: String,
      default: ''
    }
  },
  methods: {
    activeState () {
      return _.isEqual(null, this.$route.query.boardId)
    },
    onClick () {
      this.$router.push(`/${this.category}`)
    }
  }
}
</script>
