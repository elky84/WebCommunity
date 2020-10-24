import Vue from 'vue'
import Router from 'vue-router'

import Home from '@/components/Home'

import News from '@/components/NavMenu/News'
import Community from '@/components/NavMenu/Community'
import PS from '@/components/NavMenu/PS'
import PC from '@/components/NavMenu/PC'
import Xbox from '@/components/NavMenu/Xbox'
import Switch from '@/components/NavMenu/Switch'
import Mobile from '@/components/NavMenu/Mobile'
import L10N from '@/components/NavMenu/L10N'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/News',
      name: 'News',
      component: News
    },
    {
      path: '/Community',
      name: 'Community',
      component: Community
    },
    {
      path: '/PS',
      name: 'PS',
      component: PS
    },
    {
      path: '/PC',
      name: 'PC',
      component: PC
    },
    {
      path: '/Xbox',
      name: 'Xbox',
      component: Xbox
    },
    {
      path: '/Switch',
      name: 'Switch',
      component: Switch
    },
    {
      path: '/Mobile',
      name: 'Mobile',
      component: Mobile
    },
    {
      path: '/L10N',
      name: 'L10N',
      component: L10N
    }
  ]
})
