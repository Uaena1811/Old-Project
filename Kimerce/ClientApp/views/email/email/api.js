import BaseApi from '../../baseApi';
export default {
  list: BaseApi.Product + 'email/search/',
  getById: BaseApi.Product + 'email/getbyid/',
  createOrUpdate: BaseApi.Product + 'email/CreateOrUpdate/',
  delete: BaseApi.Product + 'email/delete/',
};
