<template>
  <div>
    <div class="air__utils__heading">
      <h5>Update</h5>
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
                                 label="UserName update"
                                 slot-scope="{ errors, flags }"
                                 hasFeedback
                                 :validateStatus="resolveState({ errors, flags })"
                                 :help='errors[0]'>
                      <a-input name="UpdatedByUserName" v-model="News.updatedByUserName"
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
                                 :help='errors[0]'>
                      <a-input name="title" v-model="News.title"
                               placeholder="Enter title" />
                    </a-form-item>
                  </ValidationProvider>
                  <ValidationProvider name="ShortDescription" rules="required">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 label="ShortDescription"
                                 slot-scope="{ errors, flags }"
                                 hasFeedback
                                 :validateStatus="resolveState({ errors, flags })"
                                 :help='errors[0]'>
                      <a-input name="ShortDescription" v-model="News.shortDescription"
                               placeholder="Enter ShortDescription" />
                    </a-form-item>
                  </ValidationProvider>
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 label="Status"
                                 has-feedback>
                      <a-select v-model="News.status">
                        <a-select-option v-for="realm in realms" :key="realm.value" v-bind:value="realm.value">
                          {{ realm.key }}
                        </a-select-option>
                      </a-select>
                    </a-form-item>
                    <div class="form-group">
                      <a-form-item label="Content">
                        <div :class="$style.editor">
                          <quill-editor v-model="News.description"></quill-editor>
                        </div>
                      </a-form-item>
                    </div>
                    <div class="border-top pt-4">
                      <a-form-item>
                        <a-button type="primary" htmlType="submit" v-on:click="validate().then(UpTo)">
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
        News: {},
        realms: [
          { key: "Sản Phẩm", value: 1 },
          { key: "Người Dùng", value: 2 },
          { key: "Danh Mục", value: 3 },
        ]
      }
    },
    mounted() {
      var self = this;
      axios.get('https://localhost:44397/News/GetById/' + this.$route.params.id)
        .then(function (res) {
          self.News = res.data;
          console.log(res.data);
        })
        .catch(function (error) {
          console.log(error);
        })

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
      UpTo() {
        axios.post('https://localhost:44397/News/CreateOrUpdate', this.News).then(res => {
          if (res) this.$router.replace({ path: "/News" });
        });
        console.log(this.News.StartDate, this.News.EndDate);
      },
    }
  }
</script>
<style lang="scss" module>
  @import "./style.module.scss";
</style>
