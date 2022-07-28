﻿using Business.Abstract;
using Core.Utilities.Helpers.PaginationHelper;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : BaseController
    {
        private readonly IAnswerService answerService;

        public AnswersController(IAnswerService answerService)
        {
            this.answerService = answerService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = answerService.GetAll();
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);  
        }

        [HttpPost("Add")]
        public IActionResult Add(Answer answer)
        {
            var result = answerService.Add(answer);
            if (result.IsOk)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetPaginationData")]
        public IActionResult GetPaginationData(PaginationItem<Answer> pi)
        {
            var result = answerService.GetPaginationData(pi);
            return result.IsOk ? Ok(result) : BadRequest(result);
        }
    }
}
