import BaseApi from './../API';
export default {
    list: BaseApi.API + 'campaign/search/',
    getById: BaseApi.API + 'Campaign/GetById/',
    createOrUpdate: BaseApi.API + 'campaign/createorupdate/',
    delete: BaseApi.API + 'campaign/delete/',
};
