using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreCoreApplication.Domain.Entities.Base.Interfaces;

namespace WebStoreCoreApplication.Domain.ViewModels
{
    public class CategoryViewModel : INamedEntity, IOrderEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public List<CategoryViewModel> ChildCategory { get; set; }
            = new List<CategoryViewModel>();

        public CategoryViewModel ParentCategory { get; set; }
    }
}
