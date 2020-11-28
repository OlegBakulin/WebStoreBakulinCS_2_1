using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStoreCoreApplication.Domain.Entities.Base;

namespace WebStoreCoreApplication.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [Required]
        public virtual Order Order { get; set; } // сгенерирует внешний ключ в БД
        public virtual Product Product { get; set; } // сгенерирует внешний ключ в БД
    }
}