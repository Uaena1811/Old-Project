import BaseApi from '../../../baseApi';
export default {
  list: BaseApi.Product + 'cities/search/',
    getChildren: BaseApi.Product + 'cities/getchildren/',
    createOrUpdate: BaseApi.Product + 'cities/createorupdate/',
    delete: BaseApi.Product + 'cities/delete/',
    exportToXlsx: BaseApi.Product + 'cities/exporttoxlsx/',
    getModel: BaseApi.Product + 'cities/getbyid/',
};
