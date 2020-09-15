using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public class TestModel
        {
            [Required]
            public int Id { get; set; }

            [MaxLength(11)]
            public int Iphone { get; set; }

            [Required]
            [MinLength(5)]
            public string Message { get; set; }
        }


        [HttpPost("/T2")]
        public string MyWebApi2([FromBody] TestModel model)
        {
            return "请求完成";
        }
    }
}
