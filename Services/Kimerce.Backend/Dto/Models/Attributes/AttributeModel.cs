﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kimerce.Backend.Dto.Models.Attributes
{
    public class AttributeModel: BaseModelInt
    {
        #region Fields

        /// <summary>
        /// Tên thuộc tính
        /// </summary>
        [MaxLength(512), Required]
        public string Name { get; set; }

        /// <summary>
        /// Mô tả thuộc tính
        /// </summary>
        public string Description { get; set; }

        #endregion
    }
}
