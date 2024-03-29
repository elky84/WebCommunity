<template>
  <tr>
    <td colspan="7" :class="borderClass">
      <div v-if="editMode">
        <b-form-input v-model="article.title" placeholder="Enter Title"></b-form-input>
        <b-form-input v-model="article.author" placeholder="Enter Author"></b-form-input>
        <b-form-input v-model="article.category" placeholder="Enter Category"></b-form-input>
        <b-form-input v-model="article.source" placeholder="Enter Source"></b-form-input>
        <input-tag v-model="article.tags"></input-tag>
      </div>

      <label :v-if="!!srcArticle.source">[출처] {{srcArticle.source}}</label>

      <BoardEditor ref="editor" @onEditorContent="onEditorContent(... arguments)"
        :originEditable="editMode" :originContent=article.content />

      <b-btn-group>
        <b-button variant="outline-primary" @click="onClickRecommend()">추천 {{article.recommend}}</b-button>
        <b-button variant="outline-warning" @click="onClickNotRecommend()">비추천 {{article.notRecommend}}</b-button>
        <template v-if="getProfile() && getProfile().userId == article.userId">
          <b-button variant="outline-primary" v-on:click="onClickUpdate" v-if="!editMode">수정</b-button>
          <b-button variant="outline-primary" v-on:click="onClickUpdateConfirm" v-if="editMode">수정완료</b-button>
          <b-button variant="outline-warning" v-on:click="onClickDelete" v-if="article.userId">삭제</b-button>
        </template>
        <b-button variant="outline-info" v-on:click="onClickCancel">닫기</b-button>
      </b-btn-group>
      <hr/>

      <div v-for="(comment) in comments" :key="comment.id">
        <BoardComment :comment=comment :boardId="boardId" />
        <BoardCommentEdit v-if="comment.reply" :srcComment="comment" :boardId="boardId" :srcArticle="srcArticle" @refreshComments="refreshComments(... arguments)" />
      </div>
      <b-pagination ref="pagination" align="center" size="md" v-model="currentPage" :total-rows="totalItems" :per-page="limit" @change="listing(... arguments)" />

      <BoardCommentEdit :boardId="boardId" :srcArticle="srcArticle" @refreshComments="refreshComments(... arguments)" />
    </td>
  </tr>
</template>

<script>
import BoardEditor from './BoardEditor'
import BoardCommentEdit from './BoardCommentEdit'
import BoardComment from './BoardComment'

import dayjs from 'dayjs'
import * as _ from 'lodash'

export default {
  name: 'BoardEdit',
  props: {
    boardId: String,
    srcArticle: Object,
    borderClass: String
  },
  components: {
    BoardEditor: BoardEditor,
    BoardCommentEdit: BoardCommentEdit,
    BoardComment: BoardComment
  },
  data () {
    return {
      editMode: false,
      editorContent: '',
      article: Object.assign({}, this.srcArticle),
      comments: [],
      currentPage: 1,
      limit: 10,
      totalItems: 0
    }
  },
  watch: {
    editMode () {
      this.$refs.editor.setEditMode(this.editMode)
    }
  },
  mounted () {
    this.refreshComments()
  },
  methods: {
    refreshComments () {
      var vm = this
      this.$axios.get(`${process.env.VUE_APP_URL_BACKEND}/Board/Comment/${this.boardId}/${this.article.id}`,
        {
          params: {
            offset: this.limit * (this.currentPage - 1),
            limit: this.limit
          }
        })
        .then((result) => {
          vm.comments = result.data.datas
          vm.totalItems = result.data.total
        })
    },
    onClickUpdate () {
      this.editMode = true
    },
    onClickUpdateConfirm () {
      this.editMode = false
      this.$refs.editor.getHTML()

      var vm = this
      this.$axios.put(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${this.article.id}`,
        {
          id: this.article.id,
          author: this.article.author,
          title: this.article.title,
          content: this.editorContent,
          category: this.article.category,
          tags: this.article.tags
        })
        .then((result) => {
          if (result.data.data) {
            vm.onUpdate(result.data.data)
          }
        })
    },
    onClickDelete () {
      var vm = this
      this.$axios.delete(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${this.article.id}`)
        .then((result) => {
          vm.$emit('delete', this.article)
        })
    },
    onCommentDelete (deleteComment) {
      this.comments = _.filter(this.comments, function (comment) {
        return comment.id !== deleteComment.id
      })
    },
    onClickCancel () {
      this.$emit('close', this.article)
    },
    onEditorContent (editorContent) {
      this.editorContent = editorContent
    },
    onClickRecommend () {
      var vm = this
      this.$axios.post(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${this.article.id}/Recommend`)
        .then((result) => {
          vm.onUpdate(result.data.data)
        })
    },
    onClickNotRecommend () {
      var vm = this
      this.$axios.post(`${process.env.VUE_APP_URL_BACKEND}/Board/${this.boardId}/${this.article.id}/NotRecommend`)
        .then((result) => {
          if (result.data.data) {
            vm.onUpdate(result.data.data)
          }
        })
    },
    onUpdate (newArticle) {
      this.article = newArticle
      this.$emit('update', this.article)
    },
    toDateString (date) {
      return dayjs(date).format('YYYY-MM-DD HH:mm:ss')
    },
    listing (page) {
      this.currentPage = page
      this.refreshComments()
    }
  }
}
</script>

<style scoped>

</style>
