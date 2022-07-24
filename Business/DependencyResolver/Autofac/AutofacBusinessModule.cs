using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolver
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QuestionService>().As<IQuestionService>().SingleInstance();
            builder.RegisterType<EfQuestionDal>().As<IQuestionDal>().SingleInstance();

            builder.RegisterType<CandidateService>().As<ICandidateService>().SingleInstance();
            builder.RegisterType<EfCandidateDal>().As<ICandidateDal>().SingleInstance();

            builder.RegisterType<OperationClaimService>().As<IOperationClaimService>().SingleInstance();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>().SingleInstance();

            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
        }
    }
}
