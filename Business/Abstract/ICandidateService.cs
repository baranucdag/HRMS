using Core.Utilities.Helpers.PaginationHelper;
using Core.Utilities.Result;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICandidateService
    {
        ResultItem Add(Candidate candidate);
        ResultItem Update(Candidate candidate);
        ResultItem Delete(int id);
        ResultItem GetById(int id);
        ResultItem GetCandidatesPaginated(PaginationItem<Candidate> pi);
    }
}
