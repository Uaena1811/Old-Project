<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">

      <h5>Tạo / cập nhật thuộc tính sản phẩm</h5>
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
              <div class="col-lg-8">
                <a-form-item label="Mã thuộc tính" class="mb-2">
                  <a-select v-decorator="['AttributeId', { rules: [{ required: true, message: 'Vui lòng nhập mã thuộc tính!' }] }]" @popupScroll="GetAttribute">
                    <a-select-option v-for="(attribute, index) in attributes" :key="index" :value="attribute.id">{{attribute.id}} - {{attribute.name}}</a-select-option>
                  </a-select>
                </a-form-item>
              </div>
              <div class="col-lg-8">
                <a-form-item label="Giá trị">
                  <a-input v-decorator="['Value', { rules: [{ required: true, message: 'Vui lòng nhập giá trị thuộc tính!' }] }]" />
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
  import AttributeApi from './api';
  export default {
    created() {
      Axios.get(AttributeApi.getById + this.$route.params.id).then(r => {
        this.Model.AttributeId = r.data.attributeId;
        this.Model.Value = r.data.value;
        this.Model.Id = r.data.id;
      }).then(() => {
        this.CreateForm();
      });
    },
    mounted() {
      this.GetAttribute();
    },
    data() {
      return {
        FrmProduct: null,
        Model: {
          AttributeId: '',
          Value: "",
          Id: 0,
        },
        editor: CkEditor,
        editorConfig: {

        },
        attributes: [],
        attributePageIndex: 1,
        attributeLoad: true,
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            return {
              AttributeId: this.$form.createFormField({ value: this.Model.AttributeId, }),
              Value: this.$form.createFormField({ value: this.Model.Value, }),
            };
          }
        }
        this.FrmProduct = this.$form.createForm(this, options);
      },
      GetModel() {
        this.Model.AttributeId = this.FrmProduct.getFieldValue('AttributeId');
        this.Model.Value = this.FrmProduct.getFieldValue('Value');
      },
      async SaveAndFinish() {
        this.FrmProduct.validateFieldsAndScroll((errors, values) => {
          if (!errors) {
            this.GetModel();
            Axios.post(AttributeApi.createOrUpdate, this.Model).then(r => {
              if (r.data.result != 1) {
                this.$message.error(r.data.message);
              }
              else {
                this.$message.success('Lưu dữ liệu thành công', 3);
                this.$router.replace("/attributevalue");
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
            Axios.post(AttributeApi.createOrUpdate, this.Model).then(r => {
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
        Axios.get(AttributeApi.getById + this.$route.params.id).then(r => {
          this.FrmProduct.setFieldsValue({
            Value: r.data.value,
          })
          this.Model.CallForPrice = r.data.callForPrice;
          this.$message.success('Reset thành công', 3);
        })
      },
      GetAttribute() {
        if (this.attributeLoad) {
          Axios.post(AttributeApi.getAttribute, {
            "Pagination": {
              "PageIndex": this.attributePageIndex,
              "PageSize": 10
            },
            "Sort": {
            },
            "Search": {

            }
          }).then(r => {
            this.attributes = this.attributes.concat(r.data.items);
            if (this.attributes.length == r.data.totalRecord) {
              this.attributeLoad = false;
            }
            this.attributePageIndex += 1;
          })
        }
      },
      components: {

      }
    }
  }
</script>
