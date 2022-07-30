using Core.Utilities.Helpers.PaginationHelper;
using Core.Utilities.Result;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IJobAdvertService
    {
        ResultItem Add(JobAdvert jobAdvert);
        ResultItem Delete(int id);
        ResultItem Update(JobAdvert jobAdvert);
        ResultItem GetPaginationData(PaginationItem<JobAdvert> pi);
        ResultItem GetById(int id);
    }
}
