using Business.Abstract;
using Core.Entites.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : ControllerBase
    {
        private readonly IUserOperationClaimService userOperationClaimService;

        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
        {
            this.userOperationClaimService = userOperationClaimService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = userOperationClaimService.GetAll();
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(UserOperationClaim userOperationClaim)
        {
            var result = userOperationClaimService.Add(userOperationClaim);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(UserOperationClaim userOperationClaim)
        {
            var result = userOperationClaimService.Delete(userOperationClaim);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(UserOperationClaim userOperationClaim)
        {
            var result = userOperationClaimService.Update(userOperationClaim);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
