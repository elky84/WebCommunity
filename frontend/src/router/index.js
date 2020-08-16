import Vue from 'vue'
import Router from 'vue-router'

import Home from '@/components/Home'

import News from '@/components/NavMenu/News'

// import Stage from '@/components/Stage/Stage'
// import StageList from '@/components/Stage/StageList'
// import BattleEdit from '@/components/Stage/BattleEdit'

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
    }
    // {
    //   path: '/Stage',
    //   name: 'Stage',
    //   component: Stage,
    //   children: [
    //     {
    //       path: 'Search',
    //       name: 'Search',
    //       component: StageList
    //     },
    //     {
    //       path: 'BattleEdit',
    //       name: 'BattleEdit',
    //       component: BattleEdit
    //     }
    //   ]
    // }
  ]
})
