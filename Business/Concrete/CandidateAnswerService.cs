using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CandidateAnswerService : ICandidateAnswerService
    {
        private readonly ICandidateAnswerDal candidateAnswerDal;

        public CandidateAnswerService(ICandidateAnswerDal candidateAnswerDal)
        {
            this.candidateAnswerDal = candidateAnswerDal;
        }

        public ResultItem Add(CandidateAnswer candidateAnswer)
        {
            candidateAnswerDal.Add(candidateAnswer);
            return new ResultItem(true);
        }

        public ResultItem Delete(int id)
        {
            var deletedEntity = candidateAnswerDal.Get(x=>x.Id == id);
            candidateAnswerDal.SoftDelete(deletedEntity);
            return new ResultItem(true);
        }

        public ResultItem GetCandidateAnswerDetails()
        {
            return new ResultItem(true, candidateAnswerDal.GetCandidateAnswerDetail());
        }

        public ResultItem GetCandidateAnswerDetailsById(int id)
        {
            return new ResultItem(true,candidateAnswerDal.GetCandidateAnswerDetail(x=>x.CandidateId == id));
        }

        public ResultItem Update(CandidateAnswer candidateAnswer)
        {
            candidateAnswerDal.Update(candidateAnswer);
            return new ResultItem(true);
        }

    }
}
