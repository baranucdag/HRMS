using Business.Abstract;
using Core.Utilities.Helpers.PaginationHelper;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.BaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobAdvertsController : BaseController
    {
        private readonly IJobAdvertService jobAdvertService;

        public JobAdvertsController(IJobAdvertService jobAdvertService)
        {
            this.jobAdvertService = jobAdvertService;
        }

        [HttpPost("GetPaginationData")]
        public IActionResult GetPaginationData(PaginationItem<JobAdvertDto> paginationItem)
        {
            var result = jobAdvertService.GetPaginationData(paginationItem);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetAll")]
        public IActionResult GetDetails()
        {
            var result = jobAdvertService.GetAllDetails();
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = jobAdvertService.GetAll();
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = jobAdvertService.GetById(id);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(JobAdvert jobAdvert)
        {
            var result = jobAdvertService.Add(jobAdvert);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(JobAdvert jobAdvert)
        {
            var result = jobAdvertService.Update(jobAdvert);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(int id)
        {
            var result = jobAdvertService.Delete(id);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("UnDelete")]
        public IActionResult UnDelete(int id)
        {
            var result = jobAdvertService.UnDelete(id);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
