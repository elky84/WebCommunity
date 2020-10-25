<template>
  <b-tabs pills card vertical>
    <PageTab :category="category" title="PC"/>
    <CategoryTab title="정보 게시판"/>
    <BoardTab :category="category" boardTitle="PC 유저 정보" boardId='PcUserInfo'/>
    <BoardTab :category="category" boardTitle="핫딜 게시판" boardId='Hotdeal'/>
    <CategoryTab title="게임 게시판"/>
    <BoardTab :category="category" boardTitle="게임 이야기" boardId='PcGameTalk'/>
    <BoardTab :category="category" boardTitle="잡담 게시판" boardId='PcFreeTalk'/>
    <BoardTab :category="category" boardTitle="질문 게시판" boardId='PcQ&A'/>
    <BoardTab :category="category" boardTitle="리뷰 게시판" boardId='PcReview'/>
  </b-tabs>
</template>

<script>
import BoardTab from '../Tab/BoardTab'
import CategoryTab from '../Tab/CategoryTab'
import PageTab from '../Tab/PageTab'

export default {
  name: 'PC',
  components: {
    BoardTab: BoardTab,
    CategoryTab: CategoryTab,
    PageTab: PageTab
  },
  data () {
    return {
      category: 'PC'
    }
  }
}
</script>
