using Business.Abstract;
using Business.Constans;
using Core.Entites.Concrete;
using Core.Utilities.Result;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserOperationClaimService : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal userOperationClaimDal;

        public UserOperationClaimService(IUserOperationClaimDal userOperationClaimDal)
        {
            this.userOperationClaimDal = userOperationClaimDal;
        }
        public ResultItem Add(UserOperationClaim userOperationClam)
        {
            userOperationClaimDal.Add(userOperationClam);
            return new ResultItem(true, null, Messages.AddSuccess);
        }

        public ResultItem Delete(UserOperationClaim userOperationClam)
        {
            userOperationClaimDal.HardDelete(userOperationClam);
            return new ResultItem(true, null, Messages.DeleteSuccess);
        }

        public ResultItem GetAll()
        {
            var result = userOperationClaimDal.GetAll();
            return new ResultItem(true, result, Messages.DataListed);
        }

        public ResultItem GetById(int id)
        {
            var result = userOperationClaimDal.GetAll(x=>x.Id == id);
            return new ResultItem(true, result, Messages.DataListed);
        }

        public ResultItem GetByUserId(int userId)
        {
            var result = userOperationClaimDal.GetAll(x => x.UserId == userId);
            return new ResultItem(true, result, Messages.DataListed);
        }

        public ResultItem Update(UserOperationClaim userOperationClaim)
        {
            userOperationClaimDal.Update(userOperationClaim);
            return new ResultItem(true,null,Messages.UpdateSuccess);
        }
    }
}
