<template>
  <div>
    <h3>Do you want to delete this discount ?</h3>
    <br />
    <a-button type="danger" @click="confirmDelete">Delete</a-button> |
    <router-link :to="{path:'/discountorder'}">Back to List</router-link>
  </div>

</template>

<script>
  import axios from "axios"
  export default {
    name: 'discountorderdelete',
    data() {
      return {

      }
    },
    methods: {
      confirmDelete() {
        axios.delete
          (
            "https://localhost:44397/DiscountOrder/delete/" + this.$route.params.id,
          ).then(response => { if (response) this.$router.replace({ path: "/discountorder" }); })
      }
    }
  }
</script>
