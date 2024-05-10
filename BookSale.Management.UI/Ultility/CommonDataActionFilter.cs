using BookSale.Managament.Domain.Entities;
using BookSale.Managament.Domain.Setting;
using BookSale.Management.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookSale.Management.UI.Ultility
{
    public class CommonDataActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var carts = context.HttpContext.Session.Get<List<CartModel>>(CommonConstant.CartSessionName);

            if (carts is not null)
            {
                var controller = context.Controller as Controller;

                controller.ViewData["NumberCart"] = carts.Count;
            }
        }
    }

    public class SiteAreaConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var areaAtribute = controller.Attributes.OfType<AreaAttribute>().FirstOrDefault();

            if (String.IsNullOrEmpty(areaAtribute?.RouteValue))
            {
                controller.Filters.Add(new CommonDataActionFilter());
            }
        }
    }
}
