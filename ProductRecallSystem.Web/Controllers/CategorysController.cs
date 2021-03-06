using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductRecallSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductRecallSystem.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class CategorysController : Controller
    {
        private string baseApiURL = "http://localhost:10447/api/Categorys";
        public async Task<IActionResult> Index()
        {
            List<Categorys> list = new List<Categorys>();
            JsonResponse jsonResponse = await GetCategorys();
            if (jsonResponse.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<Categorys>>(jsonResponse.Data.ToString()); //Deserialize Result
            }
            else
            {
                string msg = jsonResponse.Message.ToString();
                TempData["ErrorMsg"] = msg;
            }
            return View(list);
        }
        public async Task<JsonResponse> GetCategorys()
        {
            JsonResponse jsonResponse = new JsonResponse();
            var accessToken = HttpContext.Session.GetString("JWToken");
            string endPoint = $"{baseApiURL}/getAll";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await client.GetAsync(endPoint))
                {
                    string resultStr = response.Content.ReadAsStringAsync().Result.ToString();

                    jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(resultStr); //Deserialize Result
                }
            }

            return jsonResponse;
        }
    }
}
