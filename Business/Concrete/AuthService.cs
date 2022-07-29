using Business.Abstract;
using Business.Constans;
using Core.Entites.Concrete;
using Core.Utilities.Result;
using Core.Utilities.Results;
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

        public ResultItem Login(UserLoginDto userForLoginDto)
        {
            var userToCheck = userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ResultItem(true,Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ResultItem(true,Messages.PasswordError);
            }

            return new ResultItem(true,userToCheck,Messages.SuccessfulLogin);
        }

        public ResultItem IsUserExists(string email)
        {
            if (email != null)
            {
                if (userService.GetByMail(email) != null)
                {
                    return new ResultItem(false,null,Messages.UserAlreadyExists);
                }
                return new ResultItem();
            }
            return new ResultItem(false,Messages.EmailNullError);

        }

        public ResultItem CreateAccessToken(User user)
        {
            var claims = userService.GetClaims(user);
            var accessToken = tokenHelper.CreateToken(user, claims);
            return new ResultItem(true,accessToken, Messages.AccessTokenCreated);
        }
    }
}
