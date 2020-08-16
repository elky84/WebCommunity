<template>
  <div>
    <b-card no-body>
      <b-tabs pills card vertical>
        <b-tab title="Search" active>
          <BoardList/>
        </b-tab>
      </b-tabs>
    </b-card>
  </div>
</template>

<script>
import BoardList from '../Board/BoardList'

export default {
  name: 'News',
  data () {
    return {
    }
  },
  components: {
    BoardList: BoardList
  }
}
</script>
