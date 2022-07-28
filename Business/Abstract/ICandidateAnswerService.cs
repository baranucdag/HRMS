using Core.Utilities.Result;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICandidateAnswerService
    {
        ResultItem GetCandidateAnswerDetails();
        ResultItem GetCandidateAnswerDetailsById(int id);
        ResultItem Add(CandidateAnswer candidateAnswer);
        ResultItem Delete(int id);
        ResultItem Update(CandidateAnswer candidateAnswer);

    }
}
