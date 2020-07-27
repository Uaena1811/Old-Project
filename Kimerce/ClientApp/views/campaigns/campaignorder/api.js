import BaseApi from './../API';
export default {
    list: BaseApi.API + 'campaignorder/search/',
    getById: BaseApi.API + 'campaignorder/GetById',
    createOrUpdate: BaseApi.API + 'campaignorder/createorupdate/',
    delete: BaseApi.API + 'campaignorder/delete/',
};
