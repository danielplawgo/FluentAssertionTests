using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertionTests.Logics.Products;
using FluentAssertionTests.Logics.Repositories;
using Moq;

namespace FluentAssertionTests.Logics.UnitTests.Products.ProductLogicTests
{
    public class BaseTest
    {
        protected Mock<IProductRepository> Repository { get; private set; }

        protected ProductLogic Create()
        {
            Repository = new Mock<IProductRepository>();

            return new ProductLogic(new Lazy<IProductRepository>(() => Repository.Object));
        }
    }
}
