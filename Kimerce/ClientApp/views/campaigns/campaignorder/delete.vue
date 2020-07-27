<template>
  <div>
    <h3>Do you want to delete this campaign ?</h3>
    <br />
    <a-button type="danger" @click="confirmDelete">Delete</a-button> |
    <router-link :to="{path:'/campaignorder'}">Back to List</router-link>
  </div>

</template>

<script>
  import axios from "axios"
  export default {
    name: 'campaignorderdelete',
    data() {
      return {

      }
    },
    methods: {
      confirmDelete() {
        axios.delete
          (
            "https://localhost:44397/CampaignOrder/delete/" + this.$route.params.id,
          ).then(response => { if (response) this.$router.replace({ path: "/campaignorder" }); })
      }
    }
  }
</script>
