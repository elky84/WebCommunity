<template>
  <b-tab v-bind:active="activeState()" v-on:click="onClick()">
    <template #title>
      <div style="font-size: smaller;" class="mb-2">{{title}}</div>
    </template>
    <b-card :title=title class="mb-2">
      <form class="form-group">
        <label for="Input UserId">UserId</label>
        <input type="userId" class="form-control" id="SignInInputEmail" aria-describedby="userIdHelp" placeholder="Enter UserId" v-model="userId">
        <small id="userIdHelp" class="form-text text-muted">We'll never share your userId with anyone else.</small>
        <label for="InputPassword1">Password</label>
        <input type="password" class="form-control" id="SignInInputPassword" placeholder="Password" v-model="password" autocomplete="on">
      </form>
      <button type="submit" class="btn btn-primary" v-on:click="onClickSubmit">Submit</button>
    </b-card>
  </b-tab>
</template>

<script>
import _ from 'lodash'

export default {
  name: 'SignIn',
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
      path: `/Account?mid=${this.mid}`,
      userId: '',
      password: ''
    }
  },
  methods: {
    activeState () {
      return _.isEqual(this.mid, this.$route.query.mid) || _.isEqual(null, this.$route.query.mid)
    },
    onClick () {
      if (this.$router.currentRoute.fullPath !== this.path) {
        this.$router.push(this.path)
      }
    },
    onClickSubmit () {
      var vm = this
      this.$axios.post('/Auth/Account/SignIn',
        {
          userId: this.userId,
          password: this.password
        })
        .then((result) => {
          this.setProfile(result.data.account)
          vm.$bvModal.msgBoxConfirm('로그인에 성공했습니다. 홈 화면으로 이동하시겠습니까?', {
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
