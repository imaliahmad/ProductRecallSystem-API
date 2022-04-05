using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductRecallSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductRecallSystem.Web.Controllers
{
    public class SecurityController : Controller
    {
        private readonly string apiBaseURL = "http://localhost:10447/api/Security";
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                string endPoint = $"{apiBaseURL}/Login";
                using (var client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    using (var response = await client.PostAsync(endPoint, content))
                    {
                        string resultStr = response.Content.ReadAsStringAsync().Result.ToString();
                        var result = JsonConvert.DeserializeObject<JsonResponse>(resultStr);

                        if (response.IsSuccessStatusCode)
                        {
                            HttpContext.Session.Clear();
                            HttpContext.Session.SetString("JWToken", result.Data.ToString());
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.ToString());
                            }
                            return View();
                        }

                    }
                }
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Security");
        }
    }
}
