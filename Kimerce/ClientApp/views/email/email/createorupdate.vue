<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">
      <h5>Tạo / cập nhật email</h5>
      <router-link :to="{path:'/email/'}" class="btn btn-primary mb-3" type="primary">
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
              <a-form-item label="Tài khoản" class="mb-2">
                <a-select v-decorator="['AccountId',{ rules: [{ required: true, message: 'Vui lòng chọn tài khoản!' }] }]">
                  <a-select-option v-for="account in Accounts" :key="account.id">{{account.email}}</a-select-option>
                </a-select>
              </a-form-item>
            </div>
            <div class="col-lg-6">
              <a-form-item label="Mẫu" class="mb-2">
                <a-select v-decorator="['TemplateId',{ rules: [{ required: true, message: 'Vui lòng chọn mẫu!' }] }]">
                  <a-select-option v-for="template in Templates" :key="template.id">{{template.name}}</a-select-option>
                </a-select>
              </a-form-item>
            </div>

            <div class="col-lg-12">
              <a-form-item label="Chủ đề" class="mb-2">
                <a-textarea v-decorator="['Subject', { rules: [{ required: true, message: 'Vui lòng nhập chủ đề!' }] }]" />
              </a-form-item>
            </div>
            <div class="col-lg-6">
              <a-form-item label="Người nhận" class="mb-2">
                <a-input v-decorator="['To',{rules: [{required: true, message: 'Vui lòng nhập người nhận!'}, {email: true, message: 'Vui lòng nhập một To' }] }]" />
              </a-form-item>
            </div>
            <div class="col-lg-6">
              <a-form-item label="Người gửi" class="mb-2">
                <a-input v-decorator="['From',{ rules: [{ required: true, message: 'Vui lòng nhập người gửi!' }] }]" />
              </a-form-item>
            </div>
            <div class="col-lg-12">
              <a-form-item label="Mô tả đầy đủ" class="mb-2">
                <ckeditor :editor="editor" v-model="Model.body" :config="editorConfig"></ckeditor>
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
  import EmailApi from './api.js';
  import EmailAccountApi from '../emailAccount/api.js';
  import EmailTemplateApi from '../emailTemplate/api.js';
  import CkEditor from '@ckeditor/ckeditor5-build-classic';
  export default {
    mounted() {
      // get email to update
      axios.get(EmailApi.getById + this.$route.params.id)
        .then(res => this.Model = res.data ? res.data : this.Model);
      // get accounts
      axios.post(EmailAccountApi.list, {
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
        this.accountTotalRecords = res.data.totalRecord;
      }).then(() => axios.post(EmailAccountApi.list, {
        Pagination: {
          Total: 0,
          PageIndex: 1,
          PageSize: this.accountTotalRecords
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
        this.Accounts = res.data.items;
      });
      // get templates
      axios.post(EmailTemplateApi.list, {
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
        this.templateTotalRecords = res.data.totalRecord;
      }).then(() => axios.post(EmailTemplateApi.list, {
        Pagination: {
          Total: 0,
          PageIndex: 1,
          PageSize: this.templateTotalRecords
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
        this.Templates = res.data.items;
      }).then(() => {
        this.CreateForm();
      })
    },
    data() {
      return {
        hasErrors: false,
        FrmProduct: null,
        Model: {
          body: ""
        },
        Accounts: [],
        Templates: [],
        editor: CkEditor,
        editorConfig: {

        },
        accountTotalRecords: 0,
        templateTotalRecords: 0
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            return {
              AccountId: this.$form.createFormField({ value: this.Model.accountId }),
              TemplateId: this.$form.createFormField({ value: this.Model.templateId }),
              Subject: this.$form.createFormField({ value: this.Model.subject }),
              To: this.$form.createFormField({ value: this.Model.to }),
              From: this.$form.createFormField({ value: this.Model.from }),
            };
          }
        }
        this.FrmProduct = this.$form.createForm(this, options);
      },
      GetModel() {
        this.Model.accountId = this.FrmProduct.getFieldValue('AccountId');
        this.Model.templateId = this.FrmProduct.getFieldValue('TemplateId');
        this.Model.subject = this.FrmProduct.getFieldValue('Subject');
        this.Model.to = this.FrmProduct.getFieldValue('To');
        this.Model.from = this.FrmProduct.getFieldValue('From');
      },
      async Save() {
        await this.FrmProduct.validateFields((errors, values) => {
          if (errors) {
            this.hasErrors = true;
            return;
          }
          this.$message.success("Tạo / cập nhật thành công");
          this.GetModel();
          axios.post(EmailApi.createOrUpdate, this.Model);
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
          axios.post(EmailApi.createOrUpdate, this.Model).then(response => {
            if (response) this.$router.replace({ path: "/email" });
          });
        }
        );
      },
      Reset() {
        this.FrmProduct.resetFields();
      },
    }
  }
</script>
