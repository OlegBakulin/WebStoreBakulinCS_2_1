using WebStoreCoreApplication.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebStoreCoreApplication.Domain.Entities.Base
{
    public class NameEntity : INamedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
      
        public string Name { get; set; }
         
    }
}
