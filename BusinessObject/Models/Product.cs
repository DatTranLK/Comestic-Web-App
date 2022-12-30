using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? Amount { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }
        public byte[] Image4 { get; set; }
        public byte[] Image5 { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string Summary { get; set; }
        public int? AmountSold { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Element { get; set; }
        public string UserManual { get; set; }
        public bool? IsNeedContact { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
