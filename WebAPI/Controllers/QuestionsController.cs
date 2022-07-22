using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService questionService;
        public QuestionsController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = questionService.GetAll();
            return Ok(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(Question question)
        {
            questionService.Add(question);
            return Ok();
        }
    }
}
