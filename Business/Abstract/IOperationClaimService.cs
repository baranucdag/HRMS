using Core.Entites.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IOperationClaimService
    {
        void Add(OperationClaim operationClaim);
        void Delete(OperationClaim operationClaim);
        List<OperationClaim> GetAll();
        OperationClaim Get(int id);
    }
}
