using System.Collections.Generic;

namespace WebStoreCoreApplication.Domain.ViewModels
{
    public class SelectBrandViewModel
    {
        public IEnumerable<BrandViewModel> Brands { get; set; }
        public int? CurrentBrandId { get; set; }
    }
}