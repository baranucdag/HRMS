using Business.Abstract;
using Core.Entites.Concrete;
using Core.Utilities.Helpers.PaginationHelper;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("GetPaginationData")]
        public IActionResult GetPaginationData(PaginationItem<UserDto> paginationItem)
        {
            var result = userService.GetUsersPaginated(paginationItem);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = userService.GetById(id);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpPost("Add")]
        public IActionResult Add(User user)
        {
            return Ok(user);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(User user)
        {
            return Ok();
        }
    }
}
