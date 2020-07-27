<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">
      <h5>Tạo / cập nhật Nhà Kho</h5>
      <router-link :to="{ name:'warehouse'}" class="btn btn-primary mb-3" type="primary">
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
              <a-form-item label="Tên Nhà Kho" class="mb-2">
                <a-input placeholder="Nhập tên Nhà Kho..." v-decorator="['Name', { rules: [{ required: true, message: 'Vui lòng nhập tên Nhà kho!' }] }]" />
              </a-form-item>
              <a-form-item label="Tên người liên hệ" class="mb-2">
                <a-input placeholder="Nhập tên người liên hệ..." v-decorator="['ContactName',{ rules: [{ required: true, message: 'Vui lòng nhập Tên người liên hệ!' }] }]" />
              </a-form-item>
              <a-form-item label="SĐT liên hệ" class="mb-2">
                <a-input placeholder="Nhập số điện thoại người liên hệ..." v-decorator="['PhoneNumber',{ rules: [{ required: true, message: 'Vui lòng nhập SĐT liên hệ!' }] }]" />
              </a-form-item>
            </div>
            <div class="col-lg-6">
              <a-form-item label="Address" class="mb-2">
                <a-cascader v-decorator="['ListId',{ rules: [{ required: true, message: 'Vui lòng chọn Địa chỉ!'},
                                                             { validator: check,}] }]"
                            :options="options"
                            :fieldNames="fieldNames"
                            @change="onChange"
                            :loadData="loadData"
                            placeholder="Chọn địa chỉ..." 
                            changeOnSelect />
              </a-form-item>
              <a-form-item label="Vĩ độ" class="mb-2">
                <a-input-number placeholder="Vĩ độ..." :step="0.1" v-decorator="['Latitude',{ rules: [{ required: true, message: 'Vui lòng nhập Vĩ độ!' }] }]" />
              </a-form-item>
              <a-form-item label="Kinh độ" class="mb-2">
                <a-input-number placeholder="Kinh độ..." :step="0.1" v-decorator="['Longtitude',{ rules: [{ required: true, message: 'Vui lòng nhập Kinh độ!' }] }]" />
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
  import CityApi from '../location/city/api';
  import DistrictApi from '../location/district/api';
  import WardApi from '../location/ward/api';
  import WareHouseApi from './api';

  export default {
    created() {
      axios.get(WareHouseApi.getModel + this.$route.params.id)
        .then(res => {
          this.Model = res.data ? res.data : this.Model;
          this.ward.id = res.data.parentId;
        })
        .then(() => {
          this.CreateForm();
        })

      axios.get(WardApi.getModel + this.ward.id)
        .then(res => {
          this.ward.id = res.data.id;
          this.ward.name = res.data.name;
        });
    },
    mounted() {
      axios.post(CityApi.list,
        {
          "pagination": {
            "pageIndex": 1,
          },
          "search": {
            "predicateObject": {}
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
        ward: {
          name: "",
          id: 0,
        },
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
                Name: this.$form.createFormField({ value: this.Model.name, }),
                ListId: this.$form.createFormField({ value: [this.Model.ward.district.city.id, this.Model.ward.district.id, this.Model.parentId], }),
                Latitude: this.$form.createFormField({ value: this.Model.latitude, }),
                Longtitude: this.$form.createFormField({ value: this.Model.longtitude, }),
                PhoneNumber: this.$form.createFormField({ value: this.Model.phoneNumber, }),
                ContactName: this.$form.createFormField({ value: this.Model.contactName, }),
              };
            } else {
              return {
                Name: this.$form.createFormField({ value: this.Model.name, }),
                ListId: [],
                Latitude: this.$form.createFormField({ value: this.Model.latitude, }),
                Longtitude: this.$form.createFormField({ value: this.Model.longtitude, }),
                PhoneNumber: this.$form.createFormField({ value: this.Model.phoneNumber, }),
                ContactName: this.$form.createFormField({ value: this.Model.contactName, }),
              };
            } 
          }
        }
        this.FrmProduct = this.$form.createForm(this, options);
      },
      GetModel() {
        this.Model.name = this.FrmProduct.getFieldValue('Name'),
        this.listId = this.FrmProduct.getFieldValue('ListId');
        this.Model.parentId = this.listId[2];
        console.log(this.Model.parentId);
        this.Model.latitude = this.FrmProduct.getFieldValue('Latitude')
        this.Model.longtitude = this.FrmProduct.getFieldValue('Longtitude')
        this.Model.phoneNumber = this.FrmProduct.getFieldValue('PhoneNumber')
        this.Model.contactName = this.FrmProduct.getFieldValue('ContactName')
      },
      async Save() {
        await this.FrmProduct.validateFields((errors, values) => {
          if (errors) {
            this.hasErrors = true;
            return;
          }
          this.GetModel();
          axios.post(WareHouseApi.createOrUpdate, this.Model, {
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
          console.log(this.Model);
          axios.post(WareHouseApi.createOrUpdate, this.Model, {
            headers: {
              "Content-Type": "application/json"
            }
          }).then(response => {
            if (response.data.result == 1) {
              this.$message.success("Tạo / Cập nhật thành công!");
              if (response) this.$router.replace("/warehouse");
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
        if (value.length < 3) {
          callback("Vui lòng chọn Tỉnh/Huyện/Xã!");
        } else {
           callback();
        }
      },
      loadData(selectedOptions) {
        const targetOption = selectedOptions[selectedOptions.length - 1];
        targetOption.loading = true;

        // load options lazily
        setTimeout(() => {
          if (selectedOptions.length == 1) {
            axios.get(CityApi.getChildren + targetOption.id).then(r => {
              targetOption.loading = false;
              targetOption.children = r.data;
              console.log(targetOption.children);
              for (let i = 0; i < targetOption.children.length; i++) {
                targetOption.children[i].isLeaf = false;
              }
              this.options = [...this.options];
            });
          } else if (selectedOptions.length == 2) {
            axios.get(DistrictApi.getChildren + targetOption.id).then(r => {
            targetOption.loading = false;
            targetOption.children = r.data;
            console.log(targetOption.children);
            for (let i = 0; i < targetOption.children.length; i++) {
                targetOption.children[i].isLeaf = true;
            }
            this.options = [...this.options];
          });
          }
        }, 1000);
      },
    }
  }
</script>
