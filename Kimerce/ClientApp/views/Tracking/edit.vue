<template>
  <div class="card">
    <div class="card-body">
      <h4 class="mb-4">
        <strong>Sửa lịch sử </strong>
        <br />
        <router-link class="btn btn-primary" to="/track">
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
                      <a-input id="success" v-model="track.title" />
                    </a-form-item>

                  </ValidationProvider>
                  <a-form-item label="Tên thông báo rút gọn"
                               has-feedback
                               validate-status="success">
                    <a-input style="width: 100%" v-model="track.subTitle" />
                  </a-form-item>

                  <ValidationProvider rules="int" v-slot="{errors}">
                    <a-form-item :label-col="labelCol"
                                 :wrapper-col="wrapperCol"
                                 has-feedback
                                 :validate-status="validate(errors[0])"
                                 :help="errors[0]"
                                 label="EntityId">
                      <a-input-number placeholder="EntityId" v-model="track.entityId" />
                    </a-form-item>
                  </ValidationProvider>
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
    name: 'trackedit',
    data() {
      return {
        track: {

        }
      }
    },
    mounted() {
      var self = this;
      axios.get('https://localhost:44397/Tracking/GetById/' + this.$route.params.id)
        .then(function (res) {
          self.track = res.data;
          console.log(res.data);
        })
        .catch(function (error) {
          console.log(error);
        })
    },
    components: {
      ValidationProvider,
      ValidationObserver
    },
    methods: {
      edit() {

        axios.post('https://localhost:44397/Tracking/CreateOrUpdate', this.track)
          .then(axios.delete("https://localhost:44397/Tracking/Delete/" + this.$route.params.id))
          .then(res => {
          if (res) this.$router.replace({ path: "/track" });
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

