<template>
  <b-tabs pills card vertical>
    <PageTab :category="category" title="뉴스"/>
    <BoardTab :category="category" boardTitle="PC 유저 정보" boardId='PcUserInfo'/>
    <BoardTab :category="category" boardTitle="PS 유저 정보" boardId='PsUserInfo'/>
    <BoardTab :category="category" boardTitle="XBOX 유저 정보" boardId='XboxUserInfo'/>
    <BoardTab :category="category" boardTitle="Switch 유저 정보" boardId='SwitchUserInfo'/>
    <BoardTab :category="category" boardTitle="모바일 유저 정보" boardId='MobileUserInfo'/>
  </b-tabs>
</template>

<script>
import BoardTab from '../Tab/BoardTab'
import PageTab from '../Tab/PageTab'

export default {
  name: 'News',
  components: {
    BoardTab: BoardTab,
    PageTab: PageTab
  },
  data () {
    return {
      category: 'News'
    }
  }
}
</script>
