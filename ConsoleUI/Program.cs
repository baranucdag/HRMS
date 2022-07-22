using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            QuestionService questionService = new QuestionService(new EfQuestionDal());
            var result = questionService.GetAll();

            AnswerService answerService = new AnswerService(new EfAnswerDal());
            var result2 = answerService.GetAll();


            foreach (var item in result2)
            {
                Console.WriteLine(item.Text);
            }
        }
    }
}
