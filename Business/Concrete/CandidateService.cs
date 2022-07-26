using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

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

        public ResultItem GetAll(QueryParams queryParams)
        {
            List<Candidate> result;
            if (queryParams.OrderType == false)
            {
                if (queryParams.GlobalFilter != null)
                {
                    result = candidateDal.GetAll().OrderByDescending(x => x.Id).ToList();
                }
                result = candidateDal.GetAll(x => x.EMail.ToLower().Contains(queryParams.GlobalFilter.ToLower())
                   || x.FirstName.ToLower().Contains(queryParams.GlobalFilter.ToLower())
                   || x.LastName.ToLower().Contains(queryParams.GlobalFilter.ToLower())).OrderByDescending(x => x.Id).ToList();
            }
            else
            {
                if (queryParams.GlobalFilter == null)
                {
                    result = candidateDal.GetAll();
                }
                result = candidateDal.GetAll(x => x.EMail.ToLower().Contains(queryParams.GlobalFilter.ToLower()) 
                || x.FirstName.ToLower().Contains(queryParams.GlobalFilter.ToLower()) 
                || x.LastName.ToLower().Contains(queryParams.GlobalFilter.ToLower()));
            }

            return new ResultItem(true, result);
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

    public class QueryParams
    {
        public bool OrderType { get; set; }
        public string GlobalFilter { get; set; }
    }
}

