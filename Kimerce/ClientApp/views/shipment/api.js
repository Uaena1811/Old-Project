import BaseApi from '../baseApi';
export default {
    list: BaseApi.Product + 'shipments/search/',
    getById: BaseApi.Product + 'shipments/getbyid/',
    createOrUpdate: BaseApi.Product + 'shipments/createorupdate/',
    delete: BaseApi.Product + 'shipments/delete/',
    getModel: BaseApi.Product + 'shipments/GetById'
};
