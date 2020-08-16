<template>
  <div>
    <!-- <img src="./assets/logo.png"> -->
    <Menu/>
    <router-view :key="$route.fullPath"></router-view>
    <b-card-footer align="center">Copyright 2020. Elky. All rights reserved.</b-card-footer>
  </div>
</template>

<script>
import Menu from './Menu'

export default {
  name: 'App',
  mounted () {
  },
  components: {
    Menu: Menu
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
