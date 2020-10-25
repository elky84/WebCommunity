<template>
  <b-tabs pills card vertical>
    <PageTab :category="category" title="플레이스테이션"/>
    <CategoryTab title="정보 게시판"/>
    <BoardTab :category="category" boardTitle="플스 유저 정보" boardId='PsUserInfo'/>
    <BoardTab :category="category" boardTitle="핫딜 게시판" boardId='Hotdeal'/>
    <CategoryTab title="게임 게시판"/>
    <BoardTab :category="category" boardTitle="게임 이야기" boardId='PsGameTalk'/>
    <BoardTab :category="category" boardTitle="잡담 게시판" boardId='PsFreeTalk'/>
    <BoardTab :category="category" boardTitle="질문 게시판" boardId='PsQ&A'/>
    <BoardTab :category="category" boardTitle="리뷰 게시판" boardId='PsReview'/>
  </b-tabs>
</template>

<script>
import BoardTab from '../Tab/BoardTab'
import CategoryTab from '../Tab/CategoryTab'
import PageTab from '../Tab/PageTab'

export default {
  name: 'PS',
  components: {
    BoardTab: BoardTab,
    CategoryTab: CategoryTab,
    PageTab: PageTab
  },
  data () {
    return {
      category: 'PS'
    }
  }
}
</script>
