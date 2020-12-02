using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStoreCoreApplication.Domain.Entities.Base;
using WebStoreCoreApplication.Domain.Entities.Identity;

namespace WebStoreCoreApplication.Domain.Entities
{
    public class Order : NameEntity
    {
        public virtual User User { get; set; } // внешний ключ в БД
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
                
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
    public class OrderItem : BaseEntity
    {
        [Required]
        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
