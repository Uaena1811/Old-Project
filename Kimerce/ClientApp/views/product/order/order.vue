<template>
  <div>
    <div class="card">
      <div class="card-body" style="padding:16px;">
        <div class="air__utils__scrollTable">
          <a-table :columns="columns"
                   :dataSource="order"
                   bordered
                   :pagination="false">
            <span slot="action" slot-scope="record">
              <router-link :to="{path: '/order/createorupdate/' + record.id}">
                <a-button class="btn btn-sm btn-primary mr-2" type="primary">
                  <a-icon type="delete" />
                  Sửa
                </a-button>
              </router-link>
            </span>
          </a-table>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
  import Axios from 'axios';
  import OrderItemApi from './api';
  export default {
    data() {
      return {
        order: null,
        columns: [
          {
            title: 'ID',
            dataIndex: 'id',
          },
          {
            title: 'Mã giao dịch',
            dataIndex: 'transactionsid',
          },
          {
            title: 'Nội dung',
            dataIndex: 'shortDescription',
          },
          {
            title: 'Ngày giao dịch',
            dataIndex: 'createdTime',
          },
          {
            title: 'Giá trị',
            dataIndex: 'value',
          },
          {
            title: 'Status',
            dataIndex: 'status',
          },
          {
            title: 'Action',
            scopedSlots: { customRender: 'action' },
          },
        ],
      }
    },
    mounted() {
      this.loadOrder();
    },
    methods: {
      loadOrder() {
        Axios.get(OrderItemApi.getOrder + this.$route.params.id).then(r => {
          this.order = r.data;
        }).catch(error => {
          this.$message.error('Đã xảy ra lỗi!', 3);
          console.log(error);
        });
      },
    },
    components: {

    }
  }
</script>
