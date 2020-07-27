import BaseApi from './../API';
export default {
    list: BaseApi.API + 'discountorder/search/',
    getById: BaseApi.API + 'discountorder/GetById',
    createOrUpdate: BaseApi.API + 'discountorder/createorupdate/',
    delete: BaseApi.API + 'discountorder/delete/',
};
