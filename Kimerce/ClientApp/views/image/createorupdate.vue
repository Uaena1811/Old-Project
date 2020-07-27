<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">

      <h5>Tạo / cập nhật hình ảnh</h5>
    </div>
    <div class="row">
      <a-form layout="vertical" :form="Form">
        <div class="col-lg-12 text-right">
          <router-link to="/image">
            <a-button class="btn btn-primary mb-3" type="primary">
              <a-icon type="rollback" />
              Trở lại
            </a-button>
          </router-link>
          <a-button class="btn btn-primary mb-3" type="primary" html-type="button" icon="save" @click="SaveAndFinish">Lưu lại</a-button>
          <a-button class="btn btn-primary mb-3" type="primary" html-type="button" icon="save" @click="Save">Lưu và sửa tiếp</a-button>
          <a-button class="btn btn-danger mb-3" type="danger" html-type="button" icon="save" @click="Reset">Reset thông tin</a-button>
        </div>
        <div class="card overflow-hidden">
          <div class="card-body">
            <div class="row">

              <div class="col-lg-4">
                <a-form-item label=" Name" class="mb-2">
                  <a-input style="width: 100%"
                                  v-decorator="['Name', {
                                  rules: [
                                  {
                                    required: true,
                                    message: 'Vui lòng nhập Name!'
                                  }]
                                  }]" />
                </a-form-item>
              </div>

              <div class="col-lg-4">
                <a-form-item label="Path" class="mb-2">
                  <a-input style="width: 100%"
                                  v-decorator="['Path', {
                                  rules: [
                                  {
                                    required: true,
                                    message: 'Vui lòng nhập Path'
                                  }]
                                  }]" />
                </a-form-item>
              </div>

              <div class="col-lg-8">
                <a-form-item label="Uri" has-feedback>
                  <a-input v-decorator="['Uri',{ rules: [{ required: true, message: 'Vui lòng nhập uri!' }] }]"/>


                </a-form-item>
              </div>

              <div class="col-lg-8">
                <a-form-item label="Domain" has-feedback>
                  <a-input v-decorator="['Domain',{ rules: [{ required: true, message: 'Vui lòng nhập Domain!' }] }]"/>
                </a-form-item>
              </div>


              <div class="col-lg-8">
                <a-form-item label="Drive">
                  <!-- <a-select v-decorator="['UserId',{ rules: [{ required: true, message: 'Vui lòng nhập người dùng!' }] }]">

                       <a-select-option v-for="cter in users" :key="cter.value" :value="cter.value">
                       
                       </a-select-option>
                     </a-select>-->
                  <a-input v-decorator="['Driver',{ rules: [{ required: true, message: 'Vui lòng nhập Drive!' }] }]"/>
                </a-form-item>
              </div>

            </div>
          </div>
          
        </div>
      </a-form>
    </div>
  </div>
</template>
<script src="https://unpkg.com/axios/dist/axios.min.js"></script>

<script>
   import Axios from 'axios';
 
  export default {
    created() {
      Axios.get("https://localhost:44397/images/Getbyid/" + this.$route.params.id).then(r => {
        this.Model.Name = r.data.name;
        this.Model.Path = r.data.path;
        this.Model.Uri = r.data.uri;
        this.Model.Domain = r.data.domain;
        this.Model.Driver = r.data.driver;
        this.Model.Id = r.data.id;
      }).then(() => {
        this.CreateForm();
      });
    },
    mounted() {
     
      
    },
    data() {
      return {
        Form: null,
        Model: {
          Name: "",
          Path: "",
          Uri: "",
          Domain: "",
          Driver: ""



        },
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            return {
              Name: this.$form.createFormField({ value: this.Model.Name, }),
              Path: this.$form.createFormField({ value: this.Model.Path, }),
              Uri: this.$form.createFormField({ value: this.Model.Uri, }),
              Domain: this.$form.createFormField({ value: this.Model.Domain, }),
              Driver: this.$form.createFormField({ value: this.Model.Driver, }),


              
            };
          }
        }
        this.Form = this.$form.createForm(this, options);
      },
      GetModel() {
        this.Model.Name = this.Form.getFieldValue('Name');
        this.Model.Path = this.Form.getFieldValue('Path');
        this.Model.Uri = this.Form.getFieldValue('Uri');
        this.Model.Domain = this.Form.getFieldValue('Domain');
        this.Model.Driver = this.Form.getFieldValue('Driver');


      },
      async SaveAndFinish() {
        this.Form.validateFieldsAndScroll((errors, values) => {
          if (!errors) {
            this.GetModel();
            
            Axios.post("https://localhost:44397/images/createorupdate/", this.Model).then(r => {
              if (r.data.result != 1) {
                this.$message.error(r.data.message);
              }
              else {
                this.$message.success('Lưu dữ liệu thành công', 3);
                this.$router.replace("/image");
              }
            }).catch(error => {
              this.$message.error('Không thể kết nối tới máy chủ', 3);
              console.log(error);
            });
          }
        });
      },
      Save() {
        this.Form.validateFieldsAndScroll((errors, values) => {
          if (!errors) {
            this.GetModel();
            Axios.post("https://localhost:44397/images/createorupdate/", this.Model).then(r => {
              if (r.data.result != 1) {
                this.$message.error(r.data.message);
              }
              else {
                this.$message.success('Lưu dữ liệu thành công', 3);
              }
            }).catch(error => {
              this.$message.error('Không thể kết nối tới máy chủ', 3);
              console.log(error);
            });
          }
        });
      },
      Reset() {
        Axios.get("https://localhost:44397/images/getbyid/" + this.$route.params.id).then(r => {
          this.Form.setFieldsValue({
            Name : r.data.name,
        Path : r.data.path,
        Uri : r.data.uri,
       Domain : r.data.domain,
        Driver : r.data.driver,
        Id : r.data.id,
          })
          this.$message.success('Reset thành công', 3);
        })
      }
    }
  }
</script>
