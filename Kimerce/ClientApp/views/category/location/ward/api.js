import BaseApi from '../../../baseApi';
export default {
    list: BaseApi.Product + 'wards/search/',
    getModel: BaseApi.Product + 'wards/getbyid/',
    createOrUpdate: BaseApi.Product + 'wards/createorupdate/',
    delete: BaseApi.Product + 'wards/delete/',
    exportToXlsx: BaseApi.Product + 'wards/exporttoxlsx/',
};
