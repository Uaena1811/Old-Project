<template>
  <div>
    <a-tabs defaultActiveKey="1">
      <a-tab-pane tab="Tỉnh" key="1"><city></city></a-tab-pane>
      <a-tab-pane tab="Huyện" key="2" forceRender><district></district></a-tab-pane>
      <a-tab-pane tab="Xã" key="3"><ward></ward></a-tab-pane>
    </a-tabs>
  </div>
</template>
<script>
  import axios from 'axios'
  import city from './city/city.vue'
  import district from './district/district.vue'
  import ward from './ward/ward.vue'

  export default {
    components: {
      city, district, ward
    },
    data: function () {
      return {

      }
    }
  }
</script>
