<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">
      <h5>Tạo / cập nhật tài khoản email</h5>
      <router-link :to="{path:'/emailAccount/'}" class="btn btn-primary mb-3" type="primary">
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
            <div class="col-lg-12">
              <a-form-item label="Nhà cung cấp" class="mb-2">
                <a-select v-decorator="['ProviderId',{ rules: [{ required: true, message: 'Vui lòng chọn nhà cung cấp!' }] }]">
                  <a-select-option v-for="provider in Providers" :key="provider.id">{{provider.name}}</a-select-option>
                </a-select>
              </a-form-item>
            </div>
          </div>
          <div class="row">
            <div class="col-lg-6">
              <a-form-item label="Email" class="mb-2">
                <a-input v-decorator="['Email',{rules: [{required: true, message: 'Vui lòng nhập Email!'}, {type: 'email', message: 'Email không hợp lệ' }] }]" />
              </a-form-item>
              <a-form-item label="Tên người dùng" class="mb-2">
                <a-input v-decorator="['UserName', { rules: [{ required: true, message: 'Vui lòng nhập tên người dùng!' }] }]" />
              </a-form-item>
            </div>

            <div class="col-lg-6">
              <a-form-item label="Mật khẩu" class="mb-2">
                <a-input v-decorator="['Password',{ rules: [{ required: true, message: 'Vui lòng nhập mật khẩu!' }] }]" />
              </a-form-item>
              <a-form-item label="Tên hiển thị" class="mb-2">
                <a-input v-decorator="['DisplayName',{ rules: [{ required: true, message: 'Vui lòng nhập tên hiển thị!' }] }]" />
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
  import EmailAccountApi from './api.js';
  import EmailProviderApi from '../emailProvider/api.js';
  export default {
    mounted() {
      axios.get(EmailAccountApi.getById + this.$route.params.id)
        .then(res => this.Model = res.data ? res.data : this.Model);

      axios.post(EmailProviderApi.list, {
        Pagination: {
          Total: 0,
          PageIndex: 1,
          PageSize: 1
        },
        Sort: {
          Predicate: '',
          Reverse: false
        },
        Search: {
          PredicateObject: {

          }
        },
      }).then(res => {
        this.providerTotalRecords = res.data.totalRecord;
      }).then(() => axios.post(EmailProviderApi.list, {
        Pagination: {
          Total: 0,
          PageIndex: 1,
          PageSize: this.providerTotalRecords
        },
        Sort: {
          Predicate: '',
          Reverse: false
        },
        Search: {
          PredicateObject: {

          }
        },
      })).then(res => {
        this.Providers = res.data.items;
      })
        .then(() => {
          this.CreateForm();
        });
    },
    data() {
      return {
        hasErrors: false,
        FrmProduct: null,
        Model: {},
        Providers: [],
        providerTotalRecords: 0
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            return {
              ProviderId: this.$form.createFormField({ value: this.Model.providerId }),
              UserName: this.$form.createFormField({ value: this.Model.userName }),
              Email: this.$form.createFormField({ value: this.Model.email }),
              DisplayName: this.$form.createFormField({ value: this.Model.displayName }),
              Password: this.$form.createFormField({ value: this.Model.password })
            };
          }
        }
        this.FrmProduct = this.$form.createForm(this, options);
      },
      GetModel() {
        this.Model.providerId = this.FrmProduct.getFieldValue('ProviderId');
        this.Model.userName = this.FrmProduct.getFieldValue('UserName');
        this.Model.email = this.FrmProduct.getFieldValue('Email');
        this.Model.displayName = this.FrmProduct.getFieldValue('DisplayName');
        this.Model.password = this.FrmProduct.getFieldValue('Password');
      },
      async Save() {
        await this.FrmProduct.validateFields((errors, values) => {
          if (errors) {
            this.hasErrors = true;
            return;
          }
          this.$message.success("Tạo / cập nhật thành công");
          this.GetModel();
          axios.post(EmailAccountApi.createOrUpdate, this.Model);
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
          axios.post(EmailAccountApi.createOrUpdate, this.Model).then(response => {
            if (response) this.$router.replace({ path: "/emailaccount" });
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
