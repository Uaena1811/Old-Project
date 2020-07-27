import BaseApi from './../API';
export default {
    list: BaseApi.API + 'campaignproduct/search/',
    getById: BaseApi.API + 'campaignproduct/GetById',
    createOrUpdate: BaseApi.API + 'campaignproduct/createorupdate/',
    delete: BaseApi.API + 'campaignproduct/delete/',
};
