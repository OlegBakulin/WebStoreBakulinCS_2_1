using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStoreCoreApplication.Domain.Entities.Base;
using WebStoreCoreApplication.Domain.Entities.Base.Interfaces;

namespace WebStoreCoreApplication.Domain.Entities
{
    [Table("Categories")]
    public class Category : NameEntity, IOrderEntity
    {

        public int Order { get; set; }
        public int? ParentId { get; set; }
       
        [ForeignKey("ParentId")]
        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
