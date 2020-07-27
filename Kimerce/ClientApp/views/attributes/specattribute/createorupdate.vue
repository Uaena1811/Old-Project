<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">

      <h5>Tạo / cập nhật thuộc tính đặc biệt</h5>
    </div>
    <div class="row">
      <a-form layout="vertical" :form="FrmProduct">
        <div class="col-lg-12 text-right">
          <a-button class="btn btn-primary mb-3" type="primary" html-type="button" icon="save" @click="SaveAndFinish">Lưu lại</a-button>
          <a-button class="btn btn-primary mb-3" type="primary" html-type="button" icon="save" @click="Save">Lưu và sửa tiếp</a-button>
          <a-button class="btn btn-danger mb-3" type="danger" html-type="button" icon="save" @click="Reset">Reset thông tin</a-button>
        </div>
        <div class="card overflow-hidden">
          <div class="card-body">
            <div class="row mb-4">
              <div class="col-lg-12">

              </div>

            </div>

            <div class="row">
              <div class="col-lg-12">
                <a-form-item label="Tên thuộc tính" class="mb-2">
                  <a-input v-decorator="['Name', { rules: [{ required: true, message: 'Vui lòng nhập tên thuộc tính!' }] }]" />
                </a-form-item>
              </div>
              <div class="col-lg-12">
                <a-form-item label="Mô tả đầy đủ">
                  <ckeditor :editor="editor" v-model="Model.Description" :config="editorConfig"></ckeditor>
                </a-form-item>

              </div>
            </div>
            <div class="row">
              <div class="col-lg-12">

              </div>
            </div>
          </div>
        </div>
      </a-form>
    </div>
  </div>
  <!--<h6 class="mb-3 text-uppercase">
    <strong>Sản phẩm liên quan</strong>
  </h6>-->
</template>
<script>
  import CkEditor from '@ckeditor/ckeditor5-build-classic';
  import Axios from 'axios';
  import SpecAttributeApi from './api';
  export default {
    created() {
      Axios.get(SpecAttributeApi.getById + this.$route.params.id).then(r => {
        this.Model.Name = r.data.name;
        this.Model.Description = r.data.description;
        this.Model.Id = r.data.id;
      }).then(() => {
        this.CreateForm();
      });
    },
    mounted() {

    },
    data() {
      return {
        FrmProduct: null,
        Model: {
          Name: '',
          Description: "",
          Id: 0,
        },
        editor: CkEditor,
        editorConfig: {

        },
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            return {
              Name: this.$form.createFormField({ value: this.Model.Name, }),
            };
          }
        }
        this.FrmProduct = this.$form.createForm(this, options);
      },
      GetModel() {
        this.Model.Name = this.FrmProduct.getFieldValue('Name');
      },
      async SaveAndFinish() {
        this.FrmProduct.validateFieldsAndScroll((errors, values) => {
          if (!errors) {
            this.GetModel();
            Axios.post(SpecAttributeApi.createOrUpdate, this.Model).then(r => {
              if (r.data.result != 1) {
                this.$message.error(r.data.message);
              }
              else {
                this.$message.success('Lưu dữ liệu thành công', 3);
                this.$router.replace("/specattribute");
              }
            }).catch(error => {
              this.$message.error('Không thể kết nối tới máy chủ', 3);
              console.log(error);
            });
          }
        });
      },
      Save() {
        this.FrmProduct.validateFieldsAndScroll((errors, values) => {
          if (!errors) {
            this.GetModel();
            Axios.post(SpecAttributeApi.createOrUpdate, this.Model).then(r => {
              if (r.data.result != 1) {
                this.$message.error(r.data.message);
              }
              else {
                this.$message.success('Lưu dữ liệu thành công!', 3);

              }
            }).catch(error => {
              this.$message.error('Đã xảy ra lỗi!', 3);
              console.log(error);
            });
          }
        });
      },
      Reset() {
        Axios.get(SpecAttributeApi.getById + this.$route.params.id).then(r => {
          this.FrmProduct.setFieldsValue({
            Name: r.data.name,
          })
          this.Model.CallForPrice = r.data.callForPrice;
          this.$message.success('Reset thành công', 3);
        })
      },
      
    },
    components: {

    }
  }
</script>
