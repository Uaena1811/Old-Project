<template>
  <div>
    <div class="card">
      <div class="card-header card-header-flex">
        <div class="d-flex flex-column justify-content-center">
          <a-button class="btn btn-primary" type="primary" icon="plus" @click="showModal">
            Thêm sản phẩm liên quan
          </a-button>
        </div>
      </div>

      <a-modal title="Thêm sản phẩm liên quan"
               :visible="visible"
               :width="1000"
               @ok="handleOk"
               :confirmLoading="false"
               @cancel="handleCancel">
        <CreateOrUpdate></CreateOrUpdate>
      </a-modal>
      <div class="card-body" style="padding:16px;">
        <div class="air__utils__scrollTable">
          <a-table :columns="columns"
                   :dataSource="relate"
                   bordered
                   :scroll="{ x: '100%' }"
                   :pagination="false">
            <span slot="action" slot-scope="record">
              <a-button class="btn btn-sm btn-danger mr-2" @click="deleteRelate(record.id)">
                <a-icon type="delete" />
                Xóa
              </a-button>
            </span>
            <span slot="callForPrice" slot-scope="record">
              {{record.toString()}}
            </span>
          </a-table>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
  import Axios from 'axios';
  import ProductApi from '../api';
  import RelateApi from './api';
  import CreateOrUpdate from './createorupdate';
  export default {
    data() {
      return {
        relate: null,
        columns: [
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
            title: 'Gọi để biết giá',
            dataIndex: 'callForPrice',
            scopedSlots: { customRender: 'callForPrice' }
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
          },
        ],
        visible: false,
      }
    },
    mounted() {
      this.loadRelate();
    },
    methods: {
      showModal() {
        this.visible = true;
      },
      handleOk() {
        this.visible = false;
        this.loadRelate();
      },
      handleCancel() {
        this.visible = false;
        this.loadRelate();
      },
      loadRelate() {
        Axios.get(ProductApi.getRelate + this.$route.params.id).then(r => {
          this.relate = r.data
        }).catch(error => {
          this.$message.error('Đã xảy ra lỗi!', 3);
          console.log(error);
        });
      },
      deleteRelate(id) {
        if (confirm("Có thật sự muốn xóa?")) {
          Axios.delete(RelateApi.delete + this.$route.params.id + '/' + id).then(r => {
            if (r.data.result != 1) {
              this.$message.error(r.data.message, 3);
              console.log(r.data.message);
            }
            else {
              this.loadRelate();
            }
          }).catch(error => {
            this.$message.error("Đã xảy ra lỗi!", 3);
            console.log(error);
          });
        }
      },
    },
    components: {
      CreateOrUpdate
    }
  }
</script>
