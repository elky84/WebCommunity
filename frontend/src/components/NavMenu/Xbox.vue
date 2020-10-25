<template>
  <b-tabs pills card vertical>
    <PageTab :category="category" title="엑스박스"/>
    <CategoryTab title="정보 게시판"/>
    <BoardTab :category="category" boardTitle="엑스박스 유저 정보" boardId='XboxUserInfo'/>
    <BoardTab :category="category" boardTitle="핫딜 게시판" boardId='Hotdeal'/>
    <CategoryTab title="게임 게시판"/>
    <BoardTab :category="category" boardTitle="게임 이야기" boardId='XboxGameTalk'/>
    <BoardTab :category="category" boardTitle="잡담 게시판" boardId='XboxFreeTalk'/>
    <BoardTab :category="category" boardTitle="질문 게시판" boardId='XboxQnA'/>
    <BoardTab :category="category" boardTitle="리뷰 게시판" boardId='XboxReview'/>
  </b-tabs>
</template>

<script>
import BoardTab from '../Tab/BoardTab'
import CategoryTab from '../Tab/CategoryTab'
import PageTab from '../Tab/PageTab'

export default {
  name: 'Xbox',
  components: {
    BoardTab: BoardTab,
    CategoryTab: CategoryTab,
    PageTab: PageTab
  },
  data () {
    return {
      category: 'Xbox'
    }
  }
}
</script>
