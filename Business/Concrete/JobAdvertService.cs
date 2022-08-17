using Business.Abstract;
using Business.Constans;
using Core.Extensions;
using Core.Utilities.Helpers.PaginationHelper;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class JobAdvertService : IJobAdvertService
    {
        private readonly IJobAdvertDal jobAdvertDal;

        public JobAdvertService(IJobAdvertDal jobAdvertDal)
        {
            this.jobAdvertDal = jobAdvertDal;
        }
        public ResultItem Add(JobAdvert jobAdvert)
        {
            jobAdvertDal.Add(jobAdvert);
            return new ResultItem(true, null, Messages.AddSuccess);
        }

        //Get all data without filtering and sorting
        public ResultItem GetAllDetails()
        {
            var result = jobAdvertDal.GetJobAdvertDtos();
            return new ResultItem(true, result, Messages.DataListed);
        }
        public ResultItem Delete(int id)
        {
            var deletedEntity = jobAdvertDal.Get(x => x.Id == id);
            jobAdvertDal.SoftDelete(deletedEntity);
            return new ResultItem(true, null, Messages.DeleteSuccess);
        }
        public ResultItem UnDelete(int id)
        {
            var deletedEntity = jobAdvertDal.Get(x => x.Id == id);
            jobAdvertDal.UnDelete(deletedEntity);
            return new ResultItem(true, null, Messages.UnDeleteSuccess);
        }
        public ResultItem Update(JobAdvert jobAdvert)
        {
            jobAdvertDal.Update(jobAdvert);
            return new ResultItem(true,null,Messages.UpdateSuccess);
        }


        //Get single data by id
        public ResultItem GetById(int id)
        {
            var result = jobAdvertDal.Get(x => x.Id == id);
            return new ResultItem(true, result, null);
        }

        public ResultItem GetAll()
        {
            var result = jobAdvertDal.GetJobAdvertDtos().Where(x=>x.IsDeleted == 0);
            return new ResultItem(true, result, Messages.DataListed);
        }


        //Get Data paginated, sorted and filtered
        public ResultItem GetPaginationData(PaginationItem<JobAdvertDto> pi)
        {

            try
            {
                var rows = jobAdvertDal.GetJobAdvertDtos().AsQueryable();

                // Grid kolonlarından veya global searchden arama geldiyse
                if (pi.Filters != null && pi.Filters.Count() > 0)
                {
                    List<JobAdvertDto> globalList = new List<JobAdvertDto>();
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
                                var props = typeof(JobAdvertDto).GetProperties();

                                foreach (var prop in props)
                                {
                                    var res = new List<JobAdvertDto>();

                                    switch (prop.Name)
                                    {
                                        case nameof(JobAdvertDto.PositionName):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.PositionName.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(JobAdvertDto.QualificationLevel):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.QualificationLevel.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(JobAdvertDto.Description):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.Description.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }

                                        default:
                                            break;
                                    }
                                    globalList.AddRange(res.ToList());

                                }
                                rows = globalList.GroupBy(x => x.Id).Select(x => x.First()).AsQueryable();
                                globalList = new List<JobAdvertDto>();
                            }
                            else // grid column search - spesifik kolon bazlı işlemler için
                            {
                                switch (key)
                                {
                                    case nameof(JobAdvertDto.PositionName):
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
                                    case nameof(JobAdvertDto.QualificationLevel):
                                        {
                                            switch (matchMode.ToLowerEng())
                                            {
                                                case "startswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.QualificationLevel.ToLowerEng().StartsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "endswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.QualificationLevel.ToLowerEng().EndsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "contains":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.QualificationLevel.ToLowerEng().Contains(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "equals":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.QualificationLevel.ToLowerEng().Equals(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case nameof(JobAdvertDto.Description):
                                        {
                                            switch (matchMode.ToLowerEng())
                                            {
                                                case "startswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Description.ToLowerEng().StartsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "endswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Description.ToLowerEng().EndsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "contains":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Description.ToLowerEng().Contains(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "equals":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Description.ToLowerEng().Equals(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case nameof(JobAdvertDto.Deadline):
                                        {
                                            var a = Convert.ToDateTime(val);
                                            rows = rows.Where(x => x.Deadline <= a);
                                            break;
                                        }
                                    case nameof(JobAdvertDto.PublishDate):
                                        {
                                            var a = Convert.ToDateTime(val);
                                            rows = rows.Where(x => x.PublishDate <= a);
                                            break;
                                        }
                                    case nameof(JobAdvertDto.WorkTimeType):
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
                                    case nameof(JobAdvertDto.WorkPlaceType):
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
                                    case nameof(JobAdvertDto.IsDeleted):
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
                        rows = rows.ToApplyOrder<JobAdvertDto>(key, orderBy);
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
