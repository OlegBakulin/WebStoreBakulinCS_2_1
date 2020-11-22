using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreCoreApplication.Controllers.Infrastructure.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BaseController : Controller
    {
        private readonly IProductServices _productService;

        public BaseController(IProductServices productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(_productService.GetProducts(new ProductFilter()));
        }
    }
}
