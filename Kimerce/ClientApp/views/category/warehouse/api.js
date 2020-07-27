import BaseApi from '../../baseApi';
export default {
  list: BaseApi.WareHouse + 'warehouses/search/',
  createOrUpdate: BaseApi.WareHouse + 'warehouses/createorupdate/',
  delete: BaseApi.WareHouse + 'warehouses/delete/',
  exportToXlsx: BaseApi.WareHouse + 'warehouses/exporttoxlsx/',
  getModel: BaseApi.WareHouse + 'warehouses/getbyid/'
};
