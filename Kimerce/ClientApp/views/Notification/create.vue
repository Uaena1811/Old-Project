<template>
  <div>
    <div class="air__utils__heading">
      <h5>Create Notification</h5>
    </div>
    <div class="card">
      <div class="card-body">
        <h4 class="mb-4">
          <strong>Create Form</strong>
        </h4>
        <a-form :form="form">
          <ValidationObserver v-slot="{invalid}">
            <a-form @submit.prevent="post">
              <ValidationProvider rules="required" v-slot="{errors}">
                <a-form-item label="Tên thông báo"
                             has-feedback
                             :validate-status="validate(errors[0])"
                             :help="errors[0]"
                             :wrapper-col="wrapperCol"
                             :label-col="labelCol">
                  <a-input placeholder="Notification Title" v-model="form.title" />

                </a-form-item>

              </ValidationProvider>

              <ValidationProvider rules="required" v-slot="{errors}">
                <a-form-item label="Notification Sub Title"
                             has-feedback
                             :validate-status="validate(errors[0])"
                             :help="errors[0]"
                             :wrapper-col="wrapperCol"
                             :label-col="labelCol">
                  <a-input placeholder="Notification Sub Title" v-model="form.subtitle" />
                </a-form-item>
              </ValidationProvider>
              <ValidationProvider rules="required" v-slot="{errors}">
                <a-form-item :label-col="labelCol"
                             :wrapper-col="wrapperCol"
                             has-feedback
                             :validate-status="validate(errors[0])"
                             :help="errors[0]"
                             label="Link">
                  <a-input placeholder="Link" v-model="form.link" />
                </a-form-item>
              </ValidationProvider>
              <ValidationProvider rules="int" v-slot="{errors}">
                <a-form-item :label="NotificationConfigID"
                             has-feedback
                             :validate-status="validate(errors[0])"
                             :help="errors[0]"
                             :wrapper-col="wrapperCol"
                             :label-col="labelCol">
                  <a-input-number placeholder="NotificationConfigID" v-model="form.noticonfigID"></a-input-number>
                </a-form-item>
              </ValidationProvider>
              <div class="border-top pt-4">
                <a-form-item>
                  <a-button type="primary" @click="post">
                    Create
                  </a-button>
                </a-form-item>
              </div>
            </a-form>

          </ValidationObserver>

        </a-form>

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
  const labelCol = {
    xs: { span: 24 },
    sm: { span: 4 },
  }
  const wrapperCol = {
    xs: { span: 24 },
    sm: { span: 12 },
  }




  export default {

    data: function () {
      return {
        form: {
          title :null,
          subtitle : null,
          link : null,
          noticonfigID : null
        },

        labelCol,
        wrapperCol,
      }
    },
    methods: {
      post() {
        axios.post("https://localhost:44397/Notification/CreateOrUpdate", this.form).then(response => {
          if (response) this.$router.replace({ path: "/notification" });
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
    },
    components: {
      ValidationProvider,
      ValidationObserver
    }
  }
</script>
