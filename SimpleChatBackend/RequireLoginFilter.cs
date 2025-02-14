using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleChatShared;

namespace SimpleChatBackend;

public class RequireLoginFilter(ChatDbContext dbContext) : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context) { }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var endpoint = context.ActionDescriptor.EndpointMetadata;

        var requiredParamAttribute = endpoint.OfType<RequireLoginAttribute>().FirstOrDefault();

        if (requiredParamAttribute != null)
        {
            var query = context.HttpContext.Request.Query;

            if (!query.ContainsKey("token"))
            {
                context.Result = new UnauthorizedResult();

                return;
            }

            string token = query["token"];
            ChatUser? user = dbContext.ChatUsers.FirstOrDefault(u => u.Token == token);
            if (user == null)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
