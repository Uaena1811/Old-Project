<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">

      <h5>Tạo / cập nhật đơn vị sản phẩm</h5>
    </div>
    <div class="row">
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
              <div class="col-lg-8">
                <a-form-item label="Mã sản phẩm" class="mb-2">
                  <a-select v-decorator="['ProductId', { rules: [{ required: true, message: 'Vui lòng nhập mã sản phẩm!' }] }]"
                            @popupScroll="GetProduct"
                            @search="GetProduct">
                    <!--:showSearch="true"-->
                    <a-select-option v-for="(product, index) in products" :key="index" :value="product.id">{{product.id}} - {{product.name}}</a-select-option>
                  </a-select>
                </a-form-item>
              </div>
              <div class="col-lg-8">
                <a-form-item label="Mã thuộc tính tồn kho" class="mb-2">
                  <a-select v-decorator="['InventoryAttributeId', { rules: [{ required: true, message: 'Vui lòng nhập mã thuộc tính tồn kho!' }] }]"
                            @popupScroll="GetInventoryAttribute">
                    <a-select-option v-for="(inventoryAttribute, index) in inventoryAttributes" :key="index" :value="inventoryAttribute.id">{{inventoryAttribute.id}} - {{inventoryAttribute.name}}</a-select-option>
                  </a-select>
                </a-form-item>
              </div>
            </div>
            <div class="row">
              <div class="col-lg-8">
                <a-form-item label="Mã giá trị thuộc tính" class="mb-2">
                  <a-select v-decorator="['AttributeValueId', { rules: [{ required: true, message: 'Vui lòng nhập mã giá trị thuộc tính!' }] }]"
                            @popupScroll="GetAttributeValue">
                    <a-select-option v-for="(attributeValue, index) in attributeValues" :key="index" :value="attributeValue.id">{{attributeValue.id}} - {{attributeValue.value}}</a-select-option>
                  </a-select>
                </a-form-item>
              </div>
              <div class="col-lg-8">
                <a-form-item label="Số lượng" class="mb-2">
                  <a-input-number style="width: 100%" v-decorator="['Quantity', { rules: [{ required: true, message: 'Vui lòng nhập số lượng!' }] }]" />
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
    </div>
  </div>
  <!--<h6 class="mb-3 text-uppercase">
    <strong>Sản phẩm liên quan</strong>
  </h6>-->
</template>
<script>
  import CkEditor from '@ckeditor/ckeditor5-build-classic';
  import Axios from 'axios';
  import ProductPieceApi from './api';
  export default {
    created() {
      Axios.get(ProductPieceApi.getById + this.$route.params.id).then(r => {
        this.Model.ProductId = r.data.productId;
        this.Model.InventoryAttributeId = r.data.inventoryAttributeId;
        this.Model.AttributeValueId = r.data.attributeValueId;
        this.Model.Quantity = r.data.quantity;
      }).then(() => {
        this.CreateForm();
      });
    },
    mounted() {
      this.GetProduct();
      this.GetAttributeValue();
      this.GetInventoryAttribute();
    },
    data() {
      return {
        FrmProduct: null,
        Model: {
          ProductId: 0,
          InventoryAttributeId: 0,
          AttributeValueId: 0,
          Quantity: 0
        },
        editor: CkEditor,
        editorConfig: {

        },
        products: [],
        productPageIndex: 1,
        productLoad: true,
        inventoryAttributes: [],
        inventoryAttributePageIndex: 1,
        inventoryLoad: true,
        attributeValues: [],
        attributeValuePageIndex: 1,
        attributeLoad: true,
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            return {
              ProductId: this.$form.createFormField({ value: this.Model.ProductId, }),
              InventoryAttributeId: this.$form.createFormField({ value: this.Model.InventoryAttributeId, }),
              AttributeValueId: this.$form.createFormField({ value: this.Model.AttributeValueId, }),
              Quantity: this.$form.createFormField({ value: this.Model.Quantity, }),
            };
          }
        }
        this.FrmProduct = this.$form.createForm(this, options);
      },
      GetModel() {
        this.Model.ProductId = this.FrmProduct.getFieldValue('ProductId'); 7
        this.Model.InventoryAttributeId = this.FrmProduct.getFieldValue('InventoryAttributeId');
        this.Model.AttributeValueId = this.FrmProduct.getFieldValue('AttributeValueId');
        this.Model.Quantity = this.FrmProduct.getFieldValue('Quantity');
      },
      async SaveAndFinish() {
        this.FrmProduct.validateFieldsAndScroll((errors, values) => {
          if (!errors) {
            this.GetModel();
            Axios.post(ProductPieceApi.createOrUpdate, this.Model).then(r => {
              if (r.data.result != 1) {
                this.$message.error(r.data.message);
              }
              else {
                this.$message.success('Lưu dữ liệu thành công', 3);
                this.$router.replace("/attribute");
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
            Axios.post(ProductPieceApi.createOrUpdate, this.Model).then(r => {
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
        Axios.get(ProductPieceApi.getById + this.$route.params.id).then(r => {
          this.FrmProduct.setFieldsValue({
            ProductId: r.data.productId,
            InventoryAttributeId: r.data.inventoryAttributeId,
            AttributeValueId: r.data.attributeValueId,
            Quantity: r.data.quantity,
          })
          this.Model.CallForPrice = r.data.callForPrice;
          this.$message.success('Reset thành công', 3);
        })
      },
      async Get(link, pageIndex, keyword) {
        let result = await Axios.post(link, {
          "Pagination": {
            "PageIndex": pageIndex,
            "PageSize": 10
          },
          "Sort": {

          },
          "Search": {

          }
        });
        return result;
      },
      GetProduct(keyword) {
        if (this.productLoad) {
          this.Get(ProductPieceApi.getProduct, this.productPageIndex, keyword).then(r => {
            this.products = this.products.concat(r.data.items);
            if (this.products.length == r.data.totalRecord) {
              this.productLoad = false;
            }
          });
          this.productPageIndex += 1;
        }
      },
      GetInventoryAttribute(keyword) {
        if (this.inventoryLoad) {
          this.Get(ProductPieceApi.getInventory, this.inventoryAttributePageIndex, keyword).then(r => {
            this.inventoryAttributes = this.inventoryAttributes.concat(r.data.items);
            if (this.inventoryAttributes.length == r.data.totalRecord) {
              this.inventoryLoad = false;
            }
          });
          this.inventoryAttributePageIndex += 1;
        }
      },
      GetAttributeValue(keyword) {
        if (this.attributeLoad) {
          this.Get(ProductPieceApi.getAtrributeValue, this.attributeValuePageIndex, keyword).then(r => {
            this.attributeValues = this.attributeValues.concat(r.data.items);
            if (this.attributeValues.length == r.data.totalRecord) {
              this.attributeLoad = false;
            }
          });
          this.attributeValuePageIndex += 1;
        }
      }
    },
    components: {

    }
  }
</script>
