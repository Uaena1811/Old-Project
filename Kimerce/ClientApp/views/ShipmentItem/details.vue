<template>
  <div>
    <h1>Details</h1>

    <b-list-group>
      <b-list-group-item>
        ShipmentID : {{Model.shipmentId}}
      </b-list-group-item>
      <b-list-group-item>
        OrderItemID : {{Model.orderItemId}}
      </b-list-group-item>
      <b-list-group-item>
        Quantity : {{Model.quantity}}
      </b-list-group-item>
      <b-list-group-item>
        WareHouseID : {{Model.wareHouseId}}
      </b-list-group-item>
    </b-list-group>
  </div>
</template>

<script>
  import axios from 'axios'
  export default {

    data() {
      return {
        Model: {},
      }
    },

    mounted() {
      var self = this;
      axios.get('https://localhost:44397/ShipmentItems/GetById/' + this.$route.params.id)
        .then(function (res) {
          self.Model = res.data;
          console.log(res.data);
        })
        .catch(function (error) {
          console.log(error);
        })
    }
  }
</script>
<style scoped>
  img {
    border: 1px solid #d8d8d8;
    width: 70%;
    margin: 40px;
    box-shadow: 0px .5px 1px #d8d8d8;
  }

  .product-image {
    width: 80%;
  }

  .product-image,
  .product-info {
    margin-top: 10px;
    width: 50%;
  }

  .product {
    display: flex;
    flex-flow: wrap;
    padding: 1rem;
  }

  button {
    margin-top: 30px;
    border: none;
    background-color: #1E95EA;
    color: white;
    height: 40px;
    width: 100px;
    font-size: 14px;
  }

  .cart {
    margin-right: 25px;
    float: right;
    border: 1px solid #d8d8d8;
    padding: 5px 20px;
  }

  .color-box {
    width: 40px;
    height: 40px;
    margin-top: 5px;
  }

  .disabledButton {
    background-color: #d8d8d8;
  }

  .chu {
    background: #d8d8d8;
    margin-bottom: 20px;
    padding: 10px 20px;
  }
</style>
