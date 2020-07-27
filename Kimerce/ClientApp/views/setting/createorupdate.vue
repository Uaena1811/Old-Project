<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">
      <h5>Tạo / cập nhật cài đặt</h5>
      <router-link :to="{path:'/setting/'}" class="btn btn-primary mb-3" type="primary">
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
          <a-form-item label="Tên" class="mb-2">
            <a-input v-decorator="['Name', { rules: [{ required: true, message: 'Vui lòng nhập tên!' }] }]" />
          </a-form-item>
          <a-form-item label="Tên hệ thống" class="mb-2">
            <a-input v-decorator="['SystemName',{ rules: [{ required: true, message: 'Vui lòng nhập tên hệ thống!' }] }]" />
          </a-form-item>
          <a-form-item label="Giá trị" class="mb-2">
            <a-input v-decorator="['Value',{rules: [{required: true, message: 'Vui lòng nhập giá trị!'}] }]" />
          </a-form-item>
          <div class="row">
            <div class="col-lg-12">

            </div>
          </div>
        </a-form>
      </div>
    </div>
  </div>
</template>
<script>
  import axios from 'axios';
  import SettingApi from './api.js';
  export default {
    mounted() {
      axios.get(SettingApi.getById + this.$route.params.id)
        .then(res => this.Model = res.data ? res.data : this.Model)
        .then(() => {
          this.CreateForm();
        })
    },
    data() {
      return {
        hasErrors: false,
        FrmProduct: null,
        Model: {}
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            return {
              Name: this.$form.createFormField({ value: this.Model.name, }),
              Value: this.$form.createFormField({ value: this.Model.value }),
              SystemName: this.$form.createFormField({ value: this.Model.systemName })
            };
          }
        }
        this.FrmProduct = this.$form.createForm(this, options);
      },
      GetModel() {
        this.Model.name = this.FrmProduct.getFieldValue('Name'),
          this.Model.value = this.FrmProduct.getFieldValue('Value'),
          this.Model.systemName = this.FrmProduct.getFieldValue('SystemName')
      },
      async Save() {
        await this.FrmProduct.validateFields((errors, values) => {
          if (errors) {
            this.hasErrors = true;
            return;
          }
          this.GetModel();
          axios.post(SettingApi.createOrUpdate, this.Model);
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
          axios.post(SettingApi.createOrUpdate, this.Model).then(response => {
            if (response) this.$router.replace({ path: "/setting" });
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
