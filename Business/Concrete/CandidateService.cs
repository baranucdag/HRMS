using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateDal candidateDal;

        public CandidateService(ICandidateDal candidateDal)
        {
            this.candidateDal = candidateDal;
        }
        public void Add(Candidate candidate)
        {
            candidateDal.Add(candidate);
        }

        public void Delete(int id)
        {
            var deletedEntity = candidateDal.Get(x => x.Id == id);
            candidateDal.SoftDelete(deletedEntity);
        }

        public List<Candidate> GetAll()
        {
            return candidateDal.GetAll();
        }

        public Candidate GetById(int id)
        {
            return candidateDal.Get(x => x.Id == id);
        }

        public void Update(Candidate candidate)
        {
            candidateDal.Update(candidate);
        }
    }
}
