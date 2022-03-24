
using ProductRecallSystem.BOL;
using ProductRecallSystem.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductRecallSystem.BLL
{
    public interface IManufacturersBs
    {
        Task<JsonResponse> GetAll();
        Task<JsonResponse> GetById(int id);
        Task<JsonResponse> Insert(Manufacturers obj);
        Task<JsonResponse> Update(Manufacturers obj);
        Task<JsonResponse> Delete(int id);
    }
    public class ManufacturersBs : IManufacturersBs
    {
        private IManufacturersDb objDb;
        public ManufacturersBs(IManufacturersDb _objDb)
        {
            objDb = _objDb;
        }
        public Task<JsonResponse> Delete(int id)
        {
            return objDb.Delete(id);
        }

        public Task<JsonResponse> GetAll()
        {
            return objDb.GetAll();
        }

        public Task<JsonResponse> GetById(int id)
        {
            return objDb.GetById(id);
        }

        public Task<JsonResponse> Insert(Manufacturers obj)
        {
            return objDb.Insert(obj);
        }

        public Task<JsonResponse> Update(Manufacturers obj)
        {
            return objDb.Update(obj);
        }
    }
}
