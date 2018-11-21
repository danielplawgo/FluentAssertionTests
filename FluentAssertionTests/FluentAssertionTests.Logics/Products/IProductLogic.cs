using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertionTests.Models;

namespace FluentAssertionTests.Logics.Products
{
    public interface IProductLogic
    {
        Result<Product> GetById(int id);
    }
}
