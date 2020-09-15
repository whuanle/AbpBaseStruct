using AbpBase.Domain.Shared.Apis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace AbpBase.Web.Filters
{
    public class GlabalInvalidModelStateFilter : ActionFilterAttribute, IActionFilter
    {
        /// <summary>
        /// 统一模型验证
        /// <para>控制器必须添加 [ApiController] 才能被此过滤器拦截</para>
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            int count = context.ModelState.Count;
            ValidationErrors[] errors = new ValidationErrors[count];
            int i = 0;
            foreach (var item in context.ModelState)
            {
                errors[i] = new ValidationErrors
                {
                    Member = item.Key,
                    Messages = item.Value.Errors?.Select(x => x.ErrorMessage).ToArray()
                };
                i++;
            }
            // 响应消息
            var result = ApiResponseModel.Create(HttpStateCode.Status400BadRequest, CommonResponseType.BadRequest, errors);
            var objectResult = new BadRequestObjectResult(result);
            objectResult.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = objectResult;
        }

        /// <summary>
        /// 用于格式化实体验证信息的模型
        /// </summary>
        private class ValidationErrors
        {
            /// <summary>
            /// 验证失败的字段
            /// </summary>
            public string Member { get; set; }

            /// <summary>
            /// 此字段有何种错误
            /// </summary>
            public string[] Messages { get; set; }
        }
    }
}
