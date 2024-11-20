using Api_Project_Prn.Infra;
using Api_Project_Prn.Infra.Constants;
using Api_Project_Prn.Infra.Entities;
using Api_Project_Prn.Services.CacheService;
using Microsoft.EntityFrameworkCore;

namespace Api_Project_Prn.Services
{
    public interface IVolumeDiscountService
    {
        Task<VolumeDiscount> SaveVolumeDiscount(VolumeDiscount model);
        Task<List<VolumeDiscount>> GetVolumeDiscountList();
    }

    public class VolumeDiscountService : IVolumeDiscountService
    {
        private readonly BackendInterviewContext _context;
        private readonly ICacheService _cacheService;

        public VolumeDiscountService(BackendInterviewContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }
        public async Task<List<VolumeDiscount>> GetVolumeDiscountList()
        {
            var cacheKey = RedisCacheKey.LIST_VOLUME_DISCOUNT;
            var data = await _cacheService.GetAsync<List<VolumeDiscount>>(cacheKey);
            if(data == null)
            {
                data = await _context.VolumeDiscounts.ToListAsync();
                _ = _cacheService.SetAsync(cacheKey, data);
            }
            return data;
        }

        public async Task<VolumeDiscount> SaveVolumeDiscount(VolumeDiscount model)
        {
            if(string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.Campaign))
            {
                throw new ApplicationException("Title or campaign cannot be empty!");
            }
            if(model.DiscountRule == null || model.DiscountRule.Count() == 0)
            {
                throw new ApplicationException("Discount Rule must have atleast 1");
            }

            var exist = await _context.VolumeDiscounts.FirstOrDefaultAsync(t => t.Id == model.Id);
            if (exist == null)
            {
                exist = model;
                await _context.VolumeDiscounts.AddAsync(exist);
            }
            else
            {
                exist = model;
                _context.Update(exist);
            }

            await _context.SaveChangesAsync();

            _ = _cacheService.DeleteAsync(RedisCacheKey.LIST_VOLUME_DISCOUNT);

            return exist;
        }
    }
}
