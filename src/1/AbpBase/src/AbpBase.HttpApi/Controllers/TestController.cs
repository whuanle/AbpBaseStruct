using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace AbpBase.Web.Controllers
{
    [ApiController]
    public class TestController : AbpController
    {
        [HttpGet("/T")]
        public string MyWebApi()
        {
            return "应用启动成功！";
        }
    }
}
