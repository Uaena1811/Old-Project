<template>
  <div>
    <div class="air__utils__heading">
      <h5>Form Examples</h5>
    </div>
    <div>
      <div class="air__utils__heading">
        <h5>Quản lý sản phẩm</h5>
      </div>
     
      
    </div>
    <div class="card">
      <div class="card-body">
        <h4 class="mb-4">
          <strong>Tạo Mới</strong>
        </h4>
        <div class="bg-light rounded-lg p-5">
          <div class="row">
            <div class="col-lg-8 mx-auto">
              <ValidationObserver v-slot="{invalid}">
                <a-form @submit.prevent="create">
                  <ValidationProvider rules="required" v-slot="{errors}">

                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 label="Name"
                                 has-feedback
                                 :validate-status="validate(errors[0])">
                      <a-input v-model="Image.name" placeholder="Ex: photo_2020-01-16_11-11-56.jpg" />
                    </a-form-item>
                    <span style="color:red" v-for="(error, index) in errors" :key="index">{{error}}</span>
                  </ValidationProvider>

                  <ValidationProvider rules="required" v-slot="{errors}">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 label="Path"
                                 has-feedback
                                 :validate-status="validate(errors[0])">
                      <a-input v-model="Image.path" placeholder="Ex: Users\PC\Desktop" />
                    </a-form-item>
                    <span style="color:red" v-for="(error, index) in errors" :key="index">{{error}}</span>
                  </ValidationProvider>

                  <ValidationProvider rules="required" v-slot="{errors}">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 label="Uri"
                                 has-feedback
                                 :validate-status="validate(errors[0])">
                      <a-input v-model="Image.uri" placeholder="Ex: http://www.kamilgrzybek.com/wp-content/uploads/" />
                    </a-form-item>
                    <span style="color:red" v-for="(error, index) in errors" :key="index">{{error}}</span>
                  </ValidationProvider>

                  <ValidationProvider rules="required" v-slot="{errors}">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 label="Domain"
                                 has-feedback
                                 :validate-status="validate(errors[0])">
                      <a-input v-model="Image.domain" placeholder="Ex: http://www.kamilgrzybek.com/" />
                    </a-form-item>
                    <span style="color:red" v-for="(error, index) in errors" :key="index">{{error}}</span>
                  </ValidationProvider>


                  <ValidationProvider rules="required" v-slot="{errors}">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 label="Driver:"
                                 has-feedback>
                      <a-select v-model="Image.driver">

                        <a-select-option v-for="cter in Drivers" :key="cter.value" v-bind:value="cter.value">
                          {{ cter.key }}
                        </a-select-option>
                      </a-select>
                    </a-form-item>
                    <span style="color:red" v-for="(error, index) in errors" :key="index">{{error}}</span>
                  </ValidationProvider>




                  <div class="border-top pt-4">
                    <a-form-item>
                      <a-button type="primary" htmlType="submit" v-on:click="create">
                        Submit
                      </a-button>
                      <a-button type="primary">
                        <router-link :to="{path:'/image'}">
                          Back
                        </router-link>
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
<script src="https://unpkg.com/axios/dist/axios.min.js"></script>

<script>
  import axios from 'axios'
   import { ValidationProvider, ValidationObserver, extend } from 'vee-validate';
  import { required } from 'vee-validate/dist/rules';
  import { Validator } from 'vee-validate';

 /*Validator.extend(
  'isImage',
  (value) => {
    // value must be > zero
    if (value.includes("jpg") ) return true;
    return false;
  }
);*/


  extend('required', {
    ...required,
    message: 'Required'
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
        labelCol,
        wrapperCol,
        Image: {
          name: "", path: "", driver: "", domain: "", uri: "",
        },

        Drivers: [
          { key: "C:\\", value: "C:\\" },
          { key: "D:\\", value: "D:\\" },
          { key: "E:\\", value: "E:\\" },
          { key: "F:\\", value: "F:\\" },
        ]
      }
    },
    methods: {
      create() {
         console.log('Dat:a', this.Image);
        axios.post("https://localhost:44397/Images/CreateOrUpdate/", this.Image


         

        ).then(response => {
           if (response.data.result ==1) this.$router.replace({ path: "/image" });
          else {
            this.mess = "that bai";
          }
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
