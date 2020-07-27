import BaseApi from '../../baseApi';
export default {
  list: BaseApi.Product + 'attributevalue/search/',
  getById: BaseApi.Product + 'attributevalue/getbyid/',
  createOrUpdate: BaseApi.Product + 'attributevalue/createorupdate/',
  delete: BaseApi.Product + 'attributevalue/delete/',
  getAttribute: BaseApi.Product + 'attribute/search',
};
