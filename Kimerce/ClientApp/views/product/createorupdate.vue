<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">

      <h5>Tạo / cập nhật sản phẩm</h5>
    </div>
    <div class="row">
      <a-tabs defaultActiveKey="1">
        <a-tab-pane tab="Thông tin sản phẩm" key="1">
          <a-form layout="vertical" :form="FrmProduct">
            <div class="col-lg-12 text-right">
              <a-button class="btn btn-primary mb-3" type="primary" html-type="button" icon="save" @click="SaveAndFinish">Lưu lại</a-button>
              <a-button class="btn btn-primary mb-3" type="primary" html-type="button" icon="save" @click="Save">Lưu và sửa tiếp</a-button>
              <a-button class="btn btn-danger mb-3" type="danger" html-type="button" icon="save" @click="Reset">Reset thông tin</a-button>
            </div>
            <div class="card overflow-hidden">
              <div class="card-body">
                <div class="row mb-4">
                  <div class="col-lg-12">

                  </div>

                </div>

                <div class="row">
                  <div class="col-lg-4">
                    <a-form-item label="Tên sản phẩm" class="mb-2">
                      <a-input v-decorator="['Name', { rules: [{ required: true, message: 'Vui lòng nhập tên sản phẩm!' }] }]" />
                    </a-form-item>
                    <div class="row">
                      <div class="col-lg-8">
                        <a-form-item label="Giá bán" class="mb-2">
                          <a-input-number style="width: 100%" v-decorator="['Price', { rules: [{ required: true, message: 'Vui lòng nhập giá bán!' },
                                                                { type: 'integer', message: 'Vui lòng nhập số nguyên!' }] }]" />
                        </a-form-item>
                      </div>
                      <div class="col-lg-4">
                        <a-form-item label="Giá cũ" class="mb-2">
                          <a-input-number style="width: 100%" v-decorator="['OldPrice', { rules: [{ type: 'integer', message: 'Vui lòng nhập số nguyên!' }] }]" />
                        </a-form-item>
                      </div>
                    </div>
                    <a-form-item label="Gọi để biết giá" class="mb-2">
                      <a-checkbox v-decorator="['CallForPrice']" :checked="Model.CallForPrice" @change="OnChange" />
                    </a-form-item>
                    <a-form-item label="SKU" class="mb-2">
                      <a-input v-decorator="['Sku']" />
                    </a-form-item>
                    <a-form-item label="Trạng thái">

                    </a-form-item>
                  </div>
                  <div class="col-lg-8">
                    <a-form-item label="Mô tả ngắn" class="mb-2">
                      <!--<a-input v-decorator="['shortdescription', { rules: [{ required: true, message: 'Vui lòng nhập tên sản phẩm!' }] }]" />-->
                      <a-textarea placeholder="Mô tả ngắn"
                                  v-decorator="['ShortDescription', { rules: [{ required: true, message: 'Vui lòng nhập mô tả ngắn sản phẩm!' }] }]"
                                  :autosize="{ minRows: 3, maxRows: 6 }" />
                    </a-form-item>
                    <a-form-item label="Mô tả đầy đủ">
                      <ckeditor :editor="editor" v-model="Model.Description" :config="editorConfig"></ckeditor>
                    </a-form-item>

                  </div>
                </div>
                <div class="row">
                  <div class="col-lg-12">

                  </div>
                </div>
              </div>
            </div>
          </a-form>
        </a-tab-pane>

        <a-tab-pane tab="Danh sách ảnh" key="2">
          <ProductImage></ProductImage>
        </a-tab-pane>

        <a-tab-pane tab="Sản phẩm liên quan" key="3">
          <ProductRelate></ProductRelate>
        </a-tab-pane>

        <a-tab-pane tab="Đơn hàng" key="4">
          <Order></Order>
        </a-tab-pane>
          
      </a-tabs>
    </div>
  </div>
  <!--<h6 class="mb-3 text-uppercase">
    <strong>Sản phẩm liên quan</strong>
  </h6>-->
</template>
<script>
  import CkEditor from '@ckeditor/ckeditor5-build-classic';
  import Axios from 'axios';
  import ProductApi from './api';
  import ProductImage from './image/image';
  import ProductRelate from './relate/relate';
  import Order from './order/order';
  export default {
    created() {
      Axios.get(ProductApi.getById + this.$route.params.id).then(r => {
        this.Model.Name = r.data.name;
        this.Model.ShortDescription = r.data.shortDescription;
        this.Model.Description = r.data.description;
        this.Model.Sku = r.data.sku;
        this.Model.Price = r.data.price;
        this.Model.OldPrice = r.data.oldPrice;
        this.Model.CallForPrice = r.data.callForPrice;
        this.Model.Id = r.data.id;
      }).then(() => {
        this.CreateForm();
      });
    },
    mounted() {

    },
    data() {
      return {
        FrmProduct: null,
        Model: {
          Name: '',
          ShortDescription: "Short Description",
          Description: "",
          Sku: '',
          Price: 0,
          OldPrice: 0,
          CallForPrice: false,
          Status: 1,
          Id: 0,
        },
        editor: CkEditor,
        editorConfig: {

        },
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            return {
              Name: this.$form.createFormField({ value: this.Model.Name, }),
              ShortDescription: this.$form.createFormField({ value: this.Model.ShortDescription }),
              Sku: this.$form.createFormField({ value: this.Model.Sku }),
              Price: this.$form.createFormField({ value: this.Model.Price }),
              OldPrice: this.$form.createFormField({ value: this.Model.OldPrice }),
              CallForPrice: this.$form.createFormField({ value: this.Model.CallForPrice }),
            };
          }
        }
        this.FrmProduct = this.$form.createForm(this, options);
      },
      GetModel() {
        this.Model.Name = this.FrmProduct.getFieldValue('Name');
        this.Model.ShortDescription = this.FrmProduct.getFieldValue('ShortDescription');
        this.Model.Sku = this.FrmProduct.getFieldValue('Sku');
        this.Model.Price = this.FrmProduct.getFieldValue('Price');
        this.Model.OldPrice = this.FrmProduct.getFieldValue('OldPrice');
        this.Model.CallForPrice = this.FrmProduct.getFieldValue('CallForPrice');
      },
      async SaveAndFinish() {
        this.FrmProduct.validateFieldsAndScroll((errors, values) => {
          if (!errors) {
            this.GetModel();
            Axios.post(ProductApi.createOrUpdate, this.Model).then(r => {
              if (r.data.result != 1) {
                this.$message.error(r.data.message);
              }
              else {
                this.$message.success('Lưu dữ liệu thành công', 3);
                this.$router.replace("/product");
              }
            }).catch(error => {
              this.$message.error('Không thể kết nối tới máy chủ', 3);
              console.log(error);
            });
          }
        });
      },
      Save() {
        this.FrmProduct.validateFieldsAndScroll((errors, values) => {
          if (!errors) {
            this.GetModel();
            Axios.post(ProductApi.createOrUpdate, this.Model).then(r => {
              if (r.data.result != 1) {
                this.$message.error(r.data.message);
              }
              else {
                this.$message.success('Lưu dữ liệu thành công!', 3);
                
              }
            }).catch(error => {
              this.$message.error('Đã xảy ra lỗi!', 3);
              console.log(error);
            });
          }
        });
      },
      Reset() {
        Axios.get(ProductApi.getById + this.$route.params.id).then(r => {
          this.FrmProduct.setFieldsValue({
            Name: r.data.name,
            ShortDescription: r.data.shortDescription,
            Sku: r.data.sku,
            Price: r.data.price,
            OldPrice: r.data.oldPrice,
            CallForPrice: r.data.callForPrice,
          })
          this.Model.CallForPrice = r.data.callForPrice;
          this.$message.success('Reset thành công', 3);
        })
      },
      OnChange(e) {
        this.Model.CallForPrice = !this.Model.CallForPrice;
      }
    },
    components: {
      ProductImage,
      ProductRelate,
      Order,
    }
  }
</script>
