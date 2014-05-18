using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POA.Domain;
using POA.Web.Infrastructure;

namespace POA.Web.Filters
{
    public class LogAttribute: ActionFilterAttribute
    {
        private IDictionary<string, object> _parameters;
        public string Description { get; set; }
        public POADBEntities Context { get; set; }
        public LogAction ObjLogAction { get; set; }
        public ICurrentUser objCurrentUser { get; set; }


        public LogAttribute(string description )
        {
            Description = description;
            
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _parameters = filterContext.ActionParameters;
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
            foreach (var kvp in _parameters)
            {
                Description = Description.Replace("{" + kvp.Key + "}", kvp.Value.ToString());
            }

            ObjLogAction.ActionBy = filterContext.HttpContext.User.Identity.Name.ToString();
            ObjLogAction.Action = filterContext.ActionDescriptor.ActionName;
            ObjLogAction.Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            ObjLogAction.Description = Description;
            ObjLogAction.ActionDate = DateTime.Now;           

            Context.LogActions.Add(ObjLogAction);
            Context.SaveChanges();
        }
    }
}