using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolver
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AnswerService>().As<IAnswerService>().SingleInstance();
            builder.RegisterType<EfAnswerDal>().As<IAnswerDal>().SingleInstance();

            builder.RegisterType<ApplicationService>().As<IApplicationService>().SingleInstance();
            builder.RegisterType<EfApplicationDal>().As<IApplicationDal>().SingleInstance();

            builder.RegisterType<QuestionService>().As<IQuestionService>().SingleInstance();
            builder.RegisterType<EfQuestionDal>().As<IQuestionDal>().SingleInstance();

            builder.RegisterType<CandidateService>().As<ICandidateService>().SingleInstance();
            builder.RegisterType<EfCandidateDal>().As<ICandidateDal>().SingleInstance();

            builder.RegisterType<OperationClaimService>().As<IOperationClaimService>().SingleInstance();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>().SingleInstance();

            builder.RegisterType<EfCandidateAnswerDal>().As<ICandidateAnswerDal>().SingleInstance();
            builder.RegisterType<CandidateAnswerService>().As<ICandidateAnswerService>().SingleInstance();

            builder.RegisterType<EfJobAdvertDal>().As<IJobAdvertDal>().SingleInstance();
            builder.RegisterType<JobAdvertService>().As<IJobAdvertService>().SingleInstance();

            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<AuthService>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

        }
    }
}
