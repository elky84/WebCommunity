<template>
  <b-tabs pills card vertical>
    <PageTab category="L10N" title="한글화"/>
    <BoardTab category="L10N" boardTitle="PC 한글화" boardId='PcL10N'/>
    <BoardTab category="L10N" boardTitle="PS 한글화" boardId='PsL10N'/>
    <BoardTab category="L10N" boardTitle="XBOX 한글화" boardId='XboxL10N'/>
    <BoardTab category="L10N" boardTitle="Switch 한글화" boardId='SwitchL10N'/>
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
  },
  data () {
    return {
      category: 'L10N'
    }
  }
}
</script>
