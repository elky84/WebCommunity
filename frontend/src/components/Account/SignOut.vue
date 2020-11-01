<template>
  <b-tab v-bind:active="activeState()" v-on:click="onClick()">
    <template #title>
      <strong>{{title}}</strong>
    </template>
    <b-card :title=title>
      <button type="submit" class="btn btn-primary" v-on:click="onClickSubmit">Confirm</button>
    </b-card>
  </b-tab>
</template>

<script>
import _ from 'lodash'

export default {
  name: 'SignOut',
  props: {
    title: {
      type: String,
      default: ''
    },
    mid: {
      type: String,
      default: ''
    }
  },
  data () {
    return {
      path: `/Account?mid=${this.mid}`
    }
  },
  methods: {
    activeState () {
      return _.isEqual(this.mid, this.$route.query.mid)
    },
    onClick () {
      if (this.$router.currentRoute.fullPath !== this.path) {
        this.$router.push(this.path)
      }
    },
    onClickSubmit () {
      // var vm = this
      this.$axios.post(`${process.env.VUE_APP_URL_BACKEND}/Auth/Account/SignOut`,
        {
        })
        .then((result) => {
          console.log(result)
        })
    }
  }
}
</script>
