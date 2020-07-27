<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">
      <h5>Tạo / cập nhật Xã/Phường</h5>
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
              <a-form-item label="Tỉnh/Huyện" class="mb-2">
                <a-cascader v-decorator="['ListId',{ rules: [{ required: true, message: 'Vui lòng nhập tên Tỉnh/Thành Phố!'},
                                                             { validator: check,}] }]"
                            :options="options"
                            :fieldNames="fieldNames"
                            @change="onChange"
                            :loadData="loadData"
                            placeholder="Chọn Tỉnh/Huyện..."
                            changeOnSelect />
              </a-form-item>
              <a-form-item label="Tên Xã/Phường" class="mb-2">
                <a-input placeholder="Nhập tên Xã/Phường..." v-decorator="['Name', { rules: [{ required: true, message: 'Vui lòng nhập tên Xã/Phường!' }] }]" />
              </a-form-item>
              <a-form-item label="Mã code" class="mb-2">
                <a-input placeholder="Nhập mã code..." v-decorator="['Code',{ rules: [{ required: true, message: 'Vui lòng nhập mã Code!' }] }]" />
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
  import WardApi from './api';
  import CityApi from '../city/api.js';

  export default {
    created() {
      axios.get(WardApi.getModel + this.$route.params.id)
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
        for (let i = 0; i < this.options.length; i++) {
          this.options[i].isLeaf = false;
        }
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
          children: "children",
        },
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            if (this.Model.id > 0) {
              return {
                ListId: this.$form.createFormField({ value: [this.Model.district.city.id, this.Model.parentId], }),
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
        this.Model.parentId = this.listId[1];
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
          axios.post(WardApi.createOrUpdate, this.Model, {
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
          axios.post(WardApi.createOrUpdate, this.Model, {
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
        console.log(value);
      },
      check(rule, value, callback) {
        const form = this.form;
        console.log(value.length);
        if (value.length < 2) {
          callback("Vui lòng chọn Tỉnh/Huyện!");
        } else {
           callback();
        }
      },
      loadData(selectedOptions) {
        const targetOption = selectedOptions[selectedOptions.length - 1];
        targetOption.loading = true;

        // load options lazily
        setTimeout(() => {
          axios.get(CityApi.getChildren + targetOption.id).then(r => {
          targetOption.loading = false;
          targetOption.children = r.data;
          console.log(targetOption.children);
          for (let i = 0; i < targetOption.children.length; i++) {
            targetOption.children[i].isLeaf = true;
          }
          this.options = [...this.options];
        });
        }, 1000);
      },
    }
  }
</script>
