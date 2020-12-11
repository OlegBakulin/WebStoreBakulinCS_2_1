using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using WebStoreBakulin.Interfaces.Services;

namespace WebStoreCoreApplication.Controllers
{
    public class SaitMapController : Controller 
    {
        public IActionResult Invoke([FromServices] IProductServices ProductData)
        {
            var nodes = new List<SitemapNode>
            {
                new (Url.Action("Index", "Base")),
                new (Url.Action("ContactUs", "Base")),
                new (Url.Action("Blog", "Base")),
                new (Url.Action("Blog_Single", "Base")),
                new (Url.Action("Shop", "Catalog")),
                new (Url.Action("Index", "WebAPI")),
            };

            nodes.AddRange(ProductData.GetCategories().Select(s => new SitemapNode(Url.Action("Shop", "Catalog", new { CategoryId = s.Id }))));

            foreach (var brand in ProductData.GetBrands())
                nodes.Add(new SitemapNode(Url.Action("Shop", "Catalog", new { BrandId = brand.Id })));

            foreach (var product in ProductData.GetProducts())
                nodes.Add(new SitemapNode(Url.Action("Details", "Catalog", new { product.Id })));

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}
