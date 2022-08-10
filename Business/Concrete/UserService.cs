using Business.Abstract;
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

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal userDal;
        public UserService(IUserDal userDal)
        {
            this.userDal = userDal;
        }
        public void Add(User user)
        {
            userDal.Add(user);
        }

        public void Delete(int id)
        {
            var deletedEntity = userDal.Get(x => x.Id == id);
            userDal.HardDelete(deletedEntity);
        }

        public List<User> GetAll()
        {
            return userDal.GetAll();
        }

        //get claims of user
        public List<OperationClaim> GetClaims(User user)
        {
            return userDal.GetClaims(user);
        }
        //get single claim of user (firstOrDefault)
        public ResultItem GetClaim(User user)
        {
            throw new NotImplementedException();
        }

        public ResultItem GetById(int id)
        {
            var result = userDal.Get(x => x.Id == id);
            return new ResultItem(true, result);
        }

        public User GetByMail(string email)
        {
            return userDal.Get(x => x.Email == email);
        }

        public void Update(User user)
        {
            userDal.Update(user);
        }

        //Get users filtered, sorted and paged
        public ResultItem GetUsersPaginated(PaginationItem<UserDto> pi)
        {
            //SecuredOperationTool securedOperation = new SecuredOperationTool("admin"); 
            try
            {
                var rows = userDal.GetUserDetails().AsQueryable();

                // Grid kolonlarından veya global searchden arama geldiyse
                if (pi.Filters != null && pi.Filters.Count() > 0)
                {
                    List<UserDto> globalList = new List<UserDto>();
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
                                var props = typeof(UserDto).GetProperties();

                                foreach (var prop in props)
                                {
                                    var res = new List<UserDto>();

                                    switch (prop.Name)
                                    {
                                        case nameof(UserDto.Email):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.Email.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(UserDto.FullName):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.FullName.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    globalList.AddRange(res.ToList());

                                }
                                rows = globalList.GroupBy(x => x.Id).Select(x => x.First()).AsQueryable();
                                globalList = new List<UserDto>();
                            }
                            else // grid column search - spesifik kolon bazlı işlemler için
                            {
                                switch (key)
                                {
                                    case nameof(UserDto.Email):
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
                                    case nameof(UserDto.FullName):
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
                        rows = rows.ToApplyOrder<UserDto>(key, orderBy);
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

