<template>
  <b-tab no-body disabled >
    <template #title>
      <strong style="font-size: smaller;">{{title}}</strong>
    </template>
  </b-tab>
</template>

<script>
export default {
  name: 'CategoryTab',
  props: {
    title: {
      type: String,
      default: ''
    }
  }
}
</script>
