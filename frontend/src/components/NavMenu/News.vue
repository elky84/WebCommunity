<template>
  <b-tabs pills card vertical>
    <PageTab title="뉴스"/>
    <BoardTab boardTitle="PC 유저 정보" boardId='PcUserInfo'/>
    <BoardTab boardTitle="PS 유저 정보" boardId='PsUserInfo'/>
    <BoardTab boardTitle="XBOX 유저 정보" boardId='XboxUserInfo'/>
    <BoardTab boardTitle="Switch 유저 정보" boardId='SwitchUserInfo'/>
    <BoardTab boardTitle="모바일 유저 정보" boardId='MobileUserInfo'/>
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
  }
}
</script>
