﻿using Business.Abstract;
using Core.Utilities.Helpers.PaginationHelper;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : BaseController
    {
        private readonly IApplicationService applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        [HttpGet("GetApplicationDetails")]
        public IActionResult GetApplicationDetails()
        {
            var result = applicationService.GetApplicationDetails();
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetPaginationData")]
        public IActionResult GetPaginationData(PaginationItem<ApplicationDetailDto> pi)
        {
            var result = applicationService.GetApplicationPaginated(pi);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(Application application)
        {
            var result = applicationService.Add(application);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(Application application)
        {
            var result = applicationService.Update(application);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(int id)
        {
            var result = applicationService.Delete(id);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("UnDelete")]
        public IActionResult UnDelete(int id)
        {
            var result = applicationService.UnDelete(id);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
    }
}
