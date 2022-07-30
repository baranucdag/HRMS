using Core.Entites.Concrete;
using Core.Utilities.Result;
using Core.Utilities.Security.JWT;
using Entities.Dto;

namespace Business.Abstract
{
    public interface IAuthService
    {
        ResultItem RegisterUser(UserRegisterDto userForRegisterDto);
        ResultItem Login(UserLoginDto userForLoginDto);
        ResultItem IsUserExists(string email);
        ResultItem CreateAccessToken(User user);
    }
}
