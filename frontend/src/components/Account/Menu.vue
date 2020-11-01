<template>
  <div>
    <b-tabs pills card vertical>
      <Profile mid="Profile" title="Profile"/>
      <SignIn mid="SignIn" title="SignIn"/>
      <SignUp mid="SignUp" title="SignUp"/>
      <SignOut mid="SignOut" title="SignOut"/>
    </b-tabs>
  </div>
</template>

<script>
import Profile from './Profile'
import SignIn from './SignIn'
import SignUp from './SignUp'
import SignOut from './SignOut'

export default {
  name: 'Account',
  components: {
    Profile: Profile,
    SignIn: SignIn,
    SignUp: SignUp,
    SignOut: SignOut
  }
}
</script>
