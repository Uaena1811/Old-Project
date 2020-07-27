import BaseApi from '../..//baseApi';
export default {
  list: BaseApi.Product + 'productcategory/search/',
  getById: BaseApi.Product + 'productcategory/getbyid/',
  createOrUpdate: BaseApi.Product + 'productcategory/createorupdate/',
  delete: BaseApi.Product + 'productcategory/delete/',
  getChildren: BaseApi.Product + 'productcategory/getchildren/',
};
