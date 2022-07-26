using Core.Entites.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Dto;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> RegisterUser(UserRegisterDto userForRegisterDto);
        IDataResult<User> Login(UserLoginDto userForLoginDto);
        IResult IsUserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
