using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ApiTeht{
    public class CustomExceptionFilter : ExceptionFilterAttribute{

        public override void OnException(ExceptionContext context){
            if(context.Exception is CustomException){
                context.Result = new BadRequestObjectResult("2 noob 4 MegisBurg");
            }
        }
    }
}