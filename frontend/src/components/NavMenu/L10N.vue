<template>
  <b-tabs pills card vertical>
    <PageTab title="한글화"/>
    <BoardTab boardTitle="PC 한글화" boardId='PcL10N'/>
    <BoardTab boardTitle="PS 한글화" boardId='PsL10N'/>
    <BoardTab boardTitle="XBOX 한글화" boardId='XboxL10N'/>
    <BoardTab boardTitle="Switch 한글화" boardId='SwitchL10N'/>
  </b-tabs>
</template>

<script>
import BoardTab from '../Tab/BoardTab'
import PageTab from '../Tab/PageTab'

export default {
  name: 'L10N',
  components: {
    BoardTab: BoardTab,
    PageTab: PageTab
  }
}
</script>
