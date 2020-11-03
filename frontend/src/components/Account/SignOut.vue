<template>
  <b-tab v-bind:active="activeState()" v-on:click="onClick()">
    <template #title>
      <div style="font-size: smaller;" class="mb-2">{{title}}</div>
    </template>
    <b-card :title=title class="mb-2">
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
      var vm = this
      this.$axios.post(`${process.env.VUE_APP_URL_BACKEND}/Auth/Account/SignOut`,
        {
        })
        .then((result) => {
          this.$localStorage.set('profile', null)
          vm.$bvModal.msgBoxConfirm('로그아웃에 성공했습니다. 홈 화면으로 이동하시겠습니까?', {
            title: 'Alert'
          }).then(value => {
            if (value) {
              vm.$router.push('/')
            }
          })
        })
    }
  }
}
</script>
