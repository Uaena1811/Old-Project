<template>
  <div>
    <div class="air__utils__heading" style="margin-bottom:20px;">

      <h5>Tạo / cập nhật chiến dịch</h5>
    </div>
    <div class="row">
      <div class="col-lg-6">
        <router-link to="/campaign">
          <a-button class="btn btn-primary" type="primary" icon="arrow-left">
            Danh sách chiến dịch
          </a-button>
        </router-link>
      </div>
      <div class="col-lg-12 text-right">
        <a-button class="btn btn-primary mb-3" type="primary" html-type="button" icon="check" @click="Finish">Lưu lại</a-button>
        <a-button class="btn btn-primary mb-3" type="primary" html-type="button" icon="save" @click="Save">Lưu và sửa tiếp</a-button>
        <a-button class="btn btn-danger mb-3" type="danger" html-type="button" icon="undo" @click="Reset">Reset thông tin</a-button>
      </div>
    </div>
    <div class="card overflow-hidden">
      <div class="card-body">
        <a-form layout="vertical" :form="FrmCampaign">
          <div class="row">
            <div class="col-lg-4">
              <a-form-item label="Tên chiến dịch" class="mb-2">
                <a-input v-decorator="['Subject', { rules: [{ required: true, message: 'Vui lòng nhập tên sản phẩm!' }] }]" />
              </a-form-item>
              <div class="row">
                <div class="col-lg-8">
                  <a-form-item label="Quỹ" class="mb-2">
                    <a-input-number v-decorator="['Budget', { rules: [{ required: true, message: 'Vui lòng nhập số tự nhiên!' }] }]" />
                  </a-form-item>
                </div>
               </div>
              <a-form-item label="Date"
                           style="margin-bottom:0;">
                <a-form-item :style="{ display: 'inline-block', width: 'calc(50% - 12px)' }">
                  <a-date-picker v-model="Model.StartDate" style="width: 100%" />
                </a-form-item>
                <span :style="{ display: 'inline-block', width: '24px', textAlign: 'center' }">-</span>
                <a-form-item :style="{ display: 'inline-block', width: 'calc(50% - 12px)' }">
                  <a-date-picker v-model="Model.EndDate" style="width: 100%" />
                </a-form-item>
              </a-form-item>
                <a-form-item label="Trạng thái"
                             has-feedback>
                  <a-select v-decorator="['Status', { rules: [{ required: true, message: 'Vui lòng chọn!' }] }]">

                    <a-select-option v-for="cter in status" :key="cter.value" v-bind:value="cter.value">
                      {{ cter.key }}
                    </a-select-option>
                  </a-select>
                </a-form-item>
            </div>
            <div class="col-lg-8">
              <a-form-item label="Mô tả">
                <ckeditor :editor="editor" v-decorator="['Body']" :config="editorConfig"></ckeditor>
              </a-form-item>

            </div>
          </div>
          <div class="row">
            <div class="col-lg-12">

            </div>
          </div>
        </a-form>
      </div>
    </div>
    <!--<h6 class="mb-3 text-uppercase">
      <strong>Sản phẩm liên quan</strong>
    </h6>-->
  </div>
</template>
<script>
  import CkEditor from '@ckeditor/ckeditor5-build-classic';
  import CampaignApi from './api.js';
   import axios from 'axios';
  export default {
    mounted() {
      axios.get(CampaignApi.getById + this.$route.params.id)
        .then(res => this.Model = res.data ? res.data : this.Model)
        .then(() => {
          this.CreateForm();
        })
    },
    data() {
      return {
        hasErrors: false,
        FrmCampaign: null,
        Model: {
          StartDate: "",
          EndDate:"",
        },
        status: [
          { key: " Đang chờ", value: 1 },
          { key: " Đang chạy", value: 2 },
          { key: " Đã kích hoạt", value: 3 },
          { key: " Không kích hoạt", value: 4 },
        ],
        editor: CkEditor,
        editorConfig: {

        }
      }
    },
    methods: {
      CreateForm() {
        let options = {
          mapPropsToFields: () => {
            return {
              Subject: this.$form.createFormField({ value: this.Model.subject, }),
              Budget: this.$form.createFormField({ value: this.Model.budget }),
              StartDateDisplay: this.$form.createFormField({ value: this.Model.startDateDisplay }),
              EndDateDisplay: this.$form.createFormField({ value: this.Model.endDateDisplay }),
              Body: this.$form.createFormField({ value: this.Model.body }),
              Status: this.$form.createFormField({ value: this.Model.status })
            };
          }
        }
        this.FrmCampaign = this.$form.createForm(this, options);
      },
      GetModel() {
          this.Model.subject = this.FrmCampaign.getFieldValue('Subject'),
          this.Model.startDateDisplay = this.FrmCampaign.getFieldValue('StartDateDisplay'),
          this.Model.body = this.FrmCampaign.getFieldValue('Body'),
          this.Model.budget = this.FrmCampaign.getFieldValue('Budget'),
          this.Model.endDateDisplay = this.FrmCampaign.getFieldValue('EndDateDisplay'),
          this.Model.status = this.FrmCampaign.getFieldValue('Status')
      },
      async Save() {
        await this.FrmCampaign.validateFields((errors, values) => {
          if (errors) {
            this.hasErrors = true;
            return;
          }
          this.GetModel();
          axios.post(CampaignApi.createOrUpdate, this.Model);
        }
        );
      },
      async Finish() {
        await this.FrmCampaign.validateFields((errors, values) => {
          if (errors) {
            this.hasErrors = true;
            return;
          }
          this.GetModel();
          axios.post(CampaignApi.createOrUpdate, this.Model).then(response => {
            if (response) this.$router.replace({ path: "/campaign" });
          });
        }
        );
      },
      Reset() {
        this.FrmCampaign.resetFields();
      }
    }
  }
</script>
