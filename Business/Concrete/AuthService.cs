using Business.Abstract;
using Business.Constans;
using Core.Entites.Concrete;
using Core.Utilities.Result;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Dto;

namespace Business.Concrete
{
    public class AuthService : IAuthService
    {
        private IUserService userService;
        private ITokenHelper tokenHelper;

        public AuthService(IUserService userService, ITokenHelper tokenHelper)
        {
            this.userService = userService;
            this.tokenHelper = tokenHelper;
        }

        //Register user
        public ResultItem RegisterUser(UserRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            userService.Add(user);
            return new ResultItem(true,user, Messages.UserRegistered);
        }

        //Login user
        public ResultItem Login(UserLoginDto userForLoginDto)
        {
            var userToCheck = userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ResultItem(false,null,Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ResultItem(false,null,Messages.PasswordError);
            }

            return new ResultItem(true,userToCheck,Messages.SuccessfulLogin);
        }

        //change password by verify old password
        public ResultItem ChangePassword(ChangePasswordDto changePasswordDto)
        {
            byte[] passwordHash, passwordSalt;
            var userToCheck = userService.GetById(changePasswordDto.UserId).Data;
            User user = (User)userToCheck;
            if(userToCheck == null)
            {
                return new ResultItem(false,null,Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(changePasswordDto.OldPassword,user.PasswordHash,user.PasswordSalt))
            {
                return new ResultItem(false,null,Messages.PasswordError);
            }
            HashingHelper.CreatePasswordHash(changePasswordDto.NewPassword,out passwordHash,out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            userService.Update(user);
            return new ResultItem(true, user, Messages.passwordUpdated);
        }

        //Check if user is already exist
        public ResultItem IsUserExists(string email)
        {
            if (email != null)
            {
                if (userService.GetByMail(email) != null)
                {
                    return new ResultItem(false,null,Messages.UserAlreadyExists,System.Net.HttpStatusCode.BadRequest, "1");
                }
                return new ResultItem();
            }
            return new ResultItem(false,null,Messages.EmailNullError,System.Net.HttpStatusCode.BadRequest, "1");

        }

        //Create access token when user is logged in or registered
        public ResultItem CreateAccessToken(User user)
        {
            var claims = userService.GetClaims(user);
            var accessToken = tokenHelper.CreateToken(user, claims);
            return new ResultItem(true,accessToken, Messages.AccessTokenCreated);
        }
    }
}
