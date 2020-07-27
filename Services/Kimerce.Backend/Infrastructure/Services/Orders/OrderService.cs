using Kimerce.Backend.Domain.Orders;
using Kimerce.Backend.Dto;
using Kimerce.Backend.Dto.Items.Orders;
using Kimerce.Backend.Dto.Models.Orders;
using Kimerce.Backend.Dto.Results;
using Kimerce.Backend.Infrastructure.Helpers;
using Kimerce.Backend.Infrastructure.Repositories;
using Kimerce.Backend.Infrastructure.Services.Categories;
using Kimerce.Backend.Infrastructure.SmartTable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kimerce.Backend.Infrastructure.Services.Orders
{
    public interface IOrderService
    {
        Order Get(int id);
        Task<SmartTableResult<Dto.Items.Orders.OrderItem>> Search(SmartTableParam param);
        Task<BaseResult> CreateOrUpdate(OrderModel model, int updateBy = 0, string updateByUserName = "");
        Task<BaseResult> Delete(int id);
    }
    public class OrderService : IOrderService

    { 
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrderItemService _orderItemService;
        private readonly IWardService _wardService;
        public IQueryable<Order> GetAll()
        {
            return _orderRepository.Query();
        }

        public Order Get(int id)
        {
            return _orderRepository.GetById(id);
        }
        public async Task<ActionResult<IEnumerable<Order>>> Index()
        {
            return await _orderRepository.Query().ToListAsync();

        }
        public OrderService(IRepository<Order> OrderRepository, IOrderItemService OrderItemService, IWardService _wardService)
        {
           this._orderRepository = OrderRepository;
            this._orderItemService = OrderItemService;
            this._wardService = _wardService;
        }


        public async Task<SmartTableResult<Dto.Items.Orders.OrderItem>> Search(SmartTableParam param)
        {
            var query = _orderRepository.Query();
            if (param.Search.PredicateObject != null)
            {
                dynamic search = param.Search.PredicateObject;
               if (search.Keyword != null)
                {
                    string keyword = search.Keyword;
                    keyword = keyword.Trim().ToLower();
                    query = query.Where(x => x.transactionsid.Contains(keyword));
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

        public async Task<CreateOrUpdateResultInt> Create(Order order)
        {
            var rs = new CreateOrUpdateResultInt();



            try
            {
                await _orderRepository.InsertAsync(order);
            }
            catch (Exception ex)
            {
                rs.Result = Result.SystemError;
                rs.Message = ex.ToString();
            }
            rs.Id = order.Id;
            return rs;
        }
        public async Task<BaseResult> Update(Order order, int updateBy = 0, string updateByUserName = "")
        {

            var rs = new BaseResult() { Result = Result.Success };
            var orderForUpdate = _orderRepository.Query().FirstOrDefault(p => p.Id == order.Id);
            if (orderForUpdate != null )
            {
               
                try
                {
                    orderForUpdate = order.ToOrder(orderForUpdate);

                    orderForUpdate = orderForUpdate.UpdateCommonInt(updateBy, updateByUserName);
                    await _orderRepository.UpdateAsync(orderForUpdate);
                    _orderRepository.SaveChange();
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

        public async Task<BaseResult> CreateOrUpdate(OrderModel model, int updateBy = 0, string updateByUserName = "")
        {
            var ward = _wardService.Get(model.WardId);
            model.Address = ward.Name + ward.District.Name + ward.District.City.Name;
            List<OrderItemModel> order_Items = null;
            if (model.ListOrderItem != null)
            { 
               order_Items = model.ListOrderItem;
               model.ListOrderItem = null;
            }
            var order = model.ToOrder();
            
            if (order.Id > 0)
            {

                if (order_Items != null)
                {
                    List<int> listInDTB = _orderItemService.GetOrderItemByOrderId(model.Id).ToList();
                    List<int> orderItemUpdateID = new List<int> { };
                    foreach (OrderItemModel orderItemModel2 in order_Items)
                    {
                        
                        await _orderItemService.CreateOrUpdate(orderItemModel2);
                        orderItemUpdateID.Add( orderItemModel2.Id);
                    }

                    foreach(int a in listInDTB)
                    {
                        if( !orderItemUpdateID.Contains(a))
                        {
                            await _orderItemService.Delete(a);
                        }
                    }

                    order_Items = null;
                }
                //Cập nhật
                return await Update(order);
            }
            else
            {
                order.CreatedTime = DateTime.Now;
                CreateOrUpdateResultInt rs = await Create(order);

                if (order_Items != null)

                {
                    foreach( OrderItemModel orderItemModel2 in order_Items)
                    {
                        orderItemModel2.OrderId = rs.Id;
                        
                        await _orderItemService.CreateOrUpdate(orderItemModel2);
                    }
                }
                
               
                return rs;
            }

        }
        
        public async Task<BaseResult> Delete(int id)
        {
            var result = new BaseResult() { Result = Result.Success };
            result = await _orderItemService.DeleteByOrder(id);
            var Delete =  _orderRepository.GetById(id);
            if (Delete == null)
            {
                result.Result = Result.Failed;
                result.Message = "Không có ";
                return result;
            }
            try
            {
                await _orderRepository.DeleteAsync(Delete);
            }
            catch (Exception e)
            {
                result.Result = Result.SystemError;
                result.Message = e.ToString();
            }
            return result;
        }

    }
}



/* public async Task<ActionResult<IEnumerable<Order>>> Index()
        {
            return await _orderRepository.QueryAll().ToListAsync();

        }
        public IQueryable<Order> GetAll()
        {
            return  _orderRepository.QueryAll();
        }

        public Order GetById(int id)
        {
            return _orderRepository.GetById(id);
        }
        public SmartTableResult<OrderItem> Search(SmartTableParam param)
        {
            var query = _orderRepository.Query();
            if (param.Search.PredicateObject != null)
            {
                dynamic search = param.Search.PredicateObject;
                if (search.Keyword != null)
                {
                    string keyword = search.Keyword;
                    keyword = keyword.Trim().ToLower();
                    query = query.Where(x => x.name.Contains(keyword));
                }
            }
            param.Sort = new Sort() { Predicate = "DisplayOrder", Reverse = false };
            var gridData = query.ToSmartTableResult(param, x => x.ToItem());
            return gridData;
        }
        private BaseResult Create(Order order)
        {
            var rs = new BaseResult() { Result = Result.Success };
            order.name = order.name.Trim();
            var exists = _orderRepository.Query().Any(c => c.name == order.name);
            if (exists)
            {
                rs.Result = Result.Failed;
                rs.Message = " đã tồn tại!";
                return rs;
            }
            _orderRepository.Add(order);
            try
            {
                _orderRepository.SaveChange();
            }
            catch (Exception ex)
            {
                rs.Result = Result.SystemError;
                rs.Message = ex.ToString();
            }
            return rs;
        }

        public BaseResult Update(Order orderItem)
        {

            var rs = new BaseResult() { Result = Result.Success };
            var order = _orderRepository.Query().Where(c => !c.IsDeleted).FirstOrDefault(c => c.Id == orderItem.Id);
            if (order != null)
            {
                var exists = _orderRepository.Query().Any(c => !c.IsDeleted && c.name == orderItem.name && c.Id != order.Id);
                if (exists)
                {
                    rs.Result = Result.Failed;
                    rs.Message = "Tỉnh / thành phố đã tồn tại!";
                    return rs;
                }
                

                order.name = orderItem.name.Trim();
                order.sum = orderItem.sum;

                

                _orderRepository.Update(order);
                try
                {
                    _orderRepository.SaveChange();
                }
                catch (Exception ex)
                {
                    rs.Result = Result.SystemError;
                    rs.Message = ex.ToString();
                }
            }
            else
            {
                rs.Message = "Không tìm thấy cần sửa";
                rs.Result = Result.Failed;
            }
            return rs;
        }

        public BaseResult CreateOrUpdate(Order order)
        {
            var rs = new BaseResult() { Result = Result.Success };
            return order.Id <= 0 ? Create(order) : Update(order);

        }

        public BaseResult Delete(int Id)
        {
            var rs = new BaseResult() { Result = Result.Success };
            if (Id > 0)
            {
                var city = _orderRepository.Query().FirstOrDefault(c => c.Id == Id);
                if (city != null)
                {
                    city.IsDeleted = true;
                    _orderRepository.Update(city);
                    try
                    {
                        _orderRepository.SaveChange();
                    }
                    catch (Exception ex)
                    {
                        rs.Result = Result.SystemError;
                        rs.Message = ex.ToString();
                    }
                }
                else
                {
                    rs.Message = "Không tìm thấy yêu cầu!";
                    rs.Result = Result.Failed;
                }

            }
            else
            {
                rs.Message = " không hợp lệ!";
                rs.Result = Result.Failed;
            }
            return rs;
        }
        public async Task<Order> Details(int id)
        {

            var order = await _orderRepository.QueryAll().Include(s => s.o_a_pList).ThenInclude(e => e.Product).AsNoTracking().
                FirstOrDefaultAsync(m => m.Id == id);

            return  order;
        }
*/