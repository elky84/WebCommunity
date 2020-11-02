<template>
  <b-tab v-bind:active="activeState()" v-on:click="onClick()">
    <template #title>
      <strong>{{title}}</strong>
    </template>
    <b-card :title=title>
      <form>
        <div class="form-group row">
          <label for="inputPassword" class="col-sm-2 col-form-label">Password</label>
          <div class="col-sm-10">
            <input type="password" class="form-control" id="ProfileInputPassword" placeholder="Password" v-model="password" autocomplete="on">
          </div>
        </div>
        <div class="form-group row">
          <label for="inputReTypePassword" class="col-sm-2 col-form-label">ReType Password</label>
          <div class="col-sm-10">
            <input type="password" class="form-control" id="ProfileInputReTypePassword" placeholder="ReTypePassword" v-model="reTypePassword" autocomplete="on">
          </div>
        </div>
        <div class="form-group row">
          <label for="inputNickName" class="col-sm-2 col-form-label">NickName</label>
          <div class="col-sm-10">
            <input type="nickName" class="form-control" id="ProfileInputNickName" placeholder="NickName" v-model="nickName">
          </div>
        </div>
        <div class="form-group row">
          <label for="inputEmail" class="col-sm-2 col-form-label">Email</label>
          <div class="col-sm-10">
            <input type="email" class="form-control" id="ProfileInputEmail" placeholder="Email" v-model="email">
          </div>
        </div>
        <div class="form-group row">
          <div class="col-sm-10">
            <button type="submit" class="btn btn-primary" v-on:click="onClickSubmit">Update</button>
          </div>
        </div>
      </form>
    </b-card>
  </b-tab>
</template>

<script>
import _ from 'lodash'

export default {
  name: 'Profile',
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
      email: '',
      nickName: '',
      password: '',
      reTypePassword: ''
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
      this.$axios.put(`${process.env.VUE_APP_URL_BACKEND}/Auth/Account/Update`,
        {
          email: this.email,
          nickName: this.nickName,
          password: this.password
        })
        .then((result) => {
          vm.$buefy.dialog.alert({
            title: 'Alert',
            message: '프로필이 갱신되었습니다'
          })
        })
    }
  }
}
</script>
