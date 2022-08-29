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
            if (applicationDal.GetAll(x => x.CandidateId == application.CandidateId && x.JobAdvertId == application.JobAdvertId).FirstOrDefault() != null)
            {
                return new ResultItem(false, null, "Application already exist!");
            }
            application.CreatedAt = DateTime.Now;
            application.ApplicationDate = DateTime.Now;
            applicationDal.Add(application);
            return new ResultItem(true, application, Messages.ApplicationCreated);
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

        public ResultItem GetByUserIdAndCandidateId(int candidateId, int jobAdvertId)
        {
            var result = applicationDal.Get(x => x.JobAdvertId == jobAdvertId && x.CandidateId == candidateId);
            return new ResultItem(true, result, null);
        }
        public ResultItem GetApplicationPaginated(PaginationItem<ApplicationDto> pi)
        {
            try
            {
                var rows = applicationDal.GetApplicationDetails().AsQueryable();

                // Grid kolonlarından veya global searchden arama geldiyse
                if (pi.Filters != null && pi.Filters.Count() > 0)
                {
                    List<ApplicationDto> globalList = new List<ApplicationDto>();
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
                                var props = typeof(ApplicationDto).GetProperties();

                                foreach (var prop in props)
                                {
                                    var res = new List<ApplicationDto>();

                                    switch (prop.Name)
                                    {
                                        case nameof(ApplicationDto.PositionName):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.PositionName.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(ApplicationDto.CandidateFullName):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.CandidateFullName.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(ApplicationDto.WorkTimeType):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.WorkTimeType.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(ApplicationDto.WorkPlaceType):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.WorkPlaceType.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    globalList.AddRange(res.ToList());

                                }
                                rows = globalList.GroupBy(x => x.Id).Select(x => x.First()).AsQueryable();
                                globalList = new List<ApplicationDto>();
                            }
                            else // grid column search - spesifik kolon bazlı işlemler için
                            {
                                switch (key)
                                {
                                    case nameof(ApplicationDto.PositionName):
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
                                    case nameof(ApplicationDto.CandidateFullName):
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
                                    case nameof(ApplicationDto.Deadline):
                                        {
                                            var a = Convert.ToDateTime(val);
                                            rows = rows.Where(x => x.Deadline <= a);
                                            break;
                                        }
                                    case nameof(ApplicationDto.WorkTimeType):
                                        {
                                            switch (Convert.ToInt32(oVal))
                                            {
                                                case 0: // getAll
                                                    {
                                                        break;
                                                    }
                                                case 1: // partTime
                                                    {
                                                        rows = rows.Where(x => x.WorkTimeType == "PartTime");
                                                        break;
                                                    }
                                                case 2: // fullTime
                                                    {
                                                        rows = rows.Where(x => x.WorkTimeType == "FullTime");
                                                        break;
                                                    }
                                                case 3: // Intern
                                                    {
                                                        rows = rows.Where(x => x.WorkTimeType == "Intern");
                                                        break;
                                                    }

                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case nameof(ApplicationDto.WorkPlaceType):
                                        {
                                            switch (Convert.ToInt32(oVal))
                                            {
                                                case 0: // getAll
                                                    {
                                                        break;
                                                    }
                                                case 1: // Remote
                                                    {
                                                        rows = rows.Where(x => x.WorkPlaceType == "Remote");
                                                        break;
                                                    }
                                                case 2: // Hybrid
                                                    {
                                                        rows = rows.Where(x => x.WorkPlaceType == "Hybrid");
                                                        break;
                                                    }
                                                case 3: // FromOffice
                                                    {
                                                        rows = rows.Where(x => x.WorkPlaceType == "FromOffice");
                                                        break;
                                                    }

                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case nameof(ApplicationDto.IsDeleted):
                                        {
                                            switch (Convert.ToInt32(oVal))
                                            {
                                                case 0: // getAll
                                                    {
                                                        break;
                                                    }
                                                case 1: // IsNotDeleted
                                                    {
                                                        rows = rows.Where(x => x.IsDeleted == 0);
                                                        break;
                                                    }
                                                case 2: // IsDeleted
                                                    {
                                                        rows = rows.Where(x => x.IsDeleted == 1);
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
                        rows = rows.ToApplyOrder<ApplicationDto>(key, orderBy);
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

        public ResultItem GetApplicationPaginatedByJobAdvertId(PaginationItem<ApplicationDto> pi,int id)
        {
            try
            {
                var rows = applicationDal.GetApplicationDetails(x=>x.JobAdvertId == id).AsQueryable();

                // Grid kolonlarından veya global searchden arama geldiyse
                if (pi.Filters != null && pi.Filters.Count() > 0)
                {
                    List<ApplicationDto> globalList = new List<ApplicationDto>();
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
                                var props = typeof(ApplicationDto).GetProperties();

                                foreach (var prop in props)
                                {
                                    var res = new List<ApplicationDto>();

                                    switch (prop.Name)
                                    {
                                        case nameof(ApplicationDto.PositionName):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.PositionName.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(ApplicationDto.CandidateFullName):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.CandidateFullName.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(ApplicationDto.WorkTimeType):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.WorkTimeType.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(ApplicationDto.WorkPlaceType):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.WorkPlaceType.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    globalList.AddRange(res.ToList());

                                }
                                rows = globalList.GroupBy(x => x.Id).Select(x => x.First()).AsQueryable();
                                globalList = new List<ApplicationDto>();
                            }
                            else // grid column search - spesifik kolon bazlı işlemler için
                            {
                                switch (key)
                                {
                                    case nameof(ApplicationDto.PositionName):
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
                                    case nameof(ApplicationDto.CandidateFullName):
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
                                    case nameof(ApplicationDto.Deadline):
                                        {
                                            var a = Convert.ToDateTime(val);
                                            rows = rows.Where(x => x.Deadline <= a);
                                            break;
                                        }
                                    case nameof(ApplicationDto.WorkTimeType):
                                        {
                                            switch (Convert.ToInt32(oVal))
                                            {
                                                case 0: // getAll
                                                    {
                                                        break;
                                                    }
                                                case 1: // partTime
                                                    {
                                                        rows = rows.Where(x => x.WorkTimeType == "PartTime");
                                                        break;
                                                    }
                                                case 2: // fullTime
                                                    {
                                                        rows = rows.Where(x => x.WorkTimeType == "FullTime");
                                                        break;
                                                    }
                                                case 3: // Intern
                                                    {
                                                        rows = rows.Where(x => x.WorkTimeType == "Intern");
                                                        break;
                                                    }

                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case nameof(ApplicationDto.WorkPlaceType):
                                        {
                                            switch (Convert.ToInt32(oVal))
                                            {
                                                case 0: // getAll
                                                    {
                                                        break;
                                                    }
                                                case 1: // Remote
                                                    {
                                                        rows = rows.Where(x => x.WorkPlaceType == "Remote");
                                                        break;
                                                    }
                                                case 2: // Hybrid
                                                    {
                                                        rows = rows.Where(x => x.WorkPlaceType == "Hybrid");
                                                        break;
                                                    }
                                                case 3: // FromOffice
                                                    {
                                                        rows = rows.Where(x => x.WorkPlaceType == "FromOffice");
                                                        break;
                                                    }

                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case nameof(ApplicationDto.IsDeleted):
                                        {
                                            switch (Convert.ToInt32(oVal))
                                            {
                                                case 0: // getAll
                                                    {
                                                        break;
                                                    }
                                                case 1: // IsNotDeleted
                                                    {
                                                        rows = rows.Where(x => x.IsDeleted == 0);
                                                        break;
                                                    }
                                                case 2: // IsDeleted
                                                    {
                                                        rows = rows.Where(x => x.IsDeleted == 1);
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
                        rows = rows.ToApplyOrder<ApplicationDto>(key, orderBy);
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
