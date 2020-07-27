<template>
  <div>
    <router-link :to="{ path:'/News'}"><h4 style="color:deepskyblue">Back</h4></router-link>
    <a-tabs defaultActiveKey="1" class="air-tabs">
      <a-tab-pane tab="Description" key="1" v-html="text">
      </a-tab-pane>
      <a-tab-pane tab="CreatedBy" key="2"><h3>{{Model.createdByUserName}}</h3></a-tab-pane>
      <a-tab-pane tab="Start and EndDate" key="3"><h5>{{utc1}} <br /> {{utc2}}</h5></a-tab-pane>
    </a-tabs>
  </div>
</template>
<script>
  import 'quill/dist/quill.core.css'
  import 'quill/dist/quill.snow.css'
  import { quillEditor } from 'vue-quill-editor'
  import axios from 'axios'
  export default {
    components: {
      quillEditor,
    },
    data: function () {
      return {
        Model: {},
        text: '<p>sdads</p>',
      }
    },
    mounted() {
      var self = this;
      axios.get('https://localhost:44397/News/GetById/' +this.$route.params.id)
        .then(function (res) {
          self.Model = res.data;
          self.text = self.Model.description;
          console.log(res);
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    computed: {
      utc1() {
        return (new Date(this.Model.startDate)).toLocaleString();
      },
      utc2() {
        return (new Date(this.Model.endDate)).toLocaleString();
      },
    }
  }
</script>
