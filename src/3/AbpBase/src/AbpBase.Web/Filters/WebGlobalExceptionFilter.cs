using AbpBase.Domain.Shared.Apis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AbpBase.Web.Filters
{

    /// <summary>
    /// Web 全局异常过滤器，处理 Web 中出现的、运行时未处理的异常
    /// </summary>
    public class WebGlobalExceptionFilter : IAsyncExceptionFilter
    {

        public async Task OnExceptionAsync(ExceptionContext context)
        {

            if (!context.ExceptionHandled)
            {

                ApiResponseModel model = ApiResponseModel.Create(
                    HttpStateCode.Status500InternalServerError,
                    CommonResponseType.Status500InternalServerError);

                context.Result = new ContentResult
                {
                    Content = JsonConvert.SerializeObject(model),
                    StatusCode = StatusCodes.Status200OK,
                    ContentType = "application/json; charset=utf-8"
                };
            }

            context.ExceptionHandled = true;

            await Task.CompletedTask;
        }
    }
}
