using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FluentAssertionTests.Logics
{
    public static class ResultExtensions
    {
        public static void AddErrorToModelState(this Result result, ModelStateDictionary modelState)
        {
            if (result.Success)
            {
                return;
            }

            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.Message);
            }
        }
    }
}