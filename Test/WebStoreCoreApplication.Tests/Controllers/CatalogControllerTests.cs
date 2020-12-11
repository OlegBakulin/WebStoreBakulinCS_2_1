﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStoreCoreApplication.Controllers;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreCoreApplication.Domain.DTO.Products;
using WebStoreCoreApplication.Domain.ViewModels;
using WebStoreBakulin.Interfaces.Services;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class CatalogControllerTests
    {
        [TestMethod]
        public void Details_Returns_with_Correct_View()
        {
            // A-A-A = Arrange - Act - Assert

            #region Arrange

            const int expected_product_id = 1;
            const decimal expected_price = 10m;

            var expected_name = $"Product id {expected_product_id}";
            var expected_brand_name = $"Brand of product {expected_product_id}";

            var product_data_mock = new Mock<IProductServices>();
            product_data_mock
               .Setup(p => p.GetProductById(It.IsAny<int>()))
               .Returns<int>(id => new ProductDTO
                {
                    Id = id,
                    Name = $"Product id {id}",
                    ImageUrl = $"img{id}.png",
                    Order = 1,
                    Price = expected_price,
                    Brand = new BrandDTO
                    {
                        Id = 1,
                        Name = $"Brand of product {id}"
                    },
                    Category = new CategoryDTO
                    {
                        Id = 1,
                        Name = $"Section of product {id}"
                    }
                });

            var controller = new CatalogController(product_data_mock.Object);

            #endregion

            #region Act

            var result = controller.ProductDetails(expected_product_id);

            #endregion

            #region Assert

            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(view_result.Model);

            Assert.Equal(expected_product_id, model.Id);
            Assert.Equal(expected_name, model.Name);
            Assert.Equal(expected_price, model.Price);
            Assert.Equal(expected_brand_name, model.Brand);

            #endregion
        }

        [TestMethod]
        public void Shop_Returns_CorrectView()
        {
            var products = new[]
            {
                new ProductDTO
                {
                    Id = 1,
                    Name = "Product 1",
                    Order = 0,
                    Price = 10m,
                    ImageUrl = "Product1.png",
                    Brand = new BrandDTO
                    {
                        Id = 1,
                        Name = "Brand of product 1"
                    },
                    Category = new CategoryDTO
                    {
                        Id = 1,
                        Name = "Section of product 1"
                    }
                },
                new ProductDTO
                {
                    Id = 2,
                    Name = "Product 2",
                    Order = 0,
                    Price = 20m,
                    ImageUrl = "Product2.png",
                    Brand = new BrandDTO
                    {
                        Id = 2,
                        Name = "Brand of product 2"
                    },
                    Category = new CategoryDTO
                    {
                        Id = 2,
                        Name = "Section of product 2"
                    }
                },
            };

            var product_data_mock = new Mock<IProductServices>();
            product_data_mock
               .Setup(p => p.GetProducts(It.IsAny<ProductFilter>()))
               .Returns(products);

            const int expected_category_id = 1;
            const int expected_brand_id = 5;

            var controller = new CatalogController(product_data_mock.Object);

            var result = controller.Shop(expected_brand_id, expected_category_id);

            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CatalogViewModel>(view_result.ViewData.Model);

            Assert.Equal(products.Length, model.Products.Count());
            Assert.Equal(expected_brand_id, model.BrandId);
            Assert.Equal(expected_category_id, model.CategoryId);
        }
    }
}
