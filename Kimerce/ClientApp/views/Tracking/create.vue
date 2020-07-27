<template>
  <div>
    <div class="air__utils__heading">
      <h5>Create Tracking</h5>
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
                <a-form-item label="Lich Su "
                             has-feedback
                             :validate-status="validate(errors[0])"
                             :help="errors[0]"
                             :wrapper-col="wrapperCol"
                             :label-col="labelCol">
                  <a-input placeholder="Track Title" v-model="form.title" />

                </a-form-item>

              </ValidationProvider>

              <ValidationProvider rules="required" v-slot="{errors}">
                <a-form-item label="Track Sub Title"
                             has-feedback
                             :validate-status="validate(errors[0])"
                             :help="errors[0]"
                             :wrapper-col="wrapperCol"
                             :label-col="labelCol">
                  <a-input placeholder="Track Sub Title" v-model="form.subtitle" />
                </a-form-item>
              </ValidationProvider>
              <ValidationProvider rules="required" v-slot="{errors}">
                <a-form-item :label-col="labelCol"
                             :wrapper-col="wrapperCol"
                             has-feedback
                             :validate-status="validate(errors[0])"
                             :help="errors[0]"
                             label="EntityId">
                  <a-input-number placeholder="EntityId" v-model="form.entityId" />
                </a-form-item>
              </ValidationProvider>
              <ValidationProvider rules="required" v-slot="{errors}">
                <a-form-item :label="EntityType"
                             has-feedback
                             :validate-status="validate(errors[0])"
                             :help="errors[0]"
                             :wrapper-col="wrapperCol"
                             :label-col="labelCol">
                  <a-select v-model="form.entityType">
                    <a-select-option value=''>None</a-select-option>
                    <a-select-option v-for="e in form.entityType" :key="e.value" v-bind:value="e.value">
                      {{e.key}}
                    </a-select-option>
                  </a-select>
                </a-form-item>
              </ValidationProvider>
              <ValidationProvider rules="required" v-slot="{errors}">
                <a-form-item :label="ActionType"
                             has-feedback
                             :validate-status="validate(errors[0])"
                             :help="errors[0]"
                             :wrapper-col="wrapperCol"
                             :label-col="labelCol">
                  <a-select v-model="form.actionType">
                    <a-select-option value=''>None</a-select-option>
                    <a-select-option v-for="a in form.actionType" :key="a.value" v-bind:value="a.value">
                      {{a.key}}
                    </a-select-option>
                  </a-select>
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
          title: "Lịch sử",
          subtitle: "Lịch sử  rút gọn",
          entityId: 1,
          entityType: [
          { key: "Sản phẩm", value: 1 },
          { key: "Người dùng", value: 2 },
          { key: "Danh mục", value: 3 },
          { key: "Email", value: 4 },
        ],
          actionType: [
          { key: "Thêm", value: 1 },
          { key: "Cập nhật", value: 2 },
          { key: "Xóa ", value: 3 },
          { key: "Xem chi tiết", value: 4 },


            ],
        },

        labelCol,
        wrapperCol,
      }
    },
    methods: {
      post() {
        axios.post("https://localhost:44397/Tracking/CreateOrUpdate", this.form).then(response => {
          if (response) this.$router.replace({ path: "/track" });
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
