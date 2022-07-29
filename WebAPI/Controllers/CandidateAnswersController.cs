using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateAnswersController : BaseController
    {
        private readonly ICandidateAnswerService candidateAnswerService;
        public CandidateAnswersController(ICandidateAnswerService candidateAnswerService)
        {
            this.candidateAnswerService = candidateAnswerService;
        }

        [HttpGet("GetCandidateAnswerDetails")]
        public IActionResult GetCandidateAnswerDetails()
        {
            var result = candidateAnswerService.GetCandidateAnswerDetails();
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetCandidateAnswerDetailsById")]
        public IActionResult GetCandidateAnswerDetailsById(int id)
        {
            var result = candidateAnswerService.GetCandidateAnswerDetailsById(id);
            //CandidateAnswer candidateAnswer = (CandidateAnswer)result.Data;
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(CandidateAnswer candidateAnswer)
        {
            var result = candidateAnswerService.Add(candidateAnswer);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(int id)
        {
            var result = candidateAnswerService.Delete(id);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
