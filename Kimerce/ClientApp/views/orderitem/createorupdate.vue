<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">

      <h5>Tạo / cập nhật sản phẩm trong đơn hàng</h5>
    </div>
    <div class="row">
      <a-form layout="vertical" :form="Form">
        <div class="col-lg-12 text-right">
          <router-link to="/orderitem">
            <a-button class="btn btn-primary mb-3" type="primary">
              <a-icon type="rollback" />
              Trở lại
            </a-button>
          </router-link>
          <a-button class="btn btn-primary mb-3" type="primary" html-type="button" icon="save" @click="SaveAndFinish">Lưu lại</a-button>
          <a-button class="btn btn-primary mb-3" type="primary" html-type="button" icon="save" @click="Save">Lưu và sửa tiếp</a-button>
          <a-button class="btn btn-danger mb-3" type="danger" html-type="button" icon="save" @click="Reset">Reset thông tin</a-button>
        </div>
        <div class="card overflow-hidden">
          <div class="card-body">
            <div class="row">
              <div class="col-lg-8">
                <a-form-item label="ID Order" class="mb-2">
                  <a-input-number style="width: 100%"
                                  v-decorator="['OrderId', {
                                  rules: [
                                  {
                                    type: 'integer',
                                    message: 'Vui lòng nhập số nguyên!'
                                  },{
                                    required: true,
                                    message: 'Vui lòng nhập ID đơn hàng!'
                                  }]
                                  }]" />
                </a-form-item>
              </div>
              <div class="col-lg-8">
                <a-form-item label="ID Product" class="mb-2">
                  <a-input-number style="width: 100%"
                                  v-decorator="['ProductPieceId', {
                                  rules: [
                                  {
                                    type: 'integer',
                                    message: 'Vui lòng nhập số nguyên!'
                                  },{
                                    required: true,
                                    message: 'Vui lòng nhập ID sản phẩm!'
                                  }]
                                  }]" />
                </a-form-item>
              </div>
              <div class="col-lg-8">
                <a-form-item label="Thứ tự hiển thị" class="mb-2">
                  <a-input-number style="width: 100%"
                                  v-decorator="['DisplayOrder', {
                                  rules: [
                                  {
                                    type: 'integer',
                                    message: 'Vui lòng nhập số nguyên!'
                                  }]
                                  }]" />
                </a-form-item>
              </div>
              <div class="col-lg-8">
                <a-form-item label="Số lượng" class="mb-2">
                  <a-input-number style="width: 100%"
                                  v-decorator="['NumberOfProduct', {
                                  rules: [
                                  {
                                    type: 'integer',
                                    message: 'Vui lòng nhập số nguyên!'
                                  },{
                                    required: true,
                                    message: 'Vui lòng nhập ID sản phẩm!'
                                  }]
                                  }]" />
                </a-form-item>
              </div>

            </div>
          </div>
          <div class="row">
            <div class="col-lg-12">
            </div>
          </div>
        </div>
      </a-form>
    </div>
  </div>

</template>
<script>
  
  import Axios from 'axios';
 
  export default {
    created() {
      Axios.get("https://localhost:44397/orderitems/Getbyid/" + this.$route.params.id).then(r => {
        this.Model.ProductPieceId = r.data.productPieceId;
        this.Model.OrderId = r.data.orderId;
        this.Model.DisplayOrder = r.data.displayOrder;
        this.Model.NumberOfProduct = r.data.NumberOfProduct;
        this.Model.Id = r.data.id;
      }).then(() => {
        this.CreateForm();
      });
    },
    mounted() {
      Axios.get("https://localhost:44397/products/Getbyid/" + this.Model.IdProduct).then(r => {
        this.Model.Price = r.data.Id;
      });
      
    },
    data() {
      return {
        Form: null,
        Model: {
          ProductPieceId: null,
          OrderId: null,
          DisplayOrder: 0,
          Price: 0,
          NumberOfProduct: 0,
        },
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            return {
              ProductPieceId: this.$form.createFormField({ value: this.Model.ProductPieceId, }),
              OrderId: this.$form.createFormField({ value: this.Model.OrderId, }),
              DisplayOrder: this.$form.createFormField({ value: this.Model.DisplayOrder, }),
              NumberOfProduct: this.$form.createFormField({ value: this.Model.NumberOfProduct, }),

              
            };
          }
        }
        this.Form = this.$form.createForm(this, options);
      },
      GetModel() {
        this.Model.ProductPieceId = this.Form.getFieldValue('ProductPieceId');
        this.Model.OrderId = this.Form.getFieldValue('OrderId');
        this.Model.DisplayOrder = this.Form.getFieldValue('DisplayOrder');
        this.Model.NumberOfProduct = this.Form.getFieldValue('NumberOfProduct');

      },
      async SaveAndFinish() {
        this.Form.validateFieldsAndScroll((errors, values) => {
          if (!errors) {
            this.GetModel();
            this.Model.Price = 0;
            Axios.post("https://localhost:44397/orderitems/createorupdate/", this.Model).then(r => {
              if (r.data.result != 1) {
                this.$message.error(r.data.message);
              }
              else {
                this.$message.success('Lưu dữ liệu thành công', 3);
                this.$router.replace("/orderitem");
              }
            }).catch(error => {
              this.$message.error('Không thể kết nối tới máy chủ', 3);
              console.log(error);
            });
          }
        });
      },
      Save() {
        this.Form.validateFieldsAndScroll((errors, values) => {
          if (!errors) {
            this.GetModel();
            Axios.post("https://localhost:44397/orderitems/createorupdate/", this.Model).then(r => {
              if (r.data.result != 1) {
                this.$message.error(r.data.message);
              }
              else {
                this.$message.success('Lưu dữ liệu thành công', 3);
              }
            }).catch(error => {
              this.$message.error('Không thể kết nối tới máy chủ', 3);
              console.log(error);
            });
          }
        });
      },
      Reset() {
        Axios.get("https://localhost:44397/orderitems/getbyid/" + this.$route.params.id).then(r => {
          this.Form.setFieldsValue({
            ProductPieceId : r.data.productPieceId,
        OrderId : r.data.OrderId,
        DisplayOrder : r.data.displayOrder,
       NumberOfProduct : r.data.NumberOfProduct,
          })
          this.$message.success('Reset thành công', 3);
        })
      }
    }
  }
</script>
