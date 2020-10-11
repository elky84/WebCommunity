<template>
  <div>
    <b-card no-body>
      <b-tabs pills card vertical>
        <b-tab title="Free" active>
          <BoardList boardId="Free"/>
        </b-tab>
        <b-tab title="Humor">
          <BoardList boardId="Humor"/>
        </b-tab>
      </b-tabs>
    </b-card>
  </div>
</template>

<script>
import BoardList from '../Board/BoardList'

export default {
  name: 'Community',
  data () {
    return {
    }
  },
  components: {
    BoardList: BoardList
  }
}
</script>
