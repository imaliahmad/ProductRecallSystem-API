using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductRecallSystem.API.APIModels.Request;
using ProductRecallSystem.API.APIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRecallSystem.API.EndPoints
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        List<Categorys> list = new List<Categorys>();

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            list.Add(new Categorys() { CategoryId = 1, Name = "Food"});
            list.Add(new Categorys() { CategoryId = 2, Name = "Fruits"});
            list.Add(new Categorys() { CategoryId = 3, Name = "Beverages" });

            return Ok(new JsonResponse() { IsSuccess = true, Data = list});
        }
    }
}
