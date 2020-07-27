import BaseApi from '../baseApi';
export default {
  list: BaseApi.Product + 'setting/search/',
  getById: BaseApi.Product + 'setting/getbyid/',
  createOrUpdate: BaseApi.Product + 'setting/createorupdate/',
  delete: BaseApi.Product + 'setting/delete/',
};
