using Core.Entites.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IOperationClaimService
    {
        void Add(OperationClaim operationClaim);
        void Delete(int id);
        List<OperationClaim> GetAll();
        OperationClaim Get(int id);
    }
}
