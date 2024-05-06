using BookSale.Management.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookSale.Management.UI.Ultility
{
    public class BreadscumbAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly string _masterName;
        private readonly string _title;
        public BreadscumbAttribute(string title, string masterName = "")
        {
            _masterName = masterName;
            _title = title;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller is Controller controller)
            {
                string controllerName = controller.GetType().Name.Replace("Controller", "");

                string path = string.IsNullOrEmpty(_masterName) ? $"{controllerName}" : $"{_masterName}/{controllerName}/{_title}";

                controller.ViewData["Breadscumb"] = new BreadscumbModel
                {
                    Title = _title,
                    Path = path
                };
            }
        }
    }
}
