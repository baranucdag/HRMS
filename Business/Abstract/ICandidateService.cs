using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICandidateService
    {
        void Add(Candidate candidate);
        void Update(Candidate candidate);
        void Delete(Candidate candidate);
        List<Candidate> GetAll();
        Candidate GetById(int id);
    }
}
