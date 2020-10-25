<template>
  <b-tabs pills card vertical>
    <PageTab :category="category" title="모바일"/>
    <CategoryTab title="정보 게시판"/>
    <BoardTab :category="category" boardTitle="모바일 유저 정보" boardId='MobileUserInfo'/>
    <BoardTab :category="category" boardTitle="핫딜 게시판" boardId='Hotdeal'/>
    <CategoryTab title="게임 게시판"/>
    <BoardTab :category="category" boardTitle="게임 이야기" boardId='MobileGameTalk'/>
    <BoardTab :category="category" boardTitle="잡담 게시판" boardId='MobileFreeTalk'/>
    <BoardTab :category="category" boardTitle="질문 게시판" boardId='MobileQnA'/>
    <BoardTab :category="category" boardTitle="리뷰 게시판" boardId='MobileReview'/>
  </b-tabs>
</template>

<script>
import BoardTab from '../Tab/BoardTab'
import CategoryTab from '../Tab/CategoryTab'
import PageTab from '../Tab/PageTab'

export default {
  name: 'Mobile',
  components: {
    BoardTab: BoardTab,
    CategoryTab: CategoryTab,
    PageTab: PageTab
  },
  data () {
    return {
      category: 'Mobile'
    }
  }
}
</script>
