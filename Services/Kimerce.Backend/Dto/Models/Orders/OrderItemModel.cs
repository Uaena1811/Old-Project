using Kimerce.Backend.Domain.Orders;
using Kimerce.Backend.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kimerce.Backend.Dto.Models.Orders
{
    public class OrderItemModel : BaseModelInt
    {
        public long ProductPieceId { get; set; }

     
       
        public int OrderId { get; set; }

        

        
        public int DisplayOrder { get; set; }

        public decimal Price { get; set; }

        public int NumberOfProduct { get; set; }

    }
}
