using Business.Abstract;
using Core.Entites.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerBase
    {
        private readonly IOperationClaimService operationClaimService;
        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            this.operationClaimService = operationClaimService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetClaims()
        {
            var result = operationClaimService.GetAll();
            return Ok(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(OperationClaim operationClaim)
        {
            operationClaimService.Add(operationClaim);
            return Ok();
        }
    }
}
