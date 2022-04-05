using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductRecallSystem.API.APIModels.Request;
using ProductRecallSystem.API.APIModels.Response;
using ProductRecallSystem.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRecallSystem.API.EndPoints
{
    //[Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ManufactuersController : ControllerBase
    {
        private IManufacturersBs objBs;
        public ManufactuersController(IManufacturersBs _objBs)
        {
            objBs = _objBs;
        }

        [HttpGet("getAll")]
        public async  Task<IActionResult> GetAll()
        {
            try
            {
                var response = await objBs.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new JsonResponse() { IsSuccess = false, Message = msg, StatusCode = 500 });
            }
        }
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await objBs.GetById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new JsonResponse() { IsSuccess = false, Message = msg, StatusCode = 500 });
            }
        }

        //header, body --> Data
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Manufacturers model)
        {
            try
            {
                ProductRecallSystem.BOL.Manufacturers obj = new BOL.Manufacturers()
                { ManufacturerId = model.ManufacturerId, Name = model.Name, Address = model.Address, City = model.City, State = model.State};

                var response = await objBs.Insert(obj);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new JsonResponse() { IsSuccess = false, Message = msg, StatusCode = 500 });
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Manufacturers model)
        {
            try
            {
                ProductRecallSystem.BOL.Manufacturers obj = new BOL.Manufacturers()
                { ManufacturerId = model.ManufacturerId, Name = model.Name, Address = model.Address, City = model.City, State = model.State };

                var response = await objBs.Update(obj);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new JsonResponse() { IsSuccess = false, Message = msg, StatusCode = 500 });
            }
        }
    }
}
