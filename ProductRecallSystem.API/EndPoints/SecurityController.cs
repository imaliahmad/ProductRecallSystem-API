using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductRecallSystem.API.APIModels.Request;
using ProductRecallSystem.BOL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductRecallSystem.API.EndPoints
{
    [AllowAnonymous]
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
                    
                    var user = new AppUsers() { Email = registerVM.Email, UserName = registerVM.UserName, Password = registerVM.Password };
                    var userResult = await userManager.CreateAsync(user, registerVM.Password);
                    if (userResult.Succeeded)
                    {
                       var roleResult = await userManager.AddToRoleAsync(user, "Customer");
                        if (roleResult.Succeeded)
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
                   var user = await userManager.FindByNameAsync(signInVM.UserName);

                    //var signInResult = await signInManager.PasswordSignInAsync
                    //                                        (signInVM.UserName, signInVM.Password, false, false);

                   var signInResult = await signInManager.PasswordSignInAsync(user, user.Password, false,false);

                    var roles =await userManager.GetRolesAsync(user);

                    if (signInResult.Succeeded)
                    {
                        //generate Token
                        var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Module-Secret-Key"));
                        var credentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);

                        var identityOptions = new IdentityOptions();

                        Claim[] myClaims =
                        {
                          new Claim(identityOptions.ClaimsIdentity.UserIdClaimType, user.Id),
                          new Claim(identityOptions.ClaimsIdentity.UserNameClaimType, user.UserName),
                          new Claim(identityOptions.ClaimsIdentity.RoleClaimType, roles[0])
                       };

                        var token = new JwtSecurityToken(
                            issuer: "Module",
                            audience: "Module",
                            claims: myClaims,
                            expires: DateTime.Now.AddMinutes(120),
                            signingCredentials: credentials
                            );
                        var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);


                        return Ok(new JsonResponse() { IsSuccess = true, Data = encodeToken});
                    }
                    else
                    {
                        //foreach (var item in signInResult.err)
                        //{
                        //    ModelState.AddModelError("", item.Description);
                        //}
                        return BadRequest(new JsonResponse() { IsSuccess = false, Message = "Username/Password is incorrect."});
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
