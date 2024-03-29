﻿using Business.Abstract;
using Core.Entites.Concrete;
using DataAccess.Abstract;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class OperationClaimService : IOperationClaimService
    {
        private readonly IOperationClaimDal operationClaimDal;

        public OperationClaimService(IOperationClaimDal operationClaimDal)
        {
            this.operationClaimDal = operationClaimDal;
        }
        public void Add(OperationClaim operationClaim)
        {
            operationClaimDal.Add(operationClaim);
        }

        public void Delete(int id)
        {
            var deletedEntity = operationClaimDal.Get(x=>x.Id == id);
            operationClaimDal.SoftDelete(deletedEntity);
        }

        public OperationClaim Get(int id)
        {
            return operationClaimDal.Get(x=>x.Id == id);
        }

        public List<OperationClaim> GetAll()
        {
           return operationClaimDal.GetAll();
        }
    }
}
