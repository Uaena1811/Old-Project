﻿using Kimerce.Backend.Domain.BaseEntities;
using Kimerce.Backend.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kimerce.Backend.Domain.Orders
{
    public class Order_Item : BaseEntityByInt
    {
        public long ProductPieceId { get; set; }

        public ProductPiece ProductPiece { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
        public int DisplayOrder { get; set; }

        public decimal Price { get; set; }

        public int NumberOfProduct { get; set; }


    }
}
