import BaseApi from 'views/baseApi';
export default {
  list: BaseApi.Product + 'relateproduct/search/',
  getById: BaseApi.Product + 'relateproduct/getbyid/',
  createOrUpdate: BaseApi.Product + 'relateproduct/createorupdate/',
  delete: BaseApi.Product + 'relateproduct/delete/'
};
