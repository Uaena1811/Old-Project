import BaseApi from './../API';
export default {
    list: BaseApi.API + 'discount/search/',
    getById: BaseApi.API + 'discount/GetById/',
    createOrUpdate: BaseApi.API + 'discount/createorupdate/',
    delete: BaseApi.API + 'discount/delete/',
};
