<template>
  <div>
    <div class="card">

      <div class="card-header card-header-flex">
        <div class="d-flex flex-column justify-content-center">
          <a-button class="btn btn-primary" type="primary" icon="plus" @click="showModal">
            Thêm ảnh của sản phẩm
          </a-button>
        </div>
      </div>

      <a-modal title="Thêm ảnh"
               :visible="visible"
               :width="1000"
               @ok="handleOk"
               :confirmLoading="false"
               @cancel="handleCancel">
        <CreateOrUpdate></CreateOrUpdate>
      </a-modal>

      <div class="card-body" style="padding:16px;">
        <div class="air__utils__scrollTable">
          <div class="items">
            <div class="item" v-for="item in images" :key="item.id">
              <div class="itemContent">
                <div class="itemControl">
                  <div class="itemControlContainer">
                    <a-button-group>
                      <a-button icon="edit" @click="editImage(item.id)"/>
                      <a-button icon="delete" @click="deleteImage(item.id)"/>
                    </a-button-group>
                  </div>
                </div>
                <img :src="item.path" alt="Image">
              </div>
              <div class="text-gray-6">
                <div class="text-center">{{item.name}}</div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import Axios from 'axios';
  import ProductImageApi from './api';
  import CreateOrUpdate from './createorupdate';
  export default {
    props: {

    },
    data() {
      return {
        images: null,
        visible: false,
      }
    },
    mounted() {
      this.loadImage();
    },
    methods: {
      showModal() {
        this.visible = true;
      },
      handleOk() {
        this.visible = false;
        this.loadImage();
      },
      handleCancel() {
        this.visible = false;
        this.loadImage();
      },
      loadImage() {
        Axios.get(ProductImageApi.getImageByProduct + this.$route.params.id).then(r => {
          this.images = r.data
        }).catch(error => {
          this.$message.error('Đã xảy ra lỗi!', 3);
          console.log(error);
        });
      },
      editImage(id) {
        this.$message.error("Tính năng sẽ được cập nhật sau!", 3);
      },
      deleteImage(id) {
        if (confirm("Có thật sự muốn xóa?")) {
          Axios.delete(ProductImageApi.delete + this.$route.params.id + '/' + id).then(r => {
            if (r.data.result != 1) {
              this.$message.error(r.data.message, 3);
              console.log(r.data.message);
            }
            else {
              this.loadImage();
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
<style lang="scss" module>
  @import './style.module.scss';
</style>
