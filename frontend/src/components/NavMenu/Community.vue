<template>
  <div>
    <b-tabs pills card vertical>
      <PageTab title="커뮤니티"/>
      <BoardTab boardTitle="자유 게시판" boardId='Free'/>
      <BoardTab boardTitle="유머 게시판" boardId='Humor'/>
      <BoardTab boardTitle="핫딜 게시판" boardId='Hotdeal'/>
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
  }
}
</script>
