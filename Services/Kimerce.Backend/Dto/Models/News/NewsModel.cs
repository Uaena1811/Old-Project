﻿using Kimerce.Backend.Domain.News;
using Kimerce.Backend.Dto.Models;
using Kimerce.Backend.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kimerce.Backend.Dto.Items.News
{
    public class NewsModel : BaseModelInt
    {
        /// <summary>
        /// Tiêu đề
        /// </summary>
        [MaxLength(512)]
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// Mô tả ngắn
        /// </summary>
        [MaxLength(1024)]
        public string ShortDescription { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Trạng thái blog
        /// </summary>
        public NewsStatus Status { get; set; }
        /// <summary>
        /// Ngày công bố
        /// </summary>
        public DateTimeOffset PublishDate { get; set; }
        /// <summary>
        /// Ngày bắt đầu có hiệu lực
        /// </summary>
        public DateTimeOffset StartDate { get; set; }
        /// <summary>
        /// Ngày hết hiệu lực
        /// </summary>
        public DateTimeOffset EndDate { get; set; }

        public string StatusStr => Status.GetDescription(); 
    }
}
