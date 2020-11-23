using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WebStoreCoreApplication.Domain.Entities.Base;

namespace WebStoreCoreApplication.Domain.Entities
{
    public class Order : NameEntity
    {
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; } // внешний ключ в БД
        public virtual Collection<OrderItem> OrderItems { get; set; }
    }
}
