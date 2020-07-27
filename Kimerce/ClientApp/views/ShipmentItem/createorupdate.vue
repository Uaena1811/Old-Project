<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">
      <h5>Tạo đơn hàng</h5>
      <router-link :to="{path:'/shipmentItem/'}" class="btn btn-primary mb-3" type="primary">
        <a-icon type="rollback" />
        Quay lại
      </router-link>
    </div>
    <div class="row">
      <div class="col-lg-12 text-right">
        <a-button class="btn btn-primary mb-3" type="primary" html-type="button" icon="check" @click="Finish">Lưu lại</a-button>
        <a-button class="btn btn-primary mb-3" type="primary" html-type="button" icon="save" @click="Save">Lưu và sửa tiếp</a-button>
        <a-button class="btn btn-danger mb-3" type="danger" html-type="button" icon="undo" @click="Reset">Reset thông tin</a-button>
      </div>
    </div>
    <div class="card overflow-hidden">
      <div class="card-body">
        <div class="row mb-4">

        </div>
        <a-form layout="vertical" :form="FrmProduct">
          <div class="row">
            <div class="col-lg-6">
              <a-form-item label="Mã đơn vận" class="mb-2">
                <a-select v-decorator="['ShipmentId',{ rules: [{ required: true, message: 'Vui lòng chọn mã đơn vận!' }] }]">
                  <a-select-option v-for="shipment in Shipments" :key="shipment.id">{{shipment.id }} </a-select-option>
                </a-select>
              </a-form-item>
              <a-form-item label="Mã item của đơn hàng" class="mb-2">
                <a-input v-decorator="['OrderItemId',{ rules: [{ required: true, message: 'Vui lòng nhập mã item!' }] }]" />
              </a-form-item>
            </div>
            <div class="col-lg-6">
              <a-form-item label="Số lượng" class="mb-2">
                <a-input v-decorator="['Quantity',{rules: [{required: true,type: integer, message: 'Vui lòng nhập số lượng!'} ] }]" />
              </a-form-item>
              <a-form-item label="Mã kho hàng" class="mb-2">
                <a-select v-decorator="['WareHouseId',{ rules: [{ required: true, message: 'Vui lòng chọn mã kho hàng!' }] }]">
                  <a-select-option v-for="wareHouse in WareHouses" :key="wareHouse.id">{{wareHouse.id }} </a-select-option>
                </a-select>
              </a-form-item>
            </div>
          </div>
         
        </a-form>
      </div>
    </div>
  </div>
</template>
<script>
  import axios from 'axios';
  import ShipmentItemApi from './api.js';
  import ShipmentApi from '../shipment/api.js';
  export default {
    mounted() {
      axios.get(ShipmentItemApi.getById + this.$route.params.id)
        .then(res => this.Model = res.data ? res.data : this.Model)

        .then(() => {
          this.CreateForm();
        }),
        axios.post(ShipmentApi.list, {
          Pagination: {
            Total: 0,
            PageIndex: 1
          },
          Sort: {
            Predicate: '',
            Reverse: false
          },
          Search: {
            PredicateObject: {

            }
          },
        })
          .then(res => this.Shipments = res.data.items),
        axios.post("https://localhost:44397/WareHouses/search", {
            Pagination: {
             Total: 0,
             PageIndex: 1
            },
            Sort: {
             Predicate: '',
             Reverse: false
            },
            Search: {
             PredicateObject: {

            }
          },
        }).then(res => this.WareHouses = res.data.items)
    },
    data() {
      return {
        hasErrors: false,
        FrmProduct: null,
        Model: {},
        Shipments: [],
        WareHouses: []
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            return {
              ShipmentId: this.$form.createFormField({ value: this.Model.shipmentId }),
              OrderItemId: this.$form.createFormField({ value: this.Model.orderItemId }),
              Quantity: this.$form.createFormField({ value: this.Model.quantity }),
              WareHouseId: this.$form.createFormField({ value: this.Model.wareHouseId })
        
            };
          }
        }
        this.FrmProduct = this.$form.createForm(this, options);
      },
      GetModel() {
          this.Model.shipmentId = this.FrmProduct.getFieldValue('ShipmentId'),
          this.Model.orderItemId = this.FrmProduct.getFieldValue('OrderItemId'),
          this.Model.quantity = this.FrmProduct.getFieldValue('Quantity'),
          this.Model.wareHouseId = this.FrmProduct.getFieldValue('WareHouseId')
          
      },
      async Save() {
        await this.FrmProduct.validateFields((errors, values) => {
          if (errors) {
            this.hasErrors = true;
            return;
          }
          this.GetModel();
          axios.post(ShipmentItemApi.createOrUpdate, this.Model);
        }
        );
      },
      async Finish() {
        await this.FrmProduct.validateFields((errors, values) => {
          if (errors) {
            this.hasErrors = true;
            return;
          }
          this.GetModel();
          axios.post(ShipmentItemApi.createOrUpdate, this.Model).then(response => {
            if (response) this.$router.replace({ path: "/shipmentItem" });
          });
        }
        );
      },
      Reset() {
        this.FrmProduct.resetFields();
      }
    }
  }
</script>
