<template>
  <div>
    <board-write/>
  </div>
</template>

<script>
import BoardWrite from './Board/BoardWrite'
export default {
  name: 'Home',
  components: {
    BoardWrite: BoardWrite
  }
}

</script>
