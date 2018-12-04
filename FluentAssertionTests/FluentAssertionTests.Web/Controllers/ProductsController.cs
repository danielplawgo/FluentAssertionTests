using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using FluentAssertionTests.Logics;
using FluentAssertionTests.Logics.Products;
using FluentAssertionTests.Models;
using FluentAssertionTests.Web.ViewModels.Products;

namespace FluentAssertionTests.Web.Controllers
{
    public class ProductsController : Controller
    {
        private Lazy<IProductLogic> _logic;

        protected IProductLogic Logic
        {
            get { return _logic.Value; }
        }

        private Lazy<IMapper> _mapper;

        protected IMapper Mapper
        {
            get { return _mapper.Value; }
        }

        public ProductsController(Lazy<IProductLogic> logic,
            Lazy<IMapper> mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        public ActionResult Details(int id)
        {
            var result = Logic.GetById(id);

            if (result.Success == false)
            {
                return RedirectToAction("Index");
            }

            var viewModel = Mapper.Map<ProductViewModel>(result.Value);

            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View(new ProductViewModel());
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel viewModel)
        {
            var product = Mapper.Map<Product>(viewModel);

            var result = Logic.Create(product);

            if (result.Success == false)
            {
                result.AddErrorToModelState(ModelState);
                return View(viewModel);
            }

            return RedirectToAction("Index");
        }
    }
}