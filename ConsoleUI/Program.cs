using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            QuestionService questionService = new QuestionService(new EfQuestionDal());
            var result = questionService.GetAll();

            foreach (var item in result)
            {
                Console.WriteLine(item.Text);
            }
        }
    }
}
