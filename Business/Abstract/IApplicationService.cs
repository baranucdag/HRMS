using Core.Utilities.Helpers.PaginationHelper;
using Core.Utilities.Result;
using Entities.Concrete;
using Entities.Dto;

namespace Business.Abstract
{
    public interface IApplicationService
    {
        ResultItem GetApplicationDetails();
        ResultItem GetApplicationPaginated(PaginationItem<ApplicationDto> pi);
        ResultItem GetByUserIdAndCandidateId(int candidateId, int jobAdvertId);
        ResultItem GetByJobAdvertId(int id);
        ResultItem GetByCandidateId(int id);
        ResultItem Add(Application application);
        ResultItem Update(Application application);
        ResultItem Delete(int id);
        ResultItem UnDelete(int id);
    }
}
