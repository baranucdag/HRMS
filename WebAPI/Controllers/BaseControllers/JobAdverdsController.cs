using Business.Abstract;
using Core.Utilities.Helpers.PaginationHelper;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.BaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobAdverdsController : BaseController
    {
        private readonly IJobAdvertService jobAdvertService;

        public JobAdverdsController(IJobAdvertService jobAdvertService)
        {
            this.jobAdvertService = jobAdvertService;
        }

        [HttpGet("GetPaginatedData")]
        public IActionResult GetPaginatedData(PaginationItem<JobAdvert> paginationItem)
        {
            var result = jobAdvertService.GetPaginationData(paginationItem);
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


    }
}
