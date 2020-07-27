<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">
      <h5>Tạo / cập nhật nhà cung cấp email</h5>
      <router-link :to="{path:'/emailProvider/'}" class="btn btn-primary mb-3" type="primary">
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
              <a-form-item label="Tên" class="mb-2">
                <a-input v-decorator="['Name', { rules: [{ required: true, message: 'Vui lòng nhập tên nhà cung cấp!' }] }]" />
              </a-form-item>
              <a-form-item label="Số lượng giới hạn" class="mb-2">
                <a-input v-decorator="['LimitNumber',{ rules: [{ required: true, message: 'Vui lòng nhập số lượng giới hạn!' }] }]" />
              </a-form-item>
            </div>
            <div class="col-lg-6">
              <a-form-item label="Host" class="mb-2">
                <a-input v-decorator="['Host',{rules: [{required: true, message: 'Vui lòng nhập host'}] }]" />
              </a-form-item>
              <a-form-item label="Cổng" class="mb-2">
                <a-input v-decorator="['Port',{ rules: [{ required: true, message: 'Vui lòng nhập cổng!' }] }]" />
              </a-form-item>
            </div>
          </div>
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
  import EmailProviderApi from './api.js';
  export default {
    mounted() {
      axios.get(EmailProviderApi.getById + this.$route.params.id)
        .then(res => this.Model = res.data ? res.data : this.Model)
        .then(() => {
          this.CreateForm();
        })
    },
    data() {
      return {
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
              LimitNumber: this.$form.createFormField({ value: this.Model.limitNumber }),
              Host: this.$form.createFormField({ value: this.Model.host }),
              Port: this.$form.createFormField({ value: this.Model.port })
            };
          }
        }
        this.FrmProduct = this.$form.createForm(this, options);
      },
      GetModel() {
        this.Model.name = this.FrmProduct.getFieldValue('Name');
        this.Model.limitNumber = this.FrmProduct.getFieldValue('LimitNumber');
        this.Model.host = this.FrmProduct.getFieldValue('Host');
        this.Model.port = this.FrmProduct.getFieldValue('Port');
      },
      async Save() {
        await this.FrmProduct.validateFields((errors, values) => {
          if (errors) return;
          this.$message.success("Tạo / cập nhật thành công");
          this.GetModel();
          axios.post(EmailProviderApi.createOrUpdate, this.Model);
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
          axios.post(EmailProviderApi.createOrUpdate, this.Model).then(response => {
            if (response) this.$router.replace({ path: "/emailprovider" });
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
