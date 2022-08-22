using Business.Abstract;
using Core.Utilities.Helpers.PaginationHelper;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : BaseController
    {
        private readonly ICandidateService candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            this.candidateService = candidateService;
        }

        [HttpPost("Add")]
        public IActionResult Add(Candidate candidate)
        {
            var result = candidateService.Add(candidate);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Update")]
        public IActionResult Update(Candidate candidate)
        {
            var result = candidateService.Update(candidate);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(int id)
        {
            var result = candidateService.Delete(id);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("UnDelete")]
        public IActionResult UnDelete(int id)
        {
            var result = candidateService.UnDelete(id);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetPaginationData")]
        public IActionResult GetPaginationData(PaginationItem<CandidateDto> pi)
        {
            var result = candidateService.GetCandidatesPaginated(pi);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = candidateService.GetById(id);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("GetByUserId")]
        public IActionResult GetByUserId(int id)
        {
            var result = candidateService.GetByUserId(id);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
