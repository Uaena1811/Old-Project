import BaseApi from '../../../baseApi';
export default {
    list: BaseApi.Product + 'districts/search/',
    getChildren: BaseApi.Product + 'districts/getchildren/',
    createOrUpdate: BaseApi.Product + 'districts/createorupdate/',
    delete: BaseApi.Product + 'districts/delete/',
    getModel: BaseApi.Product + 'districts/getbyid/',
    exportToXlsx: BaseApi.Product + 'districts/exporttoxlsx/',
};
