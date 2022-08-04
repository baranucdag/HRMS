using Business.Abstract;
using Business.Constans;
using Core.Extensions;
using Core.Utilities.Helpers.PaginationHelper;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationDal applicationDal;

        public ApplicationService(IApplicationDal applicationDal)
        {
            this.applicationDal = applicationDal;
        }
        public ResultItem GetApplicationDetails()
        {
            var result = applicationDal.GetApplicationDetails();
            return new ResultItem(true, result, Messages.DataListed);
        }
        public ResultItem Add(Application application)
        {
            applicationDal.Add(application);
            return new ResultItem(true);
        }

        public ResultItem Delete(int id)
        {
            var deletedEntity = applicationDal.Get(x => x.Id == id);
            applicationDal.SoftDelete(deletedEntity);
            return new ResultItem(true);
        }

        public ResultItem UnDelete(int id)
        {
            var deletedEntity = applicationDal.Get(x => x.Id == id);
            applicationDal.UnDelete(deletedEntity);
            return new ResultItem(true);
        }

        public ResultItem Update(Application application)
        {
            applicationDal.Update(application);
            return new ResultItem();
        }
        public ResultItem GetApplicationPaginated(PaginationItem<ApplicationDetailDto> pi)
        {

            try
            {
                var rows = applicationDal.GetApplicationDetails().AsQueryable();

                // Grid kolonlarından veya global searchden arama geldiyse
                if (pi.Filters != null && pi.Filters.Count() > 0)
                {
                    List<ApplicationDetailDto> globalList = new List<ApplicationDetailDto>();
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
                                var props = typeof(ApplicationDetailDto).GetProperties();

                                foreach (var prop in props)
                                {
                                    var res = new List<ApplicationDetailDto>();

                                    switch (prop.Name)
                                    {
                                        case nameof(ApplicationDetailDto.PositionName):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.PositionName.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(ApplicationDetailDto.CandidateFullName):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.CandidateFullName.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(ApplicationDetailDto.WorkType):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.WorkType.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    globalList.AddRange(res.ToList());

                                }
                                rows = globalList.GroupBy(x => x.Id).Select(x => x.First()).AsQueryable();
                                globalList = new List<ApplicationDetailDto>();
                            }
                            else // grid column search - spesifik kolon bazlı işlemler için
                            {
                                switch (key)
                                {
                                    case nameof(ApplicationDetailDto.PositionName):
                                        {
                                            switch (matchMode.ToLowerEng())
                                            {
                                                case "startswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.PositionName.ToLowerEng().StartsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "endswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.PositionName.ToLowerEng().EndsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "contains":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.PositionName.ToLowerEng().Contains(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "equals":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.PositionName.ToLowerEng().Equals(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case nameof(ApplicationDetailDto.CandidateFullName):
                                        {
                                            switch (matchMode.ToLowerEng())
                                            {
                                                case "startswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.CandidateFullName.ToLowerEng().StartsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "endswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.CandidateFullName.ToLowerEng().EndsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "contains":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.CandidateFullName.ToLowerEng().Contains(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "equals":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.CandidateFullName.ToLowerEng().Equals(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case nameof(ApplicationDetailDto.WorkType):
                                        {
                                            switch (matchMode.ToLowerEng())
                                            {
                                                case "startswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.WorkType.ToLowerEng().StartsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "endswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.WorkType.ToLowerEng().EndsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "contains":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.WorkType.ToLowerEng().Contains(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "equals":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.WorkType.ToLowerEng().Equals(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }
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
                        rows = rows.ToApplyOrder<ApplicationDetailDto>(key, orderBy);
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
