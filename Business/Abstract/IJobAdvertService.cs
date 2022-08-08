using Core.Utilities.Helpers.PaginationHelper;
using Core.Utilities.Result;
using Entities.Concrete;
using Entities.Dto;

namespace Business.Abstract
{
    public interface IJobAdvertService
    {
        ResultItem Add(JobAdvert jobAdvert);
        ResultItem Delete(int id);
        ResultItem UnDelete(int id);
        ResultItem Update(JobAdvert jobAdvert);
        ResultItem GetPaginationData(PaginationItem<JobAdvertDto> pi);
        ResultItem GetById(int id);
        ResultItem GetAll();
        ResultItem GetAllDetails();

    }
}
