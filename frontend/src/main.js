// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.

import Vue from 'vue'

import './plugins/axios'
import App from './App'
import router from './router'
import BootstrapVue from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import 'expose-loader?$!expose-loader?jQuery!jquery'
import TreeView from 'vue-json-tree-view'
import InputTag from 'vue-input-tag'
import '@/fontAwesomeIcon.js' // fontAwesomeIcon.js 불러옴
import VueCookie from 'vue-cookie'

Vue.use(VueCookie)
Vue.use(TreeView)
Vue.use(BootstrapVue)

Vue.component('input-tag', InputTag)

Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  render: h => h(App),
  components: { App },
  template: '<App/>'
})
