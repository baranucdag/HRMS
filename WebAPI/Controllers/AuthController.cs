using Business.Abstract;
using Core.Entites.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService authService;
        private readonly IUserOperationClaimService userOperationClaimService;

        public AuthController(IAuthService authService, IUserOperationClaimService userOperationClaimService)
        {
            this.authService = authService;
            this.userOperationClaimService = userOperationClaimService;
        }

        [HttpPost("Login")]
        public ActionResult Login(UserLoginDto userLoginDto)
        {
            var userToLogin = authService.Login(userLoginDto);
            if (!userToLogin.IsOk)
            {
                return BadRequest(userToLogin.Message);
            }
            User userToCheck = (User)userToLogin.Data;
            var result = authService.CreateAccessToken(userToCheck);
            if (result.IsOk)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("Register")]
        public ActionResult Register(UserRegisterDto userForRegisterDto)
        {
            if (userForRegisterDto != null)
            {
                var userExists = authService.IsUserExists(userForRegisterDto.Email);
                if (!userExists.IsOk)
                {
                    return BadRequest(userExists.Message);
                }

                var registerResult = authService.RegisterUser(userForRegisterDto);
                if (registerResult.IsOk)
                {
                    User registerUser = (User)registerResult.Data;
                    var result = authService.CreateAccessToken(registerUser);
                    if (result.IsOk)
                    {
                        return Ok(result);
                    }

                    return BadRequest(result.Message);
                }
                return BadRequest(registerResult.Message);

            }
            return BadRequest("Dto cannot be null !");
        }

        [HttpPost("RegisterWithClaim")]
        public ActionResult RegisterWithAddClaim([FromForm] UserRegisterDto userForRegisterDto, [FromForm] OperationClaim operationClaim)
        {
            if (userForRegisterDto != null)
            {
                var userExists = authService.IsUserExists(userForRegisterDto.Email);
                if (!userExists.IsOk)
                {
                    return BadRequest(userExists.Message);
                }

                var registerResult = authService.RegisterUser(userForRegisterDto);
                if (registerResult.IsOk)
                {
                    User registerUser = (User)registerResult.Data;
                    var result = authService.CreateAccessToken(registerUser);
                    if (result.IsOk)
                    {
                        UserOperationClaim userOperationClaim = new UserOperationClaim() { OperationClaimId = operationClaim.Id, UserId = registerUser.Id };
                        userOperationClaimService.Add(userOperationClaim);
                        return Ok(result);
                    }

                    return BadRequest(result.Message);
                }
                return BadRequest(registerResult.Message);

            }
            return BadRequest("Dto cannot be null !");

        }


    }
}
