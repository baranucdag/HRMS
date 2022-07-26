using Business.Abstract;
using Business.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery]QueryParams queryParams)
        {
            var result = candidateService.GetAll(queryParams);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Add")]
        public void Add(Candidate candidate)
        {
            if(candidate == null)
            {
                candidateService.Add(candidate);
            }
             candidateService.Add(candidate);            
        }
    }
}
