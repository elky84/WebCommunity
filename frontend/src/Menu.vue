<template>
  <b-navbar toggleable="lg" type="dark" variant="info">
    <b-navbar-brand b-link href="#/" :active="routePath.startsWith('/')">Game Friend</b-navbar-brand>

    <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

    <b-collapse id="nav-collapse" is-nav>
      <b-navbar-nav>
        <b-nav-item b-link href="#/News" :active="routePath.startsWith('/Webzine')">News</b-nav-item>
        <b-nav-item b-link href="#/PC" :active="routePath.startsWith('/PC')">PC</b-nav-item>
        <b-nav-item b-link href="#/PS" :active="routePath.startsWith('/PS')">PS</b-nav-item>
        <b-nav-item b-link href="#/Xbox" :active="routePath.startsWith('/Xbox')">XBOX</b-nav-item>
        <b-nav-item b-link href="#/Switch" :active="routePath.startsWith('/Switch')">Switch</b-nav-item>
        <b-nav-item b-link href="#/Mobile" :active="routePath.startsWith('/Mobile')">모바일</b-nav-item>
        <b-nav-item b-link href="#/Community" :active="routePath.startsWith('/Community')">커뮤니티</b-nav-item>
        <b-nav-item b-link href="#/L10N" :active="routePath.startsWith('/Community')">한글화</b-nav-item>
        <!-- <b-nav-item href="#">Link</b-nav-item>
        <b-nav-item href="#" disabled>Disabled</b-nav-item> -->
      </b-navbar-nav>

      <!-- Right aligned nav items -->
      <b-navbar-nav class="ml-auto">
        <b-nav-form>
          <b-form-input size="sm" class="mr-sm-2" placeholder="메뉴 검색"></b-form-input>
          <b-button size="sm" class="my-2 my-sm-0" type="submit">Search</b-button>
        </b-nav-form>

        <b-nav-item-dropdown right>
          <!-- Using 'button-content' slot -->
          <template v-slot:button-content>
            <em>User</em>
          </template>
          <b-dropdown-item href="#">Profile</b-dropdown-item>
          <b-dropdown-item href="#">Sign Out</b-dropdown-item>
        </b-nav-item-dropdown>
      </b-navbar-nav>
    </b-collapse>
  </b-navbar>
</template>

<script>
export default {
  name: 'Menu',
  data () {
    return {
    }
  },
  components: {
  },
  methods: {
  },
  computed: {
    routePath: function () {
      return this.$route.path
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
</style>
