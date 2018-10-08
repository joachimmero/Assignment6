using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace ApiTeht
{
    public class AdminRequirement : AuthorizationHandler<AdminRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
           if(!context.User.HasClaim(c => c.Type == "AdminNumber")){
                context.Fail();
                return Task.CompletedTask;
            }         
            else{
                if(context.User.FindFirst("AdminNumber").Value == "1111"){
                    context.Succeed(this);
                    return Task.CompletedTask;
                }
                else{
                    context.Fail();
                    return Task.CompletedTask;
                }
            }
        }
    }
}