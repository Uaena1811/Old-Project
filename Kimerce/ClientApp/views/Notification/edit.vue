<template>
  <div class="card">
    <div class="card-body">
      <h4 class="mb-4">
        <strong>Sửa Thông báo </strong>
        <br />
        <router-link class="btn btn-primary" to="/notification">
          Trở lại
        </router-link>
      </h4>
      <div class="bg-light rounded-lg p-5">
        <div class="row">
          <div class="col-lg-8 mx-auto">
            <a-form>
              <ValidationObserver v-slot="{invalid}">
                <a-form @submit.prevent="post">
                  <ValidationProvider rules="required" v-slot="{errors}">
                    <a-form-item label="Tên thông báo"
                                 has-feedback
                                 :validate-status="validate(errors[0])"
                                 :help="errors[0]"
                                 :wrapper-col="wrapperCol"
                                 :label-col="labelCol">
                      <a-input id="success" v-model="notification.title" />
                    </a-form-item>

                  </ValidationProvider>
                  <a-form-item label="Tên thông báo rút gọn"
                               has-feedback
                               validate-status="success">
                    <a-input style="width: 100%" v-model="notification.subTitle" />
                  </a-form-item>

                  <ValidationProvider rules="required" v-slot="{errors}">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 has-feedback
                                 :validate-status="validate(errors[0])"
                                 :help="errors[0]"
                                 label="Link">

                      <a-input style="width: 100%" v-model="notification.link" />


                    </a-form-item>

                  </ValidationProvider>

                  <a-form-item label="Cấu hình của thông báo"
                               has-feedback>
                    <ValidationProvider rules="int" v-slot="{errors}">
                      <a-form-item :label="NotificationConfigID"
                                   has-feedback
                                   :validate-status="validate(errors[0])"
                                   :help="errors[0]"
                                   :wrapper-col="wrapperCol"
                                   :label-col="labelCol">
                        <a-input-number placeholder="NotificationConfigID" v-model="notification.noticationConfigId"></a-input-number>
                      </a-form-item>
                    </ValidationProvider>

                  </a-form-item>

                  <div class="border-top pt-4">
                    <a-form-item>
                      <a-button type="primary" @click="edit">
                        Sửa
                      </a-button>
                    </a-form-item>
                  </div>
                </a-form>
              </ValidationObserver>
            </a-form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import axios from 'axios';
  import { ValidationProvider, ValidationObserver, extend } from 'vee-validate';
  import { required } from 'vee-validate/dist/rules';

  extend('required', {
    ...required,
    message: 'Bắt buộc'
  });

  extend('int', {
    validate: value => {
      return Number.isInteger(value);
    },
    message: 'Phải là số nguyên'
  });
  export default {
    name: 'edit',
    data() {
      return {
        notification: {

         title :"",


         subTitle :"",


         link : "",

         noticationConfigId : 1


        }
      }
    },
    mounted() {
      var self = this;
            axios.get('https://localhost:44397/Notification/GetById/' + this.$route.params.id)
                .then(function (res) {
                    self.notification = res.data;
                    console.log(res.data);
                })
                .catch(function (error) {
                    console.log(error);
                })
    },
    components : {
      ValidationProvider,
      ValidationObserver
    },
    methods: {
      edit() {

         axios.post('https://localhost:44397/Notification/CreateOrUpdate', this.notification).then(res => {
          if (res) this.$router.replace({ path: "/notification" });
        });

      },

       validate(error) {
        if (error == null) {
          return "success";
        }
        else {
          return "error";
        }
      }
    }
  }
</script>

