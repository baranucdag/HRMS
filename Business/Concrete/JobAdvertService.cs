using Business.Abstract;
using Business.Constans;
using Core.Extensions;
using Core.Utilities.Helpers.PaginationHelper;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
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
            return new ResultItem(true,null,Messages.AddSuccess);
        }

        public ResultItem Delete(int id)
        {
            var deletedEntity = jobAdvertDal.Get(x => x.Id == id);
            jobAdvertDal.SoftDelete(deletedEntity);
            return new ResultItem(true,null,Messages.DeleteSuccess);
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
            return new ResultItem(true);
        }

        public ResultItem GetById(int id)
        {
            var result = jobAdvertDal.Get(x => x.Id == id);
            return new ResultItem(true,result,null);
        }

        public ResultItem GetAll()
        {
            var result = jobAdvertDal.GetAll();
            return new ResultItem(true, result, Messages.DataListed);
        }

        public ResultItem GetPaginationData(PaginationItem<JobAdvert> pi)
        {

            try
            {
                var rows = jobAdvertDal.GetAll().AsQueryable();

                // Grid kolonlarından veya global searchden arama geldiyse
                if (pi.Filters != null && pi.Filters.Count() > 0)
                {
                    List<JobAdvert> globalList = new List<JobAdvert>();
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
                                var props = typeof(JobAdvert).GetProperties();

                                foreach (var prop in props)
                                {
                                    var res = new List<JobAdvert>();

                                    switch (prop.Name)
                                    {
                                        case nameof(JobAdvert.PositionName):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.PositionName.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(JobAdvert.QualificationLevel):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.QualificationLevel.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(JobAdvert.WorkType):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.WorkType.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(JobAdvert.Description):
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
                                globalList = new List<JobAdvert>();
                            }
                            else // grid column search - spesifik kolon bazlı işlemler için
                            {
                                switch (key)
                                {
                                    case nameof(JobAdvert.PositionName):
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
                                    case nameof(JobAdvert.QualificationLevel):
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
                                    case nameof(JobAdvert.WorkType):
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
                                    case nameof(JobAdvert.Description):
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
                                    case nameof(JobAdvert.IsDeleted):
                                        {
                                            switch (Convert.ToInt32(oVal))
                                            {
                                                case 0: // getAll
                                                    {
                                                        break;
                                                    }
                                                case 1: // IsNotDeleted
                                                    {
                                                        rows = rows.Where(x => x.IsDeleted == false);
                                                        break;
                                                    }
                                                case 2: // IsDeleted
                                                    {
                                                        rows = rows.Where(x => x.IsDeleted == true);
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
                        rows = rows.ToApplyOrder<JobAdvert>(key, orderBy);
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
