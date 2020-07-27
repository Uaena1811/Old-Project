import BaseApi from '../baseApi';
export default {
  list: BaseApi.Product + 'productpiece/search/',
  getById: BaseApi.Product + 'productpiece/getbyid/',
  createOrUpdate: BaseApi.Product + 'productpiece/createorupdate/',
  delete: BaseApi.Product + 'productpiece/delete/',
  getProduct: BaseApi.Product + 'product/search/',
  getInventory: BaseApi.Product + 'inventoryattribute/search',
  getAtrributeValue: BaseApi.Product + 'attributevalue/search',
};
