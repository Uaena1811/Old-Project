<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">
      <h5>Tạo đơn hàng</h5>
      <router-link :to="{path:'/shipment/'}" class="btn btn-primary mb-3" type="primary">
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
              <a-form-item label="Mã đơn hàng" class="mb-2">
                <a-select v-decorator="['OrderId',{ rules: [{ required: true, message: 'Vui lòng chọn mã đơn hàng!' }] }]">
                  <a-select-option v-for="order in Orders" :key="order.id">{{order.id }} </a-select-option>
                </a-select>
              </a-form-item>
              <a-form-item label="Code Tracking Đơn Hàng" class="mb-2">
                <a-input v-decorator="['TrackingNumber',{ rules: [{ required: true, message: 'Vui lòng nhập code tracking đơn hàng!' }] }]" />
              </a-form-item>
            </div>
            <div class="col-lg-6">
              <a-form-item label="Tổng trọng lượng" class="mb-2">
                <a-input v-decorator="['TotalWeight',{rules: [{required: true, message: 'Vui lòng nhập tổng trọng lượng!'} ] }]" />
              </a-form-item>
              <a-form-item label="Trạng thái giao hàng" class="mb-2">
                <a-select v-decorator="['DeliveryStatus',{ rules: [{ required: true, message: 'Vui lòng chọn trạng thái giao hàng!' }] }]">
                  <a-select-option v-for="deliveryStatus in DeliveryStatus" :key="deliveryStatus.key">{{ deliveryStatus.message }} </a-select-option>
                </a-select>
              </a-form-item>
            </div>
          </div>
          <div class="row">
            <div class="col-lg-12">
              <a-form-item label="Ngày giao hàng thành công" class="mb-2">
                <a-date-picker v-decorator="['CompleteDate',{ rules: [{ required: true, message: 'Vui lòng nhập ngày giao thành công!' }] }]" />
              </a-form-item>
              <a-form-item label="Ngày bàn giao hàng" class="mb-2">
                <a-date-picker v-decorator="['HandoverDate',{ rules: [{ required: true, message: 'Vui lòng nhập ngày bàn giao hàng' }] }]" />
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
  import ShipmentApi from './api.js';
  import OrderApi from '../order/api.js';
  import moment from 'moment';
  export default {
    mounted() {
      axios.get(ShipmentApi.getById + this.$route.params.id)
        .then(res => this.Model = res.data ? res.data : this.Model)

        .then(() => {
          this.CreateForm();
        }),
        axios.post(OrderApi.list, {
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
          .then(res => this.Orders = res.data.items)
    },
    data() {
      return {
        hasErrors: false,
        FrmProduct: null,
        Model: {},
        Orders: [],
        DeliveryStatus: [
          {
            key: 1,
            message: "Chờ bàn giao"
          },
          {
            key:2,
            message: "Đã bàn giao"
          },
          {
            key:3,
            message: "Đang vận chuyển"
          },
          {
            key:4,
            message: "Đang giao hàng"
          },
          {
            key:5,
            message: "Đang trả về"
          },
          {
            key:6,
            message: "Đã giao hàng"
          },
          {
            key:7,
            message: "Đã trả về"
          }]
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            return {
              OrderId: this.$form.createFormField({ value: this.Model.orderId }),
              TrackingNumber: this.$form.createFormField({ value: this.Model.trackingNumber }),
              TotalWeight: this.$form.createFormField({ value: this.Model.totalWeight }),
              DeliveryStatus: this.$form.createFormField({ value: this.Model.deliveryStatus }),
              CompleteDate: this.$form.createFormField({ value: moment(this.Model.completeDate) }),
              HandoverDate: this.$form.createFormField({ value: moment(this.Model.handoverDate) })
            };
          }
        }
        this.FrmProduct = this.$form.createForm(this, options);
      },
      GetModel() {
        this.Model.orderId = this.FrmProduct.getFieldValue('OrderId'),
          this.Model.trackingNumber = this.FrmProduct.getFieldValue('TrackingNumber'),
          this.Model.totalWeight = this.FrmProduct.getFieldValue('TotalWeight'),
          this.Model.deliveryStatus = this.FrmProduct.getFieldValue('DeliveryStatus'),
          this.Model.completeDate = this.FrmProduct.getFieldValue('CompleteDate'),
          this.Model.handoverDate = this.FrmProduct.getFieldValue('HandoverDate')
      },
      async Save() {
        await this.FrmProduct.validateFields((errors, values) => {
          if (errors) {
            this.hasErrors = true;
            return;
          }
          this.GetModel();
          axios.post(ShipmentApi.createOrUpdate, this.Model);
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
          axios.post(ShipmentApi.createOrUpdate, this.Model).then(response => {
            if (response) this.$router.replace({ path: "/shipment" });
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
