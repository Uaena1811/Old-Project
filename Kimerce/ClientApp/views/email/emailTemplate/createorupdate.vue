<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">
      <h5>Tạo / cập nhật mẫu email</h5>
      <router-link :to="{path:'/emailTemplate/'}" class="btn btn-primary mb-3" type="primary">
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
            <a-input v-decorator="['Name', { rules: [{ required: true, message: 'Vui lòng nhập tên mẫu!' }] }]" />
          </a-form-item>
          <a-form-item label="File" class="mb-2">
            <a-input v-decorator="['FilePath',{ rules: [{ required: true, message: 'Vui lòng nhập đường dẫn file!' }] }]" />
          </a-form-item>
          <a-form-item label="Drive" class="mb-2">
            <a-input v-decorator="['Drive',{rules: [{required: true, message: 'Vui lòng nhập đường dẫn drive'}] }]" />
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
  import EmailTemplateApi from './api.js';
  export default {
    mounted() {
      axios.get(EmailTemplateApi.getById + this.$route.params.id)
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
              FilePath: this.$form.createFormField({ value: this.Model.filePath }),
              Drive: this.$form.createFormField({ value: this.Model.drive }),
            };
          }
        }
        this.FrmProduct = this.$form.createForm(this, options);
      },
      GetModel() {
        this.Model.name = this.FrmProduct.getFieldValue('Name');
        this.Model.filePath = this.FrmProduct.getFieldValue('FilePath');
        this.Model.drive = this.FrmProduct.getFieldValue('Drive');
      },
      async Save() {
        await this.FrmProduct.validateFields((errors, values) => {
          if (errors) return;
          this.GetModel();
          axios.post(EmailTemplateApi.createOrUpdate, this.Model);
          this.$message.success("Tạo / cập nhật thành công");
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
          axios.post(EmailTemplateApi.createOrUpdate, this.Model).then(response => {
            if (response) this.$router.replace({ path: "/emailtemplate" });
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
