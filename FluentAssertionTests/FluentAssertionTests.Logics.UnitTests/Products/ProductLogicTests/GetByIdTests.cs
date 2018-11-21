using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using FluentAssertionTests.Models;
using Moq;
using Xunit;

namespace FluentAssertionTests.Logics.UnitTests.Products.ProductLogicTests
{
    public class GetByIdTests : BaseTest
    {
        [Fact]
        public void Return_Product_From_Repository()
        {
            var logic = Create();

            var product = Builder<Product>.CreateNew().Build();

            Repository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(product);

            var result = logic.GetById(10);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(product, result.Value);
            Assert.NotNull(result.Errors);
            Assert.Equal(0, result.Errors.Count());

            Repository.Verify(r => r.GetById(10), Times.Once());
        }

        [Fact]
        public void Return_Error_When_Product_Not_Exist()
        {
            var logic = Create();

            Repository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns((Product)null);

            var result = logic.GetById(10);

            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Value);
            Assert.NotNull(result.Errors);
            Assert.Equal(1, result.Errors.Count());

            var error = result.Errors.First();

            Assert.Equal(string.Empty, error.PropertyName);
            Assert.Equal("Nie ma produktu o id 10.", error.Message);

            Repository.Verify(r => r.GetById(10), Times.Once());
        }

        [Fact]
        public void Return_Product_From_Repository2()
        {
            var logic = Create();

            var product = Builder<Product>.CreateNew().Build();

            Repository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(product);

            var result = logic.GetById(10);

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Value.Should().Be(product);
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(0);

            Repository.Verify(r => r.GetById(10), Times.Once());
        }

        [Fact]
        public void Return_Error_When_Product_Not_Exist2()
        {
            var logic = Create();

            Repository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns((Product)null);

            var result = logic.GetById(10);

            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(1);

            var error = result.Errors.First();

            error.PropertyName.Should().BeEmpty();
            error.Message.Should().Be("Nie ma produktu o id 10.");

            Repository.Verify(r => r.GetById(10), Times.Once());
        }

        [Fact]
        public void Return_Product_From_Repository3()
        {
            var logic = Create();

            var product = Builder<Product>.CreateNew().Build();

            Repository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(product);

            var result = logic.GetById(10);

            result.Should().BeSuccess(product);

            Repository.Verify(r => r.GetById(10), Times.Once());
        }

        [Fact]
        public void Return_Error_When_Product_Not_Exist3()
        {
            var logic = Create();

            Repository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns((Product)null);

            var result = logic.GetById(10);

            result.Should().BeFailure("Nie ma produktu o id 10.");

            Repository.Verify(r => r.GetById(10), Times.Once());
        }
    }
}
