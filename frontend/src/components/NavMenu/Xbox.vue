<template>
  <b-tabs pills card vertical>
    <PageTab title="엑스박스"/>
    <CategoryTab title="정보 게시판"/>
    <BoardTab boardTitle="엑스박스 유저 정보" boardId='XboxUserInfo'/>
    <BoardTab boardTitle="핫딜 게시판" boardId='Hotdeal'/>
    <CategoryTab title="게임 게시판"/>
    <BoardTab boardTitle="게임 이야기" boardId='XboxGameTalk'/>
    <BoardTab boardTitle="잡담 게시판" boardId='XboxFreeTalk'/>
    <BoardTab boardTitle="질문 게시판" boardId='XboxQ&A'/>
    <BoardTab boardTitle="리뷰 게시판" boardId='XboxReview'/>
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
