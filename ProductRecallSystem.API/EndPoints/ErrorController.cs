using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductRecallSystem.API.APIModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRecallSystem.API.EndPoints
{
    public class ErrorController : Controller
    {
        [Route("ErrorLog/{statusCode}")]
        public JsonResponse HttpStatusCodeHandler(int statusCode)
        {
            JsonResponse response = new JsonResponse();
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                case 405:
                    {
                        response.IsSuccess = false;
                        response.StatusCode = statusCode;
                        response.Message = "Sorry, the resource you requested could not be found.";
                        response.Description = $"Module: API, {"\n"} ErrorPath: {statusCodeResult.OriginalPath ?? ""}\n, QueryString: {statusCodeResult.OriginalQueryString ?? ""}";

                        return response;
                    }
                case 401:
                    {
                        response.IsSuccess = false;
                        response.StatusCode = statusCode;
                        response.Message = "Sorry, you are not authorized to access this page. Please contact to your admin.";
                        response.Description = $"Module: API, {"\n"} ErrorPath: {statusCodeResult.OriginalPath ?? ""}\n, QueryString: {statusCodeResult.OriginalQueryString ?? ""}";

                        return response;
                    }
            }
            return response;
        }
        [Route("Error")]
        public JsonResponse Error()
        {
            var statusCodeResult = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            JsonResponse response = new JsonResponse();

            response.IsSuccess = false;
            response.StatusCode= 500;
            response.Message = statusCodeResult.Error.InnerException != null ? statusCodeResult.Error.InnerException.Message : statusCodeResult.Error.Message;
            response.Description = $"Module: API, {"\n"} StackTrace: {statusCodeResult.Error.StackTrace ?? ""}\n";


            return response;
        }
    }
}
