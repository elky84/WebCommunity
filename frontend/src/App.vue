<template>
  <div>
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
