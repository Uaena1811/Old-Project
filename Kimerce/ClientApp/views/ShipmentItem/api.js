import BaseApi from '../baseApi';
export default {
    list: BaseApi.Product + 'shipmentItems/search/',
    getById: BaseApi.Product + 'shipmentItems/getbyid/',
    createOrUpdate: BaseApi.Product + 'shipmentItems/createorupdate/',
    delete: BaseApi.Product + 'shipmentItems/delete/',
    getModel: BaseApi.Product + 'shipmentItems/GetById'
};
