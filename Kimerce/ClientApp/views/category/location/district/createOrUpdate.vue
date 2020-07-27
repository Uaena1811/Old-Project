<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">
      <h5>Tạo / cập nhật Huyện</h5>
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
              <a-form-item label="Tỉnh" class="mb-2">
                <a-cascader v-decorator="['ListId',{ rules: [{ required: true, message: 'Vui lòng chọn Tỉnh!' }] }]"
                            :options="options"
                            :fieldNames="fieldNames"
                            @change="onChange"
                            :loadData="loadData"
                            placeholder="Chọn Tỉnh..."
                            changeOnSelect />
              </a-form-item>
              <a-form-item label="Tên Quận/Huyện" class="mb-2">
                <a-input placeholder="Nhập tên Quận/Huyện..." v-decorator="['Name', { rules: [{ required: true, message: 'Vui lòng nhập tên Quận/Huyện!' }] }]" />
              </a-form-item>
              <a-form-item label="Mã Code" class="mb-2">
                <a-input placeholder="Nhập Mã code..." v-decorator="['Code',{ rules: [{ required: true, message: 'Vui lòng nhập mã Code!' }] }]" />
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
  import DistrictApi from './api';
  import CityApi from '../city/api';

  export default {
    created() {
      axios.get(DistrictApi.getModel + this.$route.params.id)
        .then(res => this.Model = res.data ? res.data : this.Model)
        .then(() => {
          this.CreateForm();
        })
    },
    mounted() {
      axios.post(CityApi.list,
        {
          "pagination": {
            "pageIndex": 1,
          },
          "search": {
            "predicateObject": {
            }
          }
        }
      ).then(r => {
        this.options = r.data.items;
      });
    },
    data() {
      return {
        hasErrors: false,
        FrmProduct: null,
        Model: {},
        options: [],
        listId: [],
        fieldNames: {
          label: "name",
          value: "id",
          children: "children"
        },
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            if (this.Model.id > 0) {
              return {
                ListId: this.$form.createFormField({ value: [this.Model.city.id], }),
                Name: this.$form.createFormField({ value: this.Model.name, }),
                Code: this.$form.createFormField({ value: this.Model.code, }),
              };
            } else {
              return {
                ListId: [],
                Name: this.$form.createFormField({ value: this.Model.name, }),
                Code: this.$form.createFormField({ value: this.Model.code, }),
              };
            }
          }
        }
        this.FrmProduct = this.$form.createForm(this, options);
      },
      GetModel() {
        this.listId = this.FrmProduct.getFieldValue('ListId');
        this.Model.parentId = this.listId[0];
        this.Model.name = this.FrmProduct.getFieldValue('Name'),
          this.Model.code = this.FrmProduct.getFieldValue('Code')
      },
      async Save() {
        await this.FrmProduct.validateFields((errors, values) => {
          if (errors) {
            this.hasErrors = true;
            return;
          }
          this.GetModel();
          axios.post(DistrictApi.createOrUpdate, this.Model, {
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
          axios.post(DistrictApi.createOrUpdate, this.Model, {
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
      },
      onChange(value) {
        console.log("value");
      },
      loadData(selectedOptions) {
       
      },
    }
  }
</script>
