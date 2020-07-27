<template>
  <div>
    <h3>Do you want to delete this campaign ?</h3>
    <br />
    <a-button type="danger" @click="confirmDelete">Delete</a-button> |
    <router-link :to="{path:'/campaignproduct'}">Back to List</router-link>
  </div>

</template>

<script>
  import axios from "axios"
  export default {
    name: 'campaignproductdelete',
    data() {
      return {

      }
    },
    methods: {
      confirmDelete() {
        axios.delete
          (
            "https://localhost:44397/CampaignProduct/delete/" + this.$route.params.id,
          ).then(response => { if (response) this.$router.replace({ path: "/campaignproduct" }); })
      }
    }
  }
</script>
