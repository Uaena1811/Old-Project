import BaseApi from '../../baseApi';
export default {
  list: BaseApi.Product + 'attribute/search/',
  getById: BaseApi.Product + 'attribute/getbyid/',
  createOrUpdate: BaseApi.Product + 'attribute/createorupdate/',
  delete: BaseApi.Product + 'attribute/delete/',
};
