using System;
using System.Collections.Generic;
using System.Text;

namespace WebStoreCoreApplication.Domain.ViewModels
{
    public class SelectCategotyViewModel
    {
        public IEnumerable<CategoryViewModel> Category { get; set; }
        public int? CurrentCategoryId { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}