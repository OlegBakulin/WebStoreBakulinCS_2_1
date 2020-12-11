using System;
using System.Collections.Generic;
using System.Text;

namespace WebStoreCoreApplication.Domain.DTO.Products
{
    public class BrandDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int ProductsCount { get; set; }
    }
}
