<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">
      <h5>Tạo / cập nhật Tỉnh</h5>
      <router-link :to="{ name:'locationManager'}" class="btn btn-primary mb-3" type="primary">
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
            <div class="col-lg-8 mx-auto">
              <a-form-item label="Tên Tỉnh/Thành Phố" class="mb-2">
                <a-input placeholder="Nhập tên Tỉnh/Thành..." v-decorator="['Name', { rules: [{ required: true, message: 'Vui lòng nhập tên Tỉnh/Thành Phố!' }] }]" />
              </a-form-item>
              <a-form-item label="Mã Code" class="mb-2">
                <a-input placeholder="Nhập Mã code..." v-decorator="['Code',{ rules: [{ required: true, message: 'Vui lòng nhập mã Code!' }] }]" />
              </a-form-item>
              <a-form-item label="Miền" class="mb-2">
                <a-select placeholder="Chọn Vùng miền..." v-decorator="['CityRealm',{ rules: [{ required: true, message: 'Vui lòng chọn Vùng miền!' }] }]" :options="realms" />
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
  import CityApi from './api';
  export default {
    mounted() {
      axios.get(CityApi.getModel + this.$route.params.id)
        .then(res => this.Model = res.data ? res.data : this.Model)
        .then(() => {
          this.CreateForm();
        })
    },
    data() {
      return {
        hasErrors: false,
        FrmProduct: null,
        Model: {},
        realms: [
          { label: "Miền Bắc", value: 1 },
          { label: "Miền Trung", value: 2 },
          { label: "Miền Nam", value: 3 },
          { label: "Hà Nội", value: 4 },
          { label: "Hồ Chí Minh", value: 5 },
        ],
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            return {
              Name: this.$form.createFormField({ value: this.Model.name, }),
              Code: this.$form.createFormField({ value: this.Model.code }),
              CityRealm: this.$form.createFormField({ value: this.Model.cityRealm }),
            };
          }
        }
        this.FrmProduct = this.$form.createForm(this, options);
      },
      GetModel() {
        this.Model.name = this.FrmProduct.getFieldValue('Name'),
          this.Model.code = this.FrmProduct.getFieldValue('Code'),
          this.Model.cityRealm = this.FrmProduct.getFieldValue('CityRealm')
      },
      async Save() {
        await this.FrmProduct.validateFields((errors, values) => {
          if (errors) {
            this.hasErrors = true;
            return;
          }
          this.GetModel();
          axios.post(CityApi.createOrUpdate, this.Model, {
              headers: {
                "Content-Type": "application/json"
              }
            }).then(response => {
              if (response.data.result == 1) {
                this.$message.success("Tạo / Cập nhật thành công!");
              }
              else {
                this.$message.error(response.data.message);
              }
            })
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
          axios.post(CityApi.createOrUpdate, this.Model, {
              headers: {
                "Content-Type": "application/json"
              }
            }).then(response => {
              if (response.data.result == 1) {
                this.$message.success("Tạo / Cập nhật thành công!");
                if (response) this.$router.replace("/locationManager");
              }
              else {
                this.$message.error(response.data.message);
              }
            })
          }
        );
      },
      Reset() {
        this.FrmProduct.resetFields();
      }
    }
  }
</script>
