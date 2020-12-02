using System.ComponentModel.DataAnnotations.Schema;
using WebStoreCoreApplication.Domain.Entities.Base;
using WebStoreCoreApplication.Domain.Entities.Base.Interfaces;

namespace WebStoreCoreApplication.Domain.Entities
{
    [Table("Products")]
    public class Product : NameEntity, IOrderEntity
    {
        public int Order { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public int? BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }
        public string ImageUrl { get; set; }
        
        [Column(TypeName = "decimal(18,2)")] 
        public decimal Price { get; set; }
        //public int Size { get; set; }
        


    }
}