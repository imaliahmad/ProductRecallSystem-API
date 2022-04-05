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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        List<Items> list = new List<Items>();

        [HttpGet("getAll")]
        public IActionResult GetItems()
        {
            list.Add(new Items() { ItemId = 1, Name = "Oranges", Price = 20, Category ="Fruits" });
            list.Add(new Items() { ItemId = 2, Name = "Grapes", Price = 20, Category ="Fruits" });
            list.Add(new Items() { ItemId = 3, Name = "Water", Price = 20, Category ="Beverages" });

            return Ok(new JsonResponse() { IsSuccess = true, Data = list});
        }

    }
}
