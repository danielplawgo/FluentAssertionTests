using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertionTests.Logics.Repositories;
using FluentAssertionTests.Models;

namespace FluentAssertionTests.Logics.Products
{
    public class ProductLogic : IProductLogic
    {
        private Lazy<IProductRepository> _repository;

        protected IProductRepository Repository
        {
            get { return _repository.Value; }
        }

        public ProductLogic(Lazy<IProductRepository> repository)
        {
            _repository = repository;
        }

        public Result<Product> GetById(int id)
        {
            var product = Repository.GetById(id);

            if (product == null)
            {
                return Result.Failure<Product>($"Nie ma produktu o id {id}.");
            }

            return Result.Ok(product);
        }
    }
}
