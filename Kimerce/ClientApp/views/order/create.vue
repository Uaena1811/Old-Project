<template>
  <div>
    <div class="air__utils__heading">
      <h5>Form Examples</h5>
    </div>

    <div class="card">
      <div class="card-body">
        <h4 class="mb-4">
          <strong>Tạo Đơn Hàng</strong>
        </h4>


        <div class="bg-light rounded-lg p-5">
          <div class="row">
            <div class="col-lg-8 mx-auto">
              <ValidationObserver v-slot="{invalid}">

                <a-form @submit.prevent="create">

                  <ValidationProvider rules="int|required" v-slot="{errors}">

                    <a-form-item :label-col="labelCol"
                                 label="Values"
                                 has-feedback
                                 :validate-status="validate(errors[0])">
                      <a-input-number v-model="Order.value" addonBefore="$" placeholder="$" />
                      <span style="color:red" v-for="(error, index) in errors" :key="index">{{error}}</span>

                    </a-form-item>

                  </ValidationProvider>

                  <ValidationProvider rules="numeric|required" v-slot="{errors}">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 label="User"
                                 has-feedback :validate-status="validate(errors[0])">
                      <a-input v-model="Order.userid"  placeholder="ID"  type="text"/>
                      <span style="color:red" v-for="(error, index) in errors" :key="index">{{error}}</span>
                    </a-form-item>
                  </ValidationProvider>

                  <ValidationProvider rules="required" v-slot="{errors}">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 label="Khách Hàng"
                                 has-feedback
                                 :validate-status="validate(errors[0])">
                      <a-select v-model="Order.customerid">

                        <a-select-option v-for="cter in listcustomer1" :key="cter.value" v-bind:value="cter.value">
                          {{ cter.key }}
                        </a-select-option>
                      </a-select>
                      <span style="color:red" v-for="(error, index) in errors" :key="index">{{error}}</span>
                    </a-form-item>

                  </ValidationProvider>

                  <ValidationProvider rules="required" v-slot="{errors}">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 label="Ngày giao"
                                 has-feedback
                                 validate-status="validate(errors[0])">
                      <a-date-picker style="width: 100%" />
                      <span style="color:red" v-for="(error, index) in errors" :key="index">{{error}}</span>
                    </a-form-item>
                  </ValidationProvider>

                  <ValidationProvider rules="required" v-slot="{errors}">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 label="Trạng thái"
                                 has-feedback
                                 :validate-status="validate(errors[0])">
                      <a-select v-model="Order.status">

                        <a-select-option v-for="cter in statuss" :key="cter.value" v-bind:value="cter.value">
                          {{ cter.key }}
                        </a-select-option>
                      </a-select>
                      <span style="color:red" v-for="(error, index) in errors" :key="index">{{error}}</span>
                    </a-form-item>
                  </ValidationProvider>



                  <ValidationProvider rules="required" v-slot="{errors}">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 label="Miêu tả"
                                 :validate-status="validate(errors[0])">
                      <a-input v-model="Order.shortDescription" placeholder="Miêu tả ngắn" />
                      <span style="color:red" v-for="(error, index) in errors" :key="index">{{error}}</span>
                    </a-form-item>
                  </ValidationProvider>
                            <div class="border-top pt-4">
                              <p class="text-warning"> {{mess}}</p>

                              <a-form-item>
                                <a-button type="primary" htmlType="submit" v-on:click="create">
                                  Submit
                                </a-button>

                                <a-button type="primary">
                                  <router-link :to="{path:'/order'}">
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
        Order: {
          value: 0, transactionsid: "transactionsid", shortDescription: "shortDescription", status: 0, customerid: "customerid", userid: "000",
        },
        listcustomer: [],

        listcustomer1: [
          { key: "Người 1", value: 1 },
          { key: "Người 2", value: 2 },
          { key: "Người 3", value: 3 },
          { key: "Người 4", value: 4 },
          { key: "chai nuuoc", value: 5 },

        ],
        users: [
          { key: "user1", value: 1 },
          { key: "Nuser2", value: 2 },
          { key: "hihi", value: 3 },
          { key: "user 2", value: 4 },
          { key: "chai nuuoc", value: 5 },

        ],
        statuss: [
          { key: " Đã hoàn thành", value: 1 },
          { key: " Chưa hoàn thành", value: 0 },


        ]
      }
    },
    methods: {
      create() {
        this.Order.transactionsid = this.Order.customerid + this.Order.userid;

        axios.post(
          "https://localhost:44397/Orders/CreateOrUpdate/", this.Order

        ).then(response => {
          console.log("cack");
          if (response.data.result == 1) this.$router.replace({ path: "/order" });
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
