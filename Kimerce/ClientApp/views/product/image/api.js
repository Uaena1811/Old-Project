import BaseApi from 'views/baseApi';
export default {
  list: BaseApi.Product + 'productimage/search/',
  getById: BaseApi.Product + 'productimage/getbyid/',
  createOrUpdate: BaseApi.Product + 'productimage/createorupdate/',
  delete: BaseApi.Product + 'productimage/delete/',

  getImageByProduct: BaseApi.Product + 'productimage/getimagebyproduct/',
};
