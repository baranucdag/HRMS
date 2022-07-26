using TechBuddy.Middlewares.ExceptionHandling;
using TechBuddy.Middlewares.ExceptionHandling.Infrastructure;

namespace Core.Extensions
{
    public class CustomResponseModelCreatorExtension : IResponseModelCreator
    {
        public object CreateModel(ModelCreatorContext model)
        {
            return new
            {
                ExMes = model.ErrorMessage,
                DetailExMes = model.Exception.Message.ToString()

            };
        }
    }
}
