<template>
  <b-tab v-bind:active="activeState()" v-on:click="onClick()">
    <template #title>
      <div style="font-size: smaller;" class="mb-2">{{title}}</div>
    </template>
    <b-card>
      <form>
        <div class="form-group row">
          <label for="inputUserId" class="col-sm-2 col-form-label">UserId</label>
          <div class="col-sm-10">
            <input type="userId" class="form-control" id="SignUpInputUserId" placeholder="UserId" v-model="userId">
          </div>
        </div>
        <div class="form-group row">
          <label for="inputPassword" class="col-sm-2 col-form-label">Password</label>
          <div class="col-sm-10">
            <input type="password" class="form-control" id="SignUpInputPassword" placeholder="Password" v-model="password" autocomplete="on">
          </div>
        </div>
        <div class="form-group row">
          <label for="inputReTypePassword" class="col-sm-2 col-form-label">ReType Password</label>
          <div class="col-sm-10">
            <input type="password" class="form-control" id="SignUpInputReTypePassword" placeholder="ReTypePassword" v-model="reTypePassword" autocomplete="on">
          </div>
        </div>
        <div class="form-group row">
          <label for="inputNickName" class="col-sm-2 col-form-label">NickName</label>
          <div class="col-sm-10">
            <input type="nickName" class="form-control" id="SignUpInputNickName" placeholder="NickName" v-model="nickName">
          </div>
        </div>
        <div class="form-group row">
          <label for="inputEmail" class="col-sm-2 col-form-label">Email</label>
          <div class="col-sm-10">
            <input type="email" class="form-control" id="SignUpInputEmail" placeholder="Email" v-model="email">
          </div>
        </div>
        <div class="form-group row">
          <div class="col-sm-10">
            <button type="submit" class="btn btn-primary" v-on:click="onClickSubmit">Sign Up</button>
          </div>
        </div>
      </form>
    </b-card>
  </b-tab>
</template>

<script>
import _ from 'lodash'

export default {
  name: 'SignUp',
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
      this.$axios.post('/Auth/Account/SignUp',
        {
          userId: this.userId,
          email: this.email,
          nickName: this.nickName,
          password: this.password
        })
        .then((result) => {
          this.setProfile(result.data.account)
          vm.$bvModal.msgBoxConfirm('회원 가입에 성공했습니다. 홈 화면으로 이동하시겠습니까?', {
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
