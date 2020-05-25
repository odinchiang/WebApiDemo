using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Filters;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [CtmActionFilter]
    [EnableCors("any")]
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // [ApiController] ASP.NET Core 3.x 後才有
        // [ApiController] 對 Get 沒有影響，但對 Post 影響很大
        // [ApiController] 的作用
        // 1. 參數綁定策略的自動推斷，不需要對參數指定 [FromBody] 或 [FromData]。
        // 2. 自動驗證模型狀態，若驗證不通過會自動返回 400。

        // https://localhost:44374/Home
        //[HttpGet]
        //public string Get()
        //{
        //    return "GetValue Ok1";
        //}

        // https://localhost:44374/Home
        // https://localhost:44374/Home?str=test
        //[CtmActionFilter]
        [HttpGet]
        public string Get(string str)
        {
            return $"GetValue {str}";
        }

        // https://localhost:44374/Home/1234
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"GetValue {id}";
        }

        // https://localhost:44374/Home/1234/Mark
        [HttpGet("{id}/{name}")]
        public string Get(int id, string name)
        {
            return $"GetValue {id}: {name}";
        }

        [HttpGet("/GetValue2")]
        public string GetValue2()
        {
            return "GetValue2 Ok2";
        }

        // https://localhost:44374/Home/GetValue/3
        [HttpGet("GetValue/{id}")]
        public string GetValue(int id)
        {
            return $"Get {id}";
        }

        // https://localhost:44374/Home
        [HttpPost]
        public string Post()
        {
            return "Post";
        }

        // https://localhost:44374/Home/Post
        //[HttpPost("/[controller]/Post")]
        //public string Post(string name, int age)
        //{
        //    return $"Post {name}, {age}";
        //}

        // 因為有 [ApiController] 的關係，Post 無法接受多個參數傳輸，
        // 必須包成一個 ViewModel 進行傳輸，且參數前的 [FromBody] 可省略。
        [HttpPost("/[controller]/Post")]
        public string Post(UserInput user)
        {
            return $"Post {user.Name}, {user.Age}";
        }

        [HttpPut]
        public string Put()
        {
            return "Put";
        }

        [HttpDelete]
        public string Delete()
        {
            return "Delete";
        }
    }
}