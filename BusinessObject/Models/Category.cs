﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public int? TypeId { get; set; }

        public virtual Type? Type { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
