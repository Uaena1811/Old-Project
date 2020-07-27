import Vue from 'vue';
import axios from 'axios';
import router from './router/index';
import store from './store';
//import { sync } from 'vuex-router-sync';



import App from './views/App';
//import VeeValidate from 'vee-validate';
import Antd from 'ant-design-vue';
import BootstrapVue from 'bootstrap-vue';
import VueLayers from 'vuelayers';
import NProgress from 'vue-nprogress'
import 'ant-design-vue/dist/antd.css';
import { i18n } from './localization';
import { ValidationObserver, ValidationProvider, extend, localize } from 'vee-validate';
import en from 'vee-validate/dist/locale/en.json';
import * as rules from 'vee-validate/dist/rules';
import CKEditor from '@ckeditor/ckeditor5-vue';

Vue.prototype.$http = axios.create({
  headers: { 'Content-Type': 'application/json', 'Cache-Control': 'no-cache', 'Expires': '-1' }
});

// Registration of global components
Vue.component('icon', () => import('./icons'));

//sync(store, router)
localize('en', en);

// Install components globally
Vue.component('ValidationObserver', ValidationObserver);
Vue.component('ValidationProvider', ValidationProvider);

Vue.config.productionTip = false;
import './global.scss';
import './css/site.css';

//Vue.use(VeeValidate, { inject: false });
Vue.use(Antd);

Vue.use(BootstrapVue)
Vue.use(VueLayers)
Vue.use(CKEditor)
Vue.config.productionTip = false
const nprogress = new NProgress({ parent: 'body' })

const app = new Vue({
  store,
  nprogress,
  router,
    i18n,
  ...App
});


export {
  app,
  router,
  store,
  i18n,
    nprogress,
 
}
