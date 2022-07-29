using Core.Utilities.Helpers.PaginationHelper;
using Core.Utilities.Result;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IAnswerService
    {
        ResultItem Add(Answer answer);
        ResultItem Update(Answer answer);
        ResultItem Delete(int id);
        ResultItem GetAnswerDetails();
        ResultItem GetById(int id);
        ResultItem GetByQuestionId(int questionId);
        ResultItem GetPaginationData(PaginationItem<Answer> pi);
    }
}
