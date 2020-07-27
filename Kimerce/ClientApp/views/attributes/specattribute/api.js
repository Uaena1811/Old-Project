import BaseApi from '../../baseApi';
export default {
  list: BaseApi.Product + 'specattribute/search/',
  getById: BaseApi.Product + 'specattribute/getbyid/',
  createOrUpdate: BaseApi.Product + 'specattribute/createorupdate/',
  delete: BaseApi.Product + 'specattribute/delete/',
};
