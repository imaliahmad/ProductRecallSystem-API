using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductRecallSystem.API.APIModels.Request;
using ProductRecallSystem.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRecallSystem.API.EndPoints
{
    //[AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        UserManager<AppUsers> userManager;
        SignInManager<AppUsers> signInManager;
        public SecurityController(UserManager<AppUsers> _userManager, 
                                                  SignInManager<AppUsers> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterVM registerVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    var user = new AppUsers() { Email = registerVM.Email, UserName = registerVM.UserName };
                    var userResult = await userManager.CreateAsync(user, registerVM.Password);
                    if (userResult.Succeeded)
                    {
                        return Ok(user);
                    }
                    else
                    {
                        foreach (var item in userResult.Errors)
                        {
                            ModelState.AddModelError("", item.Description); 
                        }
                        return BadRequest(ModelState.Values);
                    }
                }
                return BadRequest(ModelState.Values);
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                ModelState.AddModelError("", msg);
                return BadRequest(ModelState.Values);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInVM signInVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var signInResult = await signInManager.PasswordSignInAsync
                                                            (signInVM.UserName, signInVM.Password, false, false);

                    if (signInResult.Succeeded)
                    {
                        return Ok();
                    }
                    else
                    {
                        //foreach (var item in signInResult.err)
                        //{
                        //    ModelState.AddModelError("", item.Description);
                        //}
                        return BadRequest(ModelState.Values);
                    }
                }
                return BadRequest(ModelState.Values);
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                ModelState.AddModelError("", msg);
                return BadRequest(ModelState.Values);
            }
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }
    }

}
