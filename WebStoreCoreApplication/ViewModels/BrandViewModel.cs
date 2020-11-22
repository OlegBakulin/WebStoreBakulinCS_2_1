using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreCoreApplication.Domain.Entities.Base.Interfaces;
namespace WebStoreCoreApplication.ViewModels
{
    public class BrandViewModel : INamedEntity, IOrderEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int ProdCount { get; set; }
    }
}
