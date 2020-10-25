<template>
  <b-tabs pills card vertical>
    <PageTab :category="category" title="스위치"/>
    <CategoryTab title="정보 게시판"/>
    <BoardTab :category="category" boardTitle="스위치 유저 정보" boardId='SwitchUserInfo'/>
    <BoardTab :category="category" boardTitle="핫딜 게시판" boardId='Hotdeal'/>
    <CategoryTab title="게임 게시판"/>
    <BoardTab :category="category" boardTitle="게임 이야기" boardId='SwitchGameTalk'/>
    <BoardTab :category="category" boardTitle="잡담 게시판" boardId='SwitchFreeTalk'/>
    <BoardTab :category="category" boardTitle="질문 게시판" boardId='SwitchQ&A'/>
    <BoardTab :category="category" boardTitle="리뷰 게시판" boardId='SwitchReview'/>
  </b-tabs>
</template>

<script>
import BoardTab from '../Tab/BoardTab'
import CategoryTab from '../Tab/CategoryTab'
import PageTab from '../Tab/PageTab'

export default {
  name: 'Switch',
  components: {
    BoardTab: BoardTab,
    CategoryTab: CategoryTab,
    PageTab: PageTab
  },
  data () {
    return {
      category: 'Switch'
    }
  }
}
</script>
