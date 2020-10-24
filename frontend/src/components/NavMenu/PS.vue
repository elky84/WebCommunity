<template>
  <b-tabs pills card vertical>
    <PageTab title="플레이스테이션"/>
    <CategoryTab title="정보 게시판"/>
    <BoardTab boardTitle="플스 유저 정보" boardId='PsUserInfo'/>
    <BoardTab boardTitle="핫딜 게시판" boardId='Hotdeal'/>
    <CategoryTab title="게임 게시판"/>
    <BoardTab boardTitle="게임 이야기" boardId='PsGameTalk'/>
    <BoardTab boardTitle="잡담 게시판" boardId='PsFreeTalk'/>
    <BoardTab boardTitle="질문 게시판" boardId='PsQ&A'/>
    <BoardTab boardTitle="리뷰 게시판" boardId='PsReview'/>
  </b-tabs>
</template>

<script>
import BoardTab from '../Tab/BoardTab'
import CategoryTab from '../Tab/CategoryTab'
import PageTab from '../Tab/PageTab'

export default {
  name: 'Community',
  components: {
    BoardTab: BoardTab,
    CategoryTab: CategoryTab,
    PageTab: PageTab
  }
}
</script>
