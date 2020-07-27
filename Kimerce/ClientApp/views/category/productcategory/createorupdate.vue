<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">

      <h5>Tạo / cập nhật danh mục sản phẩm</h5>
    </div>
    <div class="row">
      <a-form layout="vertical" :form="Form">
        <div class="col-lg-12 text-right">
          <router-link to="/productcategory">
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
            <div class="row mb-4">
              <div class="col-lg-12">

              </div>

            </div>

            <div class="row">
              <div class="col-lg-4">
                <a-form-item label="Tên danh mục" class="mb-2">
                  <a-input placeholder="Nhập tên danh mục" v-decorator="['Name', { rules: [{ required: true, message: 'Vui lòng nhập tên sản phẩm!' }] }]" />
                </a-form-item>
                <div class="row">
                  <div class="col-lg-6">
                    <a-form-item label="Danh mục cha" class="mb-2">
                      <a-cascader placeholder="Chọn danh mục cha"
                                  :options="options"
                                  :fieldNames="fieldNames"
                                  :loadData="loadData"
                                  changeOnSelect
                                  @change="Change" />
                      <a-input-number :disabled="true" style="width: 100%" v-decorator="['ParentId', { rules: [{ type: 'integer', message: 'Vui lòng nhập số nguyên!' }] }]" />
                    </a-form-item>
                  </div>
                  <div class="col-lg-6">
                    <a-form-item label="Thứ tự hiển thị" class="mb-2">
                      <a-input-number style="width: 100%" v-decorator="['DisplayOrder', { rules: [{ type: 'integer', message: 'Vui lòng nhập số nguyên!' }] }]" />
                    </a-form-item>
                  </div>
                </div>
                <a-form-item label="Giá trị phân trang mặc định" class="mb-2">
                  <a-input-number style="width: 100%" v-decorator="['DefaultPageSize', { rules: [{ type: 'integer', message: 'Vui lòng nhập số nguyên!' }] }]" />
                </a-form-item>
                <div class="row">
                  <div class="col-lg-6">
                    <a-form-item label="Phân khúc giá" class="mb-2">
                      <a-input :disabled="true" v-decorator="['PriceRanges', { rules: [] }]" />
                    </a-form-item>
                  </div>
                  <div class="col-lg-6">
                    <a-form-item label="Danh sách phân trang" class="mb-2">
                      <a-input :disabled="true" v-decorator="['PageSizeOptions', { rules: [] }]" />
                    </a-form-item>
                  </div>
                </div>

                <a-form-item label="Trạng thái">

                </a-form-item>
              </div>
              <div class="col-lg-8">
                <a-form-item label="Mô tả ngắn" class="mb-2">
                  <!--<a-input v-decorator="['shortdescription', { rules: [{ required: true, message: 'Vui lòng nhập tên sản phẩm!' }] }]" />-->
                  <a-textarea placeholder="Mô tả ngắn"
                              v-decorator="['ShortDescription', { rules: [{ required: true, message: 'Vui lòng nhập mô tả ngắn sản phẩm!' }] }]"
                              :autosize="{ minRows: 3, maxRows: 6 }" />
                </a-form-item>
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
  import ProductCategoryApi from './api';
  export default {
    created() {
      Axios.get(ProductCategoryApi.getById + this.$route.params.id).then(r => {
        this.Model.Name = r.data.name;
        this.Model.DefaultPageSize = r.data.defaultPageSize;
        this.Model.ParentId = r.data.parentId;
        this.Model.ShortDescription = r.data.shortDescription;
        this.Model.Description = r.data.description;
        this.Model.DisplayOrder = r.data.displayOrder;
        this.Model.Id = r.data.id;
      }).then(() => {
        this.CreateForm();
      });
    },
    mounted() {
      Axios.get(ProductCategoryApi.getChildren + 0).then(r => {
        this.options = r.data;
        for (let i = 0; i < this.options.length; i++) {
          this.options[i].isLeaf = false;
        }
      });
    },
    data() {
      return {
        Form: null,
        Model: {
          Name: '',
          DefaultPageSize: 16,
          ParentId: null,
          ShortDescription: '',
          Description: '',
          DisplayOrder: 0,
        },
        editor: CkEditor,
        editorConfig: {

        },

        options: [
        ],

        fieldNames: {
          label: 'name',
          value: 'id',
          children: 'children',
        }
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            return {
              Name: this.$form.createFormField({ value: this.Model.Name, }),
              DefaultPageSize: this.$form.createFormField({ value: this.Model.DefaultPageSize, }),
              ParentId: this.$form.createFormField({ value: this.Model.ParentId, }),
              ShortDescription: this.$form.createFormField({ value: this.Model.ShortDescription }),
              DisplayOrder: this.$form.createFormField({ value: this.Model.DisplayOrder }),
            };
          }
        }
        this.Form = this.$form.createForm(this, options);
      },
      GetModel() {
        this.Model.Name = this.Form.getFieldValue('Name');
        this.Model.ShortDescription = this.Form.getFieldValue('ShortDescription');
        this.Model.ParentId = this.Form.getFieldValue('ParentId');
        this.Model.DefaultPageSize = this.Form.getFieldValue('DefaultPageSize');
        this.Model.DisplayOrder = this.Form.getFieldValue('DisplayOrder');
      },
      async SaveAndFinish() {
        this.Form.validateFieldsAndScroll((errors, values) => {
          if (!errors) {
            this.GetModel();
            Axios.post(ProductCategoryApi.createOrUpdate, this.Model).then(r => {
              if (r.data.result != 1) {
                this.$message.error(r.data.message);
              }
              else {
                this.$message.success('Lưu dữ liệu thành công', 3);
                this.$router.replace("/productcategory");
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
            Axios.post(ProductCategoryApi.createOrUpdate, this.Model).then(r => {
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
        Axios.get(ProductCategoryApi.getById + this.$route.params.id).then(r => {
          this.Form.setFieldsValue({
            Name: r.data.name,
            ShortDescription: r.data.shortDescription,
            Description: r.data.description,
            ParentId: r.data.parentId,
            DefaultPageSize: r.data.defaultPageSize,
            DisplayOrder: r.data.displayOrder,
            Id: r.data.Id,
          })
          this.$message.success('Reset thành công', 3);
        })
      },
      Change(value, selectedOptions) {
        this.Form.setFieldsValue({ 'ParentId': value[value.length - 1] });
        console.log(this.Form.getFieldValue('ParentId'));
      },
      loadData(selectedOptions) {
        const targetOption = selectedOptions[selectedOptions.length - 1];
        // load options lazily
        Axios.get(ProductCategoryApi.getChildren + targetOption.id).then(r => {
          targetOption.children = r.data;
          for (let i = 0; i < targetOption.children.length; i++) {
            targetOption.children[i].isLeaf = false;
          }
          this.options = [...this.options];
        });
      }
    }
  }
</script>
