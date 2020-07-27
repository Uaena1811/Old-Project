using Kimerce.Backend.Domain.Orders;
using Kimerce.Backend.Domain.Products;
using Kimerce.Backend.Dto.Items.Locations;
using Kimerce.Backend.Dto.Items.Orders;
using Kimerce.Backend.Dto.Models.Orders;
using Kimerce.Backend.Dto.Models.Products;
using Kimerce.Backend.Infrastructure.Services.Orders;
using Kimerce.Backend.Infrastructure.Services.Products;
using Kimerce.Backend.Infrastructure.SmartTable;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kimerce.Backend.Controllers.Orders
{

    [Route("[controller]")]

    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _Service;

        private readonly IProductService _productService;

        public OrderItemsController(IOrderItemService Service, IProductService _ProductService)
        {
            this._Service = Service;
            this._productService = _ProductService;
        }

        /*[HttpGet("GetAll")]

        public async Task<ActionResult<IEnumerable<Domain.Orders.OrderItem>>> GetAll()
        {
            return await _Service.Index();
        }
        */

        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromBody]SmartTableParam param)
        {
            var orders = await _Service.Search(param);
            return Ok(orders);
        }


        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate([FromBody]OrderItemModel order)
        {
            //order.Price = _productService.Get(order.IdProductPiece).Price;
               
            var result = await _Service.CreateOrUpdate(order);
            return Ok(result);
        }


        [HttpGet("GetById/{id}")]
        public Domain.Orders.Order_Item Get(int id)
        {
            return _Service.Get(id);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _Service.Delete(id);
            return Ok(result);
        }

        [HttpGet("GetProductsByOrderId/{id}")]
        public List<ProductModel> GetProductPiecesById(int id)
        {
            return _Service.GetProductsByOrderId(id);
        }


        [HttpGet("GetOrdersByProductId/{id}")]
        public IQueryable<OrderItem> GetOrdersById(int id)
        {
            return _Service.GetOrdersByProductPieceId(id);
        }

       [HttpGet("GetOrderItem/{id}")]
        public IQueryable<int> GetO(int id)
        {
            return _Service.GetOrderItemByOrderId(id);
        }

        [HttpDelete("DeleteProductInOrder/{id}")]
        public async Task <IActionResult> DeleteProductInOrder( int id, int id1)
        { 
            var rs = await _Service.DeleteProductInOrder(id, id1);
            return Ok(rs);
        }

        [HttpGet("Phantich")]
        public async Task<WardItem> Phantich(string chuoi)  
        {
            var ward =  _Service.Phantich(chuoi);
            return await ward;
        }

       
    }
}
