using Core.Utilities.Helpers.PaginationHelper;
using Core.Utilities.Result;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface ICandidateService
    {
        ResultItem UpdateWithFile(IFormFile file, Candidate candidate);
        ResultItem Add( Candidate candidate);
        ResultItem Update(Candidate candidate);
        ResultItem Delete(int id);
        ResultItem UnDelete(int id);
        ResultItem GetById(int id);
        ResultItem GetByUserId(int id);
        ResultItem GetCandidatesPaginated(PaginationItem<CandidateDto> pi);
    }
}
