using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStoreCoreApplication.Domain.Entities.Base;
using WebStoreCoreApplication.Domain.Entities.Base.Interfaces;

namespace WebStoreCoreApplication.Domain.Entities
{
    [Table("Brand")]
    public class Brand : NameEntity, IOrderEntity
    {
        public int Order { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
