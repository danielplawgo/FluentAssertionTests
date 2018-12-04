using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class DetailsTests : BaseTest
    {
        protected ProductViewModel ViewModel { get; set; }

        protected Product Product { get; set; }

        protected Result<Product> ProductResult { get; set; }

        protected override ProductsController Create()
        {
            var controller = base.Create();

            Product = Builder<Product>.CreateNew().Build();

            ProductResult = Result.Ok(Product);

            Logic.Setup(l => l.GetById(It.IsAny<int>()))
                .Returns(() => ProductResult);

            ViewModel = Builder<ProductViewModel>.CreateNew().Build();

            Mapper.Setup(m => m.Map<ProductViewModel>(It.IsAny<Product>()))
                .Returns(ViewModel);

            return controller;
        }

        [Fact]
        public void Redirect_To_Index_When_Result_Is_Failure()
        {
            var controller = Create();

            ProductResult = Result.Failure<Product>("Property", "Error");

            var result = controller.Details(10);

            result.Should()
                .BeRedirectToRouteResult()
                .WithAction("Index");
        }

        [Fact]
        public void Return_View_With_Data_When_Result_Is_Failure()
        {
            var controller = Create();

            var result = controller.Details(10);

            result.Should()
                .BeViewResult()
                .WithDefaultViewName()
                .Model
                .Should()
                .BeEquivalentTo(ViewModel);
        }
    }
}
