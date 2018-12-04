using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using FluentAssertions.Mvc;
using FluentAssertionTests.Logics;
using FluentAssertionTests.Models;
using FluentAssertionTests.Web.Controllers;
using FluentAssertionTests.Web.ViewModels.Products;
using Moq;
using Xunit;

namespace FluentAssertionTests.Web.UnitTests.Controllers.Products
{
    public class CreateTests : BaseTest
    {
        protected ProductViewModel ViewModel { get; set; }

        protected Product Product { get; set; }

        protected Result<Product> ProductResult { get; set; }

        protected override ProductsController Create()
        {
            var controller = base.Create();

            Product = Builder<Product>.CreateNew().Build();

            ProductResult = Result.Ok(Product);

            Logic.Setup(l => l.Create(It.IsAny<Product>()))
                .Returns(() => ProductResult);

            ViewModel = Builder<ProductViewModel>.CreateNew().Build();

            Mapper.Setup(m => m.Map<Product>(It.IsAny<ProductViewModel>()))
                .Returns(Product);

            return controller;
        }

        [Fact]
        public void Return_View_With_Errors_When_Result_Is_Failure()
        {
            var controller = Create();

            ProductResult = Result.Failure<Product>("Property", "Error");

            var result = controller.Create(ViewModel);

            result.Should()
                .BeViewResult()
                .WithDefaultViewName()
                .Model
                .Should()
                .BeEquivalentTo(ViewModel);

            controller.Should()
                .HasError("Property", "Error");
        }
    }
}
