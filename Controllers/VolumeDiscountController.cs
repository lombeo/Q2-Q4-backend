using Api_Project_Prn.DTO.Common;
using Api_Project_Prn.Infra.Entities;
using Api_Project_Prn.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api_Project_Prn.Controllers
{
    [ApiController]
    [Route("api/volume-discount")]
    public class VolumeDiscountController : BaseAPIController
    {
        private readonly IVolumeDiscountService _volumeDiscountService;

        public VolumeDiscountController(IVolumeDiscountService volumeDiscountService)
        {
            _volumeDiscountService = volumeDiscountService;
        }

        [HttpGet("get-volume-discount-list")]
        public async Task<ResponseDTO<List<VolumeDiscount>>> GetVolumeDiscountList()
        {
            var list = _volumeDiscountService.GetVolumeDiscountList();
            return await HandleException(list);
        }

        [HttpPost("get-volume-discount-list")]
        public async Task<ResponseDTO<VolumeDiscount>> SaveVolumeDiscount([FromBody] VolumeDiscount model)
        {
            var result = _volumeDiscountService.SaveVolumeDiscount(model);
            return await HandleException(result);
        }
    }
}
