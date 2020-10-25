<template>
  <div>
    <b-tabs pills card vertical>
      <PageTab :category="category" title="커뮤니티"/>
      <BoardTab :category="category" boardTitle="자유 게시판" boardId='Free'/>
      <BoardTab :category="category" boardTitle="유머 게시판" boardId='Humor'/>
      <BoardTab :category="category" boardTitle="핫딜 게시판" boardId='Hotdeal'/>
    </b-tabs>
  </div>
</template>

<script>
import BoardTab from '../Tab/BoardTab'
import PageTab from '../Tab/PageTab'

export default {
  name: 'Community',
  components: {
    BoardTab: BoardTab,
    PageTab: PageTab
  },
  data () {
    return {
      category: 'Community'
    }
  }
}
</script>
