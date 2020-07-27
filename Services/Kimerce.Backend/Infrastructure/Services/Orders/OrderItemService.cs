using Kimerce.Backend.Domain.Categories;
using Kimerce.Backend.Domain.Locations;
using Kimerce.Backend.Domain.Orders;
using Kimerce.Backend.Domain.Products;
using Kimerce.Backend.Dto;
using Kimerce.Backend.Dto.Items.Locations;
using Kimerce.Backend.Dto.Items.Orders;
using Kimerce.Backend.Dto.Items.Products;
using Kimerce.Backend.Dto.Models.Orders;
using Kimerce.Backend.Dto.Models.Products;
using Kimerce.Backend.Dto.Results;
using Kimerce.Backend.Infrastructure.Helpers;
using Kimerce.Backend.Infrastructure.Repositories;
using Kimerce.Backend.Infrastructure.Services.ProductPieces;
using Kimerce.Backend.Infrastructure.Services.Products;
using Kimerce.Backend.Infrastructure.SmartTable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kimerce.Backend.Infrastructure.Services.Orders
{
    public interface IOrderItemService
    {
         long GetValue(int id);
        
        Task<BaseResult> DeleteByOrder(int id);
        Domain.Orders.Order_Item Get(int id);
        Task<SmartTableResult<OrderItemItem>> Search(SmartTableParam param);
        Task<SmartTableResult<DistrictItem>> SearchCityChildren(SmartTableParam param);
        Task<SmartTableResult<WardItem>> SearchDistrictChildren(SmartTableParam param);
        Task<WardItem> Phantich(string chuoi);
        Task<BaseResult> CreateOrUpdate(OrderItemModel order, int updateBy = 0, string updateByUserName = "");
        Task<BaseResult> Delete(int id);
        Task<ActionResult<IEnumerable<Domain.Orders.Order_Item>>> Index();
        List<ProductModel> GetProductsByOrderId(int id);
        IQueryable<Dto.Items.Orders.OrderItem> GetOrdersByProductPieceId(int id);
        Task<BaseResult> DeleteProductInOrder(int id1, int id2);
        IQueryable<int> GetOrderItemByOrderId(int id);
    }
    public class OrderItemService : IOrderItemService
    {

        private readonly IRepository<Order_Item> _Repository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IProductService _productService;
        private readonly IProductPieceService _productpieceService;
        private readonly IRepository<Ward> _wardRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<City> _cityRepository;
        private static readonly string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };


        public OrderItemService(IRepository<Order> _OrderRepository,IRepository<Domain.Orders.Order_Item> Repository, IProductService productService, IProductPieceService productpieceService, IRepository<Ward> _wardRepository, IRepository<District> _districtRepository, IRepository<City> _cityRepository)
        {
            this._Repository = Repository;
            this._productService = productService;
            this._productpieceService = productpieceService;
            this._wardRepository = _wardRepository;
            this._districtRepository = _districtRepository;
            this._cityRepository = _cityRepository;
            this._orderRepository = _OrderRepository;
        }

        public Domain.Orders.Order_Item Get(int id)
        {
            return _Repository.GetById(id);
        }

        public async Task<ActionResult<IEnumerable<Domain.Orders.Order_Item>>> Index()
        {
            return await _Repository.Query().ToListAsync();

        }

        public async Task<SmartTableResult<Dto.Items.Orders.OrderItemItem>> Search(SmartTableParam param)
        {
            var query = _Repository.Query();
            if (param.Search.PredicateObject != null)
            {
                dynamic search = param.Search.PredicateObject;
                if (search.Keyword != null)
                {
                    string keyword = search.Keyword;
                    keyword = keyword.Trim().ToLower();
                    query = query.Where(x => x.OrderId.ToString().Contains(keyword));
                }

                if (search.CreateStart != null)
                {
                    DateTime createStart = DateTime.Parse(search.CreateStart.ToString());
                    DateTime startOfDay = createStart.StartOfDay();
                    query = query.Where(x => x.CreatedTime >= startOfDay);
                }

                if (search.CreateEnd != null)
                {
                    DateTime createEnd = DateTime.Parse(search.CreateEnd.ToString());
                    DateTime endOfDay = createEnd.EndOfDay();
                    query = query.Where(x => x.CreatedTime <= endOfDay);
                }

            }
            //  param.Sort = new Sort() { Predicate = "DisplayOrder", Reverse = false };
            var gridData = query.ToSmartTableResult(param, x => x.ToItem());
            return gridData;
        }
        public async Task<SmartTableResult<DistrictItem>> SearchCityChildren(SmartTableParam param)
        {
            var query = _districtRepository.Query()
                            .Include(d => d.City)
                            .AsNoTracking();
            if (param.Search.PredicateObject != null)
            {
                dynamic search = param.Search.PredicateObject;
                if (search.Keyword != null)
                {
                    string keyword = search.Keyword;
                    keyword = keyword.Trim().ToLower();
                    query = query.Where(x => x.ParentId.ToString() == keyword);
                }

            }
          //  param.Sort = new Sort() { Predicate = "DisplayOrder", Reverse = false };
            var gridData = query.ToSmartTableResult(param, x => x.ToItem());
            return gridData;
        }
        public async Task<BaseResult> Delete(int id)
        {
            var result = new BaseResult() { Result = Result.Success };
            var Delete = _Repository.GetById(id);
            if (Delete == null)
            {
                result.Result = Result.Failed;
                result.Message = "Không có ";
                return result;
            }
            try
            {
                await _Repository.DeleteAsync(Delete);
                var orderForUpdate = _orderRepository.Query().FirstOrDefault(p => p.Id == Delete.OrderId);
                orderForUpdate.value = this.GetValue(orderForUpdate.Id);
                await _orderRepository.UpdateAsync(orderForUpdate);
                _orderRepository.SaveChange();
            }
            catch (Exception e)
            {
                result.Result = Result.SystemError;
                result.Message = e.ToString();
            }
            return result;
        }
        public async Task<SmartTableResult<WardItem>> SearchDistrictChildren(SmartTableParam param)
        {
            var query = _wardRepository.Query();
                            
            if (param.Search.PredicateObject != null)
            {
                dynamic search = param.Search.PredicateObject;
                if (search.Keyword != null)
                {
                    string keyword = search.Keyword;
                    keyword = keyword.Trim().ToLower();
                    query = query.Where(x => x.ParentId.ToString() == keyword);
                }

            }
          //  param.Sort = new Sort() { Predicate = "DisplayOrder", Reverse = false };
            var gridData = query.ToSmartTableResult(param, x => x.ToItem());
            return gridData;
        }
        


        public async Task<BaseResult> Update(Domain.Orders.Order_Item orderitem, int updateBy = 0, string updateByUserName = "")
        {

            var rs = new BaseResult() { Result = Result.Success };
            var orderForUpdate = _Repository.Query().FirstOrDefault(p => p.Id == orderitem.Id);
            if (orderForUpdate != null)
            {

                try
                {
                    orderForUpdate = orderitem.ToOrderItem(orderForUpdate);

                    orderForUpdate = orderForUpdate.UpdateCommonInt(updateBy, updateByUserName);
                    await _Repository.UpdateAsync(orderForUpdate);

                    var orderForUpdate2 = _orderRepository.Query().FirstOrDefault(p => p.Id == orderitem.OrderId);
                    orderForUpdate2.value = this.GetValue(orderForUpdate.Id);
                    await _orderRepository.UpdateAsync(orderForUpdate2);
                    _orderRepository.SaveChange();
                    _Repository.SaveChange();
                }
                catch (Exception ex)
                {
                    rs.Result = Result.SystemError;
                    rs.Message = ex.ToString();
                }
            }
            else
            {
                rs.Message = "Không tìm thấy don hang cần sửa";
                rs.Result = Result.Failed;
            }
            return rs;
        }

        public async Task<BaseResult> Create(Domain.Orders.Order_Item orderitem)
        {
            var rs = new BaseResult() { Result = Result.Success };

            //var productpiece = _productpieceService.Get(orderitem.ProductPieceId);
            // var product = _productService.Get(productpiece.ProductId);
            //orderitem.Price = product.Price;
            orderitem.Price = 6969669;


            try
            {
                await _Repository.InsertAsync(orderitem);
               var orderForUpdate = _orderRepository.Query().FirstOrDefault(p => p.Id == orderitem.OrderId);
               orderForUpdate.value = (int)this.GetValue(orderForUpdate.Id);
                rs.Message = GetValue(orderitem.OrderId).ToString();
               await _orderRepository.UpdateAsync(orderForUpdate);
               _orderRepository.SaveChange();
            }
            catch (Exception ex)
            {
                rs.Result = Result.SystemError;
                rs.Message = ex.ToString();
            }
            return rs;
        }

        public async Task<BaseResult> CreateOrUpdate(OrderItemModel model, int updateBy = 0, string updateByUserName = "")
        {
            var order = model.ToOrderItem();
            
            order.CreatedTime = DateTime.Now;
           
            if (order.Id > 0)
            {
                //Cập nhật
                return await Update(order);
            }
            else
            {
                //Thêm mới
                return await Create(order);
            }

        }

        public long GetValue (int id)
        {
            long val = 0;

            var query = _Repository.Query().Where( c => c.OrderId == id).ToList();
            foreach (Domain.Orders.Order_Item d in query)
            {
                val += (long)d.Price*d.NumberOfProduct;
            }

            return val;
        }

        public async Task<BaseResult> DeleteByOrder(int id)
        {
            var result = new BaseResult() { Result = Result.Success };
            var query = _Repository.Query().Where(c => c.OrderId == id).ToList();
            foreach (Domain.Orders.Order_Item d in query)
            {
                var resulttmp = new BaseResult() { Result = Result.Success };
                resulttmp = await Delete(d.Id);
                if (resulttmp.Result == Result.Failed)
                {
                    result = await Delete(d.Id);
                }
                    
            }
            return result;
        }

        
        public List<ProductModel> GetProductsByOrderId(int id)
        {
            var result = _Repository.Query().Where(x => x.OrderId == id).Select(x => x.ProductPiece).ToList();
            List<ProductModel> rs = new List<ProductModel>();
            foreach(ProductPiece i in result)
            {
                ProductModel a = _productService.Get(i.ProductId);
                rs.Add(a);
            }
            return rs;

        }
        public IQueryable<int> GetOrderItemByOrderId(int id)
        {
            var result = _Repository.Query().Where(x => x.OrderId == id).Select(x =>x.Id);
            return result;

        }
        
        public IQueryable<Dto.Items.Orders.OrderItem> GetOrdersByProductPieceId(int id)
        {
            var result = _Repository.Query().Where(x => x.ProductPieceId == id).Select(x => x.Order);

            return result.Select(x => x.ToItem());

        }
        public async Task <BaseResult> DeleteProductInOrder(int id1, int id2)
        {
            var rs = new BaseResult() { Result = Result.Success };
            var orderitemForUpdate = _Repository.Query().FirstOrDefault(p => p.OrderId == id1 & p.ProductPieceId == id2);

            return await this.Delete(orderitemForUpdate.Id);
        }

        public static string RemoveSign4VietnameseString(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

         public async Task<WardItem> Phantich(string chuoi)
         {
            WardItem wardItem = _wardRepository.Query()
                                           .Include(w => w.District)
                                           .ThenInclude(d => d.City)
                                           .FirstOrDefault(w => w.Id == 9).ToItem();

            WardItem wardItem2 = _wardRepository.Query()
                                           .Include(w => w.District)
                                           .ThenInclude(d => d.City)
                                           .FirstOrDefault(w => w.Id == 5).ToItem();
            var city = _cityRepository.Query();
            
            int idcity = 0; int iddistrict = 0; int idward = 0;
            //chuoi = "13node65 tp1";
            chuoi = RemoveSign4VietnameseString(chuoi);
            chuoi = chuoi.Replace(" ", "");
            
            foreach ( City c in city )
            {
                var cityname = c.UnsignName.Replace(" ", "");
                if(  chuoi.Contains(cityname))
                {
                    idcity = c.Id;
                    break;
                }
            }
            
            if ( idcity == 0)
            {
                
                return new WardItem();
            }
            else
            {
               
                var districtt = _districtRepository.Query().Where(x => x.ParentId == idcity);
                foreach (District c in districtt)
                {
                    if (chuoi.Contains((c.UnsignName).Replace(" ", "")))
                    {
                        iddistrict = c.Id;
                        break;
                    }
                }
                if( iddistrict == 0)
                {
                      
                    iddistrict = _districtRepository.Query().Where(x => x.ParentId == idcity).FirstOrDefault().Id;
                    idward = _wardRepository.Query().Where(x => x.ParentId == iddistrict).FirstOrDefault().Id;
                    WardItem ward = _wardRepository.Query().Where(x => x.Id == idward).FirstOrDefault().ToItem();
                    return ward;
                    
                }
                else
                {
                    
                    var wardd = _wardRepository.Query().Where(x => x.ParentId == iddistrict);
                    foreach( Ward c in wardd)
                    {
                        if (chuoi.Contains((c.UnsignName).Replace(" ", "")))
                        {
                            idward = c.Id;
                            break;
                        }
                    }
                    if (idward != 0)
                    {
                        WardItem ward = _wardRepository.Query().Where(x => x.Id == idward).FirstOrDefault().ToItem();
                        return ward;
                    }
                    else
                    {
                        idward = _wardRepository.Query().Where(x => x.ParentId == iddistrict).FirstOrDefault().Id;
                        WardItem ward = _wardRepository.Query().Where(x => x.Id == idward).FirstOrDefault().ToItem();
                        return ward;
                    }
                }
            }



           
         }
    }
}
