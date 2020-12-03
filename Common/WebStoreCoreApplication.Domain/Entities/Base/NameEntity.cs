using WebStoreCoreApplication.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebStoreCoreApplication.Domain.Entities.Base
{
    public class NameEntity : BaseEntity, INamedEntity
    {
        
        [Required]
        public string Name { get; set; }
         
    }
}
