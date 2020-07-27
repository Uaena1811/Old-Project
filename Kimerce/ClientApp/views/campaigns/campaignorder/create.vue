<template>
  <div>
    <div class="air__utils__heading">
      <h5>Form Examples</h5>
    </div>

    <div class="card">
      <div class="card-body">
        <h4 class="mb-4">
          <strong>Tạo </strong>
        </h4>
        <div class="bg-light rounded-lg p-5">
          <div class="row">
            <div class="col-lg-8 mx-auto">
              <ValidationObserver v-slot="{invalid}">

                <a-form @submit.prevent="create">

                  <ValidationProvider rules="int|required" v-slot="{errors}">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 label="Campaign ID"
                                 has-feedback :validate-status="validate(errors[0])">
                      <a-input-number v-model="CampaignOrder.campaignId" placeholder="0" type="text" />
                      <span style="color:red" v-for="(error, index) in errors" :key="index">{{error}}</span>
                    </a-form-item>
                  </ValidationProvider>

                  <ValidationProvider rules="int|required" v-slot="{errors}">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 label="Order Id"
                                 has-feedback :validate-status="validate(errors[0])">
                      <a-input-number v-model="CampaignOrder.orderId" placeholder="0" type="text" />
                      <span style="color:red" v-for="(error, index) in errors" :key="index">{{error}}</span>
                    </a-form-item>
                  </ValidationProvider>
                  <ValidationProvider rules="int|required" v-slot="{errors}">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 label="Display Order"
                                 has-feedback :validate-status="validate(errors[0])">
                      <a-input-number v-model="CampaignOrder.displayOrder" placeholder="0" type="text" />
                      <span style="color:red" v-for="(error, index) in errors" :key="index">{{error}}</span>
                    </a-form-item>
                  </ValidationProvider>
                  <div class="border-top pt-4">
                    <p class="text-warning"> {{mess}}</p>
                    <a-form-item>
                      <a-button type="primary" htmlType="submit" v-on:click="update">
                        Submit
                      </a-button>

                      <a-button type="primary">
                        <router-link :to="{path:'/campaignorder'}">
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


<script>
  import axios from 'axios'
  import { ValidationProvider, ValidationObserver, extend } from 'vee-validate';
  import { required } from 'vee-validate/dist/rules';

  extend('required', {
    ...required,
    message: 'Required'
  });
  extend('numeric', {
    validate: va => {
      return Number.isInteger(parseInt(va));
    },
    message: 'Ma so khong hop le'
  });

  extend('int', {
    validate: value => {
      return Number.isInteger(value);
    },
    message: 'Not integer'
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
        mess: "...",
        CampaignOrder: {
          campaignId: 0,
          orderId: 0,
          displayOrder: 0,
        },
      }
    },
    methods: {
      create() {
        axios.post(
          "https://localhost:44397/CampaignOrder/CreateOrUpdate/", this.CampaignOrder
        ).then(response => {
          if (response.data.result == 1) this.$router.replace({ path: "/CampaignOrder" });
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
