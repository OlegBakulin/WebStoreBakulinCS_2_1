using System;
using System.Collections.Generic;
using System.Text;
using WebStoreCoreApplication.Domain.Entities;

namespace WebStoreCoreApplication.Domain.ViewModels
{
    public class BreadCrumbsViewModel
    {
        public Category Category { get; set; }

        public Brand Brand { get; set; }

        public string Product { get; set; }
    }
}