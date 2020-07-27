<template>
  <div>
    <div class="air__utils__heading">
      <h5>Add</h5>
    </div>
    <router-link :to="{ path:'/News'}"><h4 style="color:deepskyblue">Back</h4></router-link>
    <div class="card">
      <div class="card-body">
        <h4 class="mb-4">
          <strong>New</strong>
        </h4>
        <div class="bg-light rounded-lg p-5">
          <div class="row">
            <div class="col-lg-8 mx-auto">
              <ValidationObserver ref="observer">
                <a-form slot-scope="{ validate }">
                  <ValidationProvider name="UserName" rules="required">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 slot-scope="{ errors, flags }"
                                 hasFeedback
                                 :validateStatus="resolveState({ errors, flags })"
                                 :help='errors[0]'
                                 label="UserName"
                                 >
                      <a-input name="CreatedByUserName" v-model="News.CreatedByUserName"
                               placeholder="Enter User Name" />
                     
                    </a-form-item>
                  </ValidationProvider>
                  <ValidationProvider name="title" rules="required">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 label="title"
                                 slot-scope="{ errors, flags }"
                                 hasFeedback
                                 :validateStatus="resolveState({ errors, flags })"
                                 :help='errors[0]'
                                 >
                      <a-input name="title" v-model="News.title"
                               placeholder="Enter title" />
                     
                    </a-form-item>
                  </ValidationProvider>
                  <ValidationProvider name="ShortDescription" rules="required">
                    <a-form-item label="ShortDescription"
                                 :label-col="labelCol"
                                 slot-scope="{ errors, flags }"
                                 hasFeedback
                                 :validateStatus="resolveState({ errors, flags })"
                                 :help='errors[0]'
                                 :wrapper-col="wrapperCol"
                                 >
                      <a-input name="ShortDescription" v-model="News.ShortDescription"
                               placeholder="Enter ShortDescription" />
                     
                    </a-form-item>
                  </ValidationProvider>
                  <a-form-item :label-col="labelCol"
                               :wrapper-col="wrapperCol"
                               label="Status"
                               has-feedback>
                    <a-select v-model="News.Status">
                      <a-select-option v-for="realm in realms" :key="realm.value" v-bind:value="realm.value">
                        {{ realm.key }}
                      </a-select-option>
                    </a-select>
                  </a-form-item>
                 
                    <a-form-item label="Date"
                                 :label-col="labelCol"                                 
                                 :wrapper-col="wrapperCol"
                                 style="margin-bottom:0;">
                      <a-form-item :style="{ display: 'inline-block', width: 'calc(50% - 12px)' }">
                        <a-date-picker v-model="News.StartDate" style="width: 100%" />
                      </a-form-item>
                      <span :style="{ display: 'inline-block', width: '24px', textAlign: 'center' }">-</span>
                      <a-form-item :style="{ display: 'inline-block', width: 'calc(50% - 12px)' }">
                        <a-date-picker v-model="News.EndDate" style="width: 100%" />
                      </a-form-item>
                    </a-form-item>
                  
                  <div class="form-group">
                    <a-form-item label="Content">
                      <div :class="$style.editor">
                        <quill-editor v-model="News.Description"></quill-editor>
                      </div>
                    </a-form-item>
                  </div>
                  <div class="border-top pt-4">
                    <a-form-item>
                      <a-button type="primary" htmlType="submit" v-on:click="validate().then(AddTo)">
                        Submit
                      </a-button>
                    </a-form-item>
                  </div>
                </a-form>
                </ValidationObserver>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import { ValidationProvider, ValidationObserver } from 'vee-validate';
  import 'quill/dist/quill.core.css'
  import 'quill/dist/quill.snow.css'
  import { quillEditor } from 'vue-quill-editor'
  import axios from "axios"
  const baseURL = "https://localhost:44397/News/CreateOrUpdate"
  const labelCol = {
    xs: { span: 24 },
    sm: { span: 6 },
  }
  const wrapperCol = {
    xs: { span: 24 },
    sm: { span: 15 },
  }
  export default {
    components: {
      quillEditor,
      ValidationProvider,
      ValidationObserver
    },
    data: function () {
      return {
        form: this.$form.createForm(this),
        labelCol,
        wrapperCol,
        News: {
          CreatedByUserName: "",
          title: "",
          ShortDescription: "",
          Description: "",
          Status: 1,
          StartDate: "",
          EndDate: "",
        },
        realms: [
          { key: "Sản Phẩm", value: 1 },
          { key: "Người Dùng", value: 2 },
          { key: "Danh Mục", value: 3 },
        ]
      }
    },
    methods: {
      resolveState({ errors, flags }) {
        if (errors[0]) {
          return 'error';
        }

        if (flags.pending) {
          return 'validating';
        }

        if (flags.valid) {
          return 'success';
        }
        return '';
      },
      AddTo() {
        try {
          axios.post(baseURL, this.News).then(res => {
            if (res) this.$router.replace({ path: "/News" });
          });
        } catch (e) { }
      },
    }
  }
</script>
<style lang="scss" module>
  @import "./style.module.scss";
</style>
