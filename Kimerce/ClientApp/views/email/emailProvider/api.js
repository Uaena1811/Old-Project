import BaseApi from '../../baseApi';
export default {
  list: BaseApi.Product + 'emailprovider/search/',
  getById: BaseApi.Product + 'emailprovider/getbyid/',
  createOrUpdate: BaseApi.Product + 'emailprovider/createorupdate/',
  delete: BaseApi.Product + 'emailprovider/delete/',
};
