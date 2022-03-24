using Microsoft.EntityFrameworkCore;
using ProductRecallSystem.BOL;
using ProductRecallSystem.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductRecallSystem.DAL
{
    public interface IManufacturersDb
    {
        Task<JsonResponse> GetAll();
        Task<JsonResponse> GetById(int id);
        Task<JsonResponse> Insert(Manufacturers obj);
        Task<JsonResponse> Update(Manufacturers obj);
        Task<JsonResponse> Delete(int id);
    }
    public class ManufacturersDb : IManufacturersDb
    {
        private EFCodeDbContext context;
        public ManufacturersDb(EFCodeDbContext _context)
        {
            context = _context;
        }
        public async Task<JsonResponse> Delete(int id)
        {
            var obj = await context.Manufacturers.FindAsync(id);
            if (obj == null)
            {
                return new JsonResponse() { IsSuccess = false, Message = "Record not found.", StatusCode = 404 };
            }
            else
            {
                context.Manufacturers.Remove(obj);
                await context.SaveChangesAsync();

                return new JsonResponse() { IsSuccess = true, Message = "Record deleted successfully.", StatusCode = 200 };
            }
        }

        public async Task<JsonResponse> GetAll()
        {
            var list = await context.Manufacturers.ToListAsync();
            return new JsonResponse() { IsSuccess = true, StatusCode = 200, Data = list };
        }

        public async Task<JsonResponse> GetById(int id)
        {
            var obj = await context.Manufacturers.FindAsync(id);
            if (obj == null)
            {
                return new JsonResponse() { IsSuccess = false, Message = "Record not found.", StatusCode = 404 };
            }
            else
            {
                return new JsonResponse() { IsSuccess = true, Data = obj, StatusCode = 200 };
            }
        }

        public async Task<JsonResponse> Insert(Manufacturers obj)
        {
            context.Manufacturers.Add(obj);
            int result = await context.SaveChangesAsync();
            if (result > 0) //save successfully
            {
                return new JsonResponse() { IsSuccess = true, Message = "Record saved successfully.", StatusCode = 200, Data = obj };
            }
            else
            {
                return new JsonResponse() { IsSuccess = false, Message = "Record saved failed.", StatusCode = 500 };
            }
        }

        public async Task<JsonResponse> Update(Manufacturers obj)
        {
            context.Manufacturers.Update(obj);
            int result = await context.SaveChangesAsync();
            if (result > 0) //save successfully
            {
                return new JsonResponse() { IsSuccess = true, Message = "Record saved successfully.", StatusCode = 200, Data = obj };
            }
            else
            {
                return new JsonResponse() { IsSuccess = false, Message = "Record saved failed.", StatusCode = 500 };
            }
        }
    }
}
