import BaseApi from '../../baseApi';
export default {
  list: BaseApi.Product + 'inventoryattribute/search/',
  getById: BaseApi.Product + 'inventoryattribute/getbyid/',
  createOrUpdate: BaseApi.Product + 'inventoryattribute/createorupdate/',
  delete: BaseApi.Product + 'inventoryattribute/delete/',
};
