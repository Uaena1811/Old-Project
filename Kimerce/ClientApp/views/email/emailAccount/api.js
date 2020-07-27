import BaseApi from '../../baseApi';
export default {
  list: BaseApi.Product + 'emailaccount/search/',
  getById: BaseApi.Product + 'emailaccount/getbyid/',
  createOrUpdate: BaseApi.Product + 'emailaccount/CreateOrUpdate/',
  delete: BaseApi.Product + 'emailaccount/delete/',
};
