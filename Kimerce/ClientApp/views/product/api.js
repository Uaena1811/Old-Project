import BaseApi from '../baseApi';
export default {
  list: BaseApi.Product + 'product/search/',
  getById: BaseApi.Product + 'product/getbyid/',
  createOrUpdate: BaseApi.Product + 'product/createorupdate/',
  delete: BaseApi.Product + 'product/delete/',
  getRelate: BaseApi.Product + 'Product/GetRelate/',
};
