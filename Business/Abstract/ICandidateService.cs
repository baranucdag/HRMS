using Business.Concrete;
using Core.Utilities.Result;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICandidateService
    {
        void Add(Candidate candidate);
        void Update(Candidate candidate);
        void Delete(int id);
        ResultItem GetAll(QueryParams queryParams);
        Candidate GetById(int id);
    }
}
