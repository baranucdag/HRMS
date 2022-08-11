using Core.DataAccess;
using Entities.Concrete;
using Entities.Dto;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IAnswerDal : IEntityRepository<Answer>
    {
        public List<AnswerDto> GetAnswerDetails();
    }
}
