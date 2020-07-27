<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">

      <h5>Tạo / cập nhật giảm giá</h5>
    </div>
    <div class="row">
      <div class="col-lg-6">
        <router-link to="/discount">
          <a-button class="btn btn-primary" type="primary" icon="arrow-left">
            Danh sách giảm giá
          </a-button>
        </router-link>
      </div>
      <div class="col-lg-12 text-right">
        <a-button class="btn btn-primary mb-3" type="primary" html-type="button" icon="check" @click="Finish">Lưu lại</a-button>
        <a-button class="btn btn-primary mb-3" type="primary" html-type="button" icon="save" @click="Save">Lưu và sửa tiếp</a-button>
        <a-button class="btn btn-danger mb-3" type="danger" html-type="button" icon="undo" @click="Reset">Reset thông tin</a-button>
      </div>
    </div>
    <div class="card overflow-hidden">
      <div class="card-body">
        <a-form layout="vertical" :form="FrmDiscount">
          <a-form-item label="Tên giảm giá" class="mb-2">
            <a-input v-decorator="['Name', { rules: [{ required: true, message: 'Vui lòng nhập tên giảm giá!' }] }]" />
          </a-form-item>
          <a-form-item label="Use Percentage" class="mb-2">
            <a-checkbox v-decorator="['UsePercentage']" :checked="Model.UsePercentage" @change="OnChange" />
          </a-form-item>
          <div v-if="Model.UsePercentage == true">
            <a-form-item label="Discount Percentage" class="mb-2">
              <a-input-number v-decorator="['DiscountPercentage', { rules: [{ required: true, message: 'Vui lòng nhập số tự nhiên!' }] }]" />
            </a-form-item>
          </div>
          <a-form-item label="Discount Amount" class="mb-2">
            <a-input-number v-decorator="['DiscountAmount', { rules: [{ required: true, message: 'Vui lòng nhập số tự nhiên!' }] }]" />
          </a-form-item>
          <a-form-item label="Max Discount Amount" class="mb-2">
            <a-input-number v-decorator="['MaxDiscountAmount', { rules: [{ required: true, message: 'Vui lòng nhập số tự nhiên!' }] }]" />
          </a-form-item>
          <a-form-item label="Date"
                       style="margin-bottom:0;">
            <a-form-item :style="{ display: 'inline-block', width: 'calc(50% - 12px)' }">
              <a-date-picker v-model="Model.StartDate" style="width: 100%" />
            </a-form-item>
            <span :style="{ display: 'inline-block', width: '24px', textAlign: 'center' }">-</span>
            <a-form-item :style="{ display: 'inline-block', width: 'calc(50% - 12px)' }">
              <a-date-picker v-model="Model.EndDate" style="width: 100%" />
            </a-form-item>
          </a-form-item>
          <a-form-item label="Require Coupon Code" class="mb-2">
            <a-checkbox v-decorator="['RequireCouponCode']" :checked="Model.RequireCouponCode" @change="OnChhange" />
          </a-form-item>
          <div v-if="Model.RequireCouponCode == true">
            <a-form-item label="Mã giảm giá" class="mb-2">
              <a-input v-decorator="['CouponCode', { rules: [{ required: true, message: 'Vui lòng nhập tên giảm giá!' }] }]" />
            </a-form-item>
          </div>
          <a-form-item label="Quantity" class="mb-2">
            <a-input-number v-decorator="['Quantity', { rules: [{ required: true, message: 'Vui lòng nhập số tự nhiên!' }] }]" />
          </a-form-item>
        </a-form>
      </div>
    </div>
    <!--<h6 class="mb-3 text-uppercase">
      <strong>Sản phẩm liên quan</strong>
    </h6>-->
  </div>
</template>
<script>
  import CkEditor from '@ckeditor/ckeditor5-build-classic';
  import DiscountApi from './api.js';
  import axios from 'axios';
  export default {
    mounted() {
      axios.get(DiscountApi.getById + this.$route.params.id)
        .then(res => this.Model = res.data ? res.data : this.Model)
        .then(() => {
          this.CreateForm();
        })
    },
    data() {
      return {
        hasErrors: false,
        FrmDiscount: null,
        Model: {
          RequireCouponCode: false,
          UsePercentage: false,
          StartDate: "",
          EndDate: "",
        },
        editor: CkEditor,
        editorConfig: {

        }
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            return {
              Name: this.$form.createFormField({ value: this.Model.name, }),
              UsePercentage: this.$form.createFormField({ value: this.Model.usePercentage }),
              DiscountPercentage: this.$form.createFormField({ value: this.Model.discountPercentage }),
              DiscountAmount: this.$form.createFormField({ value: this.Model.discountAmount }),
              MaxDiscountAmount: this.$form.createFormField({ value: this.Model.maxDiscountAmount }),
              StartDateDisplay: this.$form.createFormField({ value: this.Model.startDateDisplay }),
              EndDateDisplay: this.$form.createFormField({ value: this.Model.endDateDisplay }),
              RequireCouponCode: this.$form.createFormField({ value: this.Model.requireCouponCode }),
              CouponCode: this.$form.createFormField({ value: this.Model.CouponCode }),
              Quantity: this.$form.createFormField({ value: this.Model.quantity })
            };
          }
        }
        this.FrmDiscount = this.$form.createForm(this, options);
      },
      GetModel() {
        this.Model.name = this.FrmDiscount.getFieldValue('Name'),
          this.Model.usePercentage = this.FrmDiscount.getFieldValue('UsePercentage'),
          this.Model.discountPercentage = this.FrmDiscount.getFieldValue('DiscountPercentage'),
          this.Model.discountAmount = this.FrmDiscount.getFieldValue('DiscountAmount'),
          this.Model.maxDiscountAmount = this.FrmDiscount.getFieldValue('MaxDiscountAmount'),
          this.Model.startDateDisplay = this.FrmDiscount.getFieldValue('StartDateDisplay')
        this.Model.requireCouponCode = this.FrmDiscount.getFieldValue('RequireCouponCode'),
          this.Model.endDateDisplay = this.FrmDiscount.getFieldValue('EndDateDisplay'),
          this.Model.couponCode = this.FrmDiscount.getFieldValue('CouponCode'),
          this.Model.quantity = this.FrmDiscount.getFieldValue('Quantity')
      },
      async Save() {
        await this.FrmDiscount.validateFields((errors, values) => {
          if (errors) {
            this.hasErrors = true;
            return;
          }
          this.GetModel();
          axios.post(DiscountApi.createOrUpdate, this.Model);
        }
        );
      },
      async Finish() {
        await this.FrmDiscount.validateFields((errors, values) => {
          if (errors) {
            this.hasErrors = true;
            return;
          }
          this.GetModel();
          axios.post(DiscountApi.createOrUpdate, this.Model).then(response => {
            if (response) this.$router.replace({ path: "/discount" });
          });
        }
        );
      },
      Reset() {
        this.FrmDiscount.resetFields();
      },
      OnChange(e) {
        this.Model.UsePercentage = !this.Model.UsePercentage;
      },
      OnChhange(e) {
        this.Model.RequireCouponCode = !this.Model.RequireCouponCode;
      }
    }
  }
</script>
