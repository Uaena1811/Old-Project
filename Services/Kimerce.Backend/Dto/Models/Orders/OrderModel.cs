﻿using Kimerce.Backend.Domain.Orders;
using Kimerce.Backend.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kimerce.Backend.Dto.Models.Orders
{
    public class OrderModel : BaseModelInt
    {
        public Status status { get; set; }




        public long value { get; set; } = 0;

        public string transactionsid { get; set; } = "";

        [Required(ErrorMessage = "Miêu tả không để trống")]

        public string shortDescription { get; set; } = "";

        public string customerid { get; set; } = "";

        public string userid { get; set; } = "";
        public int WardId { get; set; }

        public string Address { get; set; }

        public List<OrderItemModel> ListOrderItem { get; set; }
    }
}
