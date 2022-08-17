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
                return BadRequest(userToLogin);
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
                    return BadRequest(userExists);
                }

                var registerResult = authService.RegisterUser(userForRegisterDto);
                if (registerResult.IsOk)
                {
                    User registerUser = (User)registerResult.Data;
                    userOperationClaimService.Add(new UserOperationClaim(){ OperationClaimId=3,UserId=registerUser.Id});
                    var result = authService.CreateAccessToken(registerUser);
                    if (result.IsOk)
                    {
                        return Ok(result);
                    }

                    return BadRequest(result);
                }
                return BadRequest(registerResult);

            }
            return BadRequest("Dto cannot be null !");
        }


        //todo : refactor this function
        //register a user with claim (only admin can access)
        [HttpPost("RegisterWithClaim")]
        public ActionResult RegisterWithAddClaim([FromForm] UserRegisterDto userForRegisterDto, [FromForm] OperationClaim operationClaim)
        {
            if (userForRegisterDto != null)
            {
                var userExists = authService.IsUserExists(userForRegisterDto.Email);
                if (!userExists.IsOk)
                {
                    return BadRequest(userExists);
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

                    return BadRequest(result);
                }
                return BadRequest(registerResult);

            }
            return BadRequest("Dto cannot be null !");
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var result = authService.ChangePassword(changePasswordDto);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
