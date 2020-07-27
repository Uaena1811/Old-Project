using Kimerce.Backend.Domain.Orders;
using Kimerce.Backend.Domain.Products;
using Kimerce.Backend.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kimerce.Backend.Dto.Items.Orders
{
    public class OrderItemItem : BaseModelInt
    {
        public long ProductPieceId { get; set; }

        public ProductPiece ProductPiece { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
        public int DisplayOrder { get; set; }

        public decimal Price { get; set; }

        public int NumberOfProduct { get; set; }




        public DateTimeOffset? CreatedTime { get; set; }

        public string CreatedTimeDisplay => CreatedTime.HasValue ? CreatedTime.Value.ToString("dd/MM/yyyy HH:mm") : "";


        public DateTimeOffset? UpdatedTime { get; set; }

        public string UpdatedTimeDisplay => UpdatedTime.HasValue ? UpdatedTime.Value.ToString("dd/MM/yyyy HH:mm") : "";

    }
}
