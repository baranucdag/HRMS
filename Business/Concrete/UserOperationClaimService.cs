using Business.Abstract;
using Business.Constans;
using Core.Entites.Concrete;
using Core.Extensions;
using Core.Utilities.Helpers.PaginationHelper;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Dto;
using Newtonsoft.Json.Linq;
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
            if (userOperationClam != null)
            {
                var operationClaim = userOperationClaimDal.Get(x => x.UserId == userOperationClam.UserId);
                if (operationClaim != null)
                {
                    userOperationClaimDal.HardDelete(operationClaim);
                }
                userOperationClaimDal.Add(userOperationClam);
                return new ResultItem(true, null, Messages.AddSuccess);
            }
            return new ResultItem(false, null, Messages.AddFail);
        }

        public ResultItem Delete(UserOperationClaim userOperationClam)
        {
            userOperationClaimDal.HardDelete(userOperationClam);
            return new ResultItem(true, null, Messages.DeleteSuccess);
        }


        //get all data (not dto just data)
        public ResultItem GetAll()
        {
            var result = userOperationClaimDal.GetAll();
            return new ResultItem(true, result, Messages.DataListed);
        }


        // get data by id
        public ResultItem GetById(int id)
        {
            var result = userOperationClaimDal.GetAll(x => x.Id == id);
            return new ResultItem(true, result, Messages.DataListed);
        }


        //get data by userId
        public ResultItem GetByUserId(int userId)
        {
            var result = userOperationClaimDal.GetOperationClaimDetail(x => x.UserId == userId);
            return new ResultItem(true, result);
        }

        public ResultItem Update(UserOperationClaim userOperationClaim)
        {
            userOperationClaimDal.Update(userOperationClaim);
            return new ResultItem(true, null, Messages.UpdateSuccess);
        }


        //Get user operation claims filtered, sorted and paged
        public ResultItem GetclaimsPaginated(PaginationItem<UserOperationClaimDto> pi)
        {
            //SecuredOperationTool securedOperation = new SecuredOperationTool("admin"); 
            try
            {
                var rows = userOperationClaimDal.GetDetails().AsQueryable();

                // Grid kolonlarından veya global searchden arama geldiyse
                if (pi.Filters != null && pi.Filters.Count() > 0)
                {
                    List<UserOperationClaimDto> globalList = new List<UserOperationClaimDto>();
                    foreach (var item in pi.Filters)
                    {
                        string key = item.Key.ToFirstCharUpper(true);
                        dynamic obj = JObject.Parse(Convert.ToString(item.Value));
                        string val = obj.value;
                        string oVal = val.ToLowerEng();
                        string matchMode = obj.matchMode;
                        matchMode = matchMode.ToFirstCharUpper(true);

                        // val null değilse
                        if (!val.IsNullOrEmpty())
                        {
                            // grid global search
                            if (item.Key.ToLowerEng() == "global")
                            {
                                var props = typeof(UserOperationClaimDto).GetProperties();

                                foreach (var prop in props)
                                {
                                    var res = new List<UserOperationClaimDto>();

                                    switch (prop.Name)
                                    {
                                        case nameof(UserOperationClaimDto.ClaimName):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.ClaimName.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(UserOperationClaimDto.FullName):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.FullName.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(UserOperationClaimDto.Email):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.Email.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    globalList.AddRange(res.ToList());

                                }
                                rows = globalList.GroupBy(x => x.Id).Select(x => x.First()).AsQueryable();
                                globalList = new List<UserOperationClaimDto>();
                            }
                            else // grid column search - spesifik kolon bazlı işlemler için
                            {
                                switch (key)
                                {
                                    case nameof(UserOperationClaimDto.Email):
                                        {
                                            switch (matchMode.ToLowerEng())
                                            {
                                                case "startswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Email.ToLowerEng().StartsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "endswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Email.ToLowerEng().EndsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "contains":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Email.ToLowerEng().Contains(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "equals":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Email.ToLowerEng().Equals(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case nameof(UserOperationClaimDto.FullName):
                                        {
                                            switch (matchMode.ToLowerEng())
                                            {
                                                case "startswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.FullName.ToLowerEng().StartsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "endswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.FullName.ToLowerEng().EndsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "contains":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.FullName.ToLowerEng().Contains(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "equals":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.FullName.ToLowerEng().Equals(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case nameof(UserOperationClaimDto.ClaimName):
                                        {
                                            switch (matchMode.ToLowerEng())
                                            {
                                                case "startswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.ClaimName.ToLowerEng().StartsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "endswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.ClaimName.ToLowerEng().EndsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "contains":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.ClaimName.ToLowerEng().Contains(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "equals":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.ClaimName.ToLowerEng().Equals(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    //case nameof(CandidateDto.IsDeleted):
                                    //    {
                                    //        switch (Convert.ToInt32(oVal))
                                    //        {
                                    //            case 0: // getAll
                                    //                {
                                    //                    break;
                                    //                }
                                    //            case 1: // IsNotDeleted
                                    //                {
                                    //                    rows = rows.Where(x => x.IsDeleted == 0);
                                    //                    break;
                                    //                }
                                    //            case 2: // IsDeleted
                                    //                {
                                    //                    rows = rows.Where(x => x.IsDeleted == 1);
                                    //                    break;
                                    //                }
                                    //            default:
                                    //                break;
                                    //        }
                                    //        break;
                                    //    }
                                    default:
                                        {
                                            break;
                                        }
                                }

                            }
                        }
                    }
                }

                // Column sorting varsa
                if (pi.MultiSortMeta.Count > 0)
                {
                    for (int i = 0; i < pi.MultiSortMeta.Count; i++)
                    {
                        var item = pi.MultiSortMeta[i];
                        string key = item.Field.ToFirstCharUpper(true);

                        string orderBy = item.Order == 1 ? "ThenBy" : "ThenByDescending";
                        if (i == 0)
                        {
                            orderBy = item.Order == 1 ? "OrderBy" : "OrderByDescending";
                        }
                        rows = rows.ToApplyOrder<UserOperationClaimDto>(key, orderBy);
                    }
                }

                // Toplam kayıt sayısı 
                pi.TotalRowCount = rows.Count();

                // Pagination uygula
                pi.Rows = rows.ToPagination(pi.PageSize, pi.CurPage);

                return new ResultItem(true, pi);
            }
            catch (Exception ex)
            {
                return new ResultItem(false, null, ex.Message + ex.StackTrace);
            }
        }
    }
}
