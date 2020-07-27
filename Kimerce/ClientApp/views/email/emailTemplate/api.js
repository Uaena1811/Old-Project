import BaseApi from '../../baseApi';
export default {
  list: BaseApi.Product + 'emailtemplate/search/',
  getById: BaseApi.Product + 'emailtemplate/getbyid/',
  createOrUpdate: BaseApi.Product + 'emailtemplate/createorupdate/',
  delete: BaseApi.Product + 'emailtemplate/delete/',
};
