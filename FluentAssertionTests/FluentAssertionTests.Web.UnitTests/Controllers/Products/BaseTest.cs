using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertionTests.Logics.Products;
using FluentAssertionTests.Web.Controllers;
using Moq;

namespace FluentAssertionTests.Web.UnitTests.Controllers.Products
{
    public class BaseTest
    {
        protected Mock<IProductLogic> Logic { get; set; }

        protected Mock<IMapper> Mapper { get; set; }

        protected virtual ProductsController Create()
        {
            Logic = new Mock<IProductLogic>();

            Mapper = new Mock<IMapper>();

            return new ProductsController(new Lazy<IProductLogic>(() => Logic.Object), 
                new Lazy<IMapper>(() => Mapper.Object));
        }
    }
}
