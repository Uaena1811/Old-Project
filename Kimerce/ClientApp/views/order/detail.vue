<template>
<div>
  <div class="air__utils__heading" style="margin-bottom:20px;">

    <h5>Chi tiết đơn hàng</h5>
  </div>
  <div class="row">
    <div>
      <div class="col-lg-12 text-right">
        <router-link to="/order">
          <a-button class="btn btn-primary mb-3" type="primary">
            <a-icon type="rollback" />
            Trở lại
          </a-button>
        </router-link>

        <router-link :to="{path: '/order/createorupdate/'+ $route.params.id}">
          <a-button class="btn btn-primary mb-3" type="primary">
            <a-icon type="edit" />
            Edit
          </a-button>
        </router-link>

        <router-link to="/order/createorupdate/0">
          <a-button class="btn btn-primary mb-3" type="primary">
            <a-icon type="plus" />
            Tạo đơn hàng mới
          </a-button>
        </router-link>

        <router-link to="/orderitem/createorupdate/0">
          <a-button class="btn btn-primary mb-3" type="primary">
            <a-icon type="plus" />
            Thêm sản phẩm
          </a-button>
        </router-link>

      </div>
      <div class="card overflow-hidden">
        <div class="card-body">
          <a-tabs defaultActiveKey="1">
            <a-tab-pane tab="Miêu tả" key="1">
              <p> {{concak}}</p>
            </a-tab-pane>
            <a-tab-pane tab="Sản phẩm" key="2">
              <a-table :columns="Columns"
                       :dataSource="arr"
                       :scroll="{ x: '100%' }"
                       bordered
                       :pagination="false">
                <span slot="action" slot-scope="record">

                  <router-link :to= "{path:'/product/details/' + record.id}" >
                    <a-button class="btn btn-sm btn-primary mr-2">
                    <a-icon type="profile" />
                     </a-button>
                  </router-link>

                  <router-link :to="{path:'/product/edit/' + record.id}" >
                    <a-button class="btn btn-sm btn-primary mr-2">
                      <a-icon type="edit" />
                     </a-button>
                  </router-link>

                  <a-button class="btn btn-sm btn-danger" @click="DeleteProductinOrder(record.id)">
                    <a-icon type="delete" />                   
                  </a-button>
                </span>
              </a-table>
            </a-tab-pane>
            
          </a-tabs>

          
          
        </div>
      </div>
    </div>
  </div>
</div>

</template>

<script>
  import axios from 'axios'
  export default {
    data() {
      return {
        concak: "concakkkkk",
        arr: [],
        Order: null,
        Columns: [
          {
            title: 'ID',
            dataIndex: 'id',
          },
          {
            title: 'Tên sản phẩm',
            dataIndex: 'name',
          },
          {
            title: 'SKU',
            dataIndex: 'sku',
          },
          {
            title: 'Giá',
            dataIndex: 'price',
          },
          {
            title: 'Mô tả ngắn',
            dataIndex: 'shortDescription',
          },
          {
            title: 'Ngày tạo',
            dataIndex: 'createdTimeDisplay',
          },
          {
            title: 'Ngày cập nhật',
            dataIndex: 'updatedTimeDisplay',
          },
          {
            title: 'Action',
            scopedSlots: { customRender: 'action' },
            width: 50,
          },
        ]
      };
    },
    methods: {
      callback(key) {
        console.log(key);
      },
      LoadData() {
        var self = this;
        axios.get('https://localhost:44397/orderitems/GetProductsByOrderId/' + this.$route.params.id)
            .then(function (res) {
              self.arr = res.data;
              console.log('Dat:a', self.arr);
            })
            .catch(function (error) {
              console.log('Error: ', error);
            })
      },
      DeleteProductinOrder(id) {
        if (confirm("Có thật sự muốn xóa?")) {
          axios.delete('https://localhost:44397/Orderitems/DeleteProductInOrder/'+this.$route.params.id + "?id1=" + id).then(response => {
            console.log(response);
            if (response.data.result == 1) {
              this.LoadData();
            }
          });

        }
      },

      },
        mounted() {
          var self = this;

          axios.get('https://localhost:44397/orders/getbyid/' + this.$route.params.id)
            .then(function (res) {
              self.Order = res.data;
              self.concak = res.data.shortDescription;
              console.log(self.Order);
            })
            .catch(function (error) {
              console.log(error);
            })


          axios.get('https://localhost:44397/orderitems/GetProductsByOrderId/' + this.$route.params.id)
            .then(function (res) {
              self.arr = res.data;
              console.log('Dat:a', self.arr);
            })
            .catch(function (error) {
              console.log('Error: ', error);
            })
        }
      
    
  }
</script>
