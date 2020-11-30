using System.Collections.Generic;
using System.Linq;
using WebStoreCoreApplication.Domain.DTO.Products;
using WebStoreCoreApplication.Domain.Entities;

namespace WebStoreBakulin.Services.Mapping
{
    public static class ProductDTOMapper
    {
        public static ProductDTO ToDTO(this Product Product) => Product is null ? null : new ProductDTO
        {
            Id = Product.Id,
            Name = Product.Name,
            Order = Product.Order,
            Price = Product.Price,
            ImageUrl = Product.ImageUrl,
            Brand = Product.Brand.ToDTO(),
            Category = Product.Category.ToDTO(),
        };

        public static Product FromDTO(this ProductDTO Product) => Product is null ? null : new Product
        {
            Id = Product.Id,
            Name = Product.Name,
            Order = Product.Order,
            Price = Product.Price,
            ImageUrl = Product.ImageUrl,
            BrandId = Product.Brand?.Id,
            Brand = Product.Brand.FromDTO(),
            CategoryId = Product.Category.Id,
            Category = Product.Category.FromDTO(),
        };

        public static IEnumerable<ProductDTO> ToDTO(this IEnumerable<Product> products) => products.Select(ToDTO);

        public static IEnumerable<Product> FromDTO(this IEnumerable<ProductDTO> products) => products.Select(FromDTO);
    }
}