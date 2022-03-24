using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRecallSystem.API.APIModels.Response
{
    public class JsonResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public object Data { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }
    }
}
