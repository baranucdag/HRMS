using Business.Abstract;
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
            return new ResultItem(true);
        }

        public ResultItem Delete(int id)
        {
            var deletedEntity = jobAdvertDal.Get(x => x.Id == id);
            jobAdvertDal.SoftDelete(deletedEntity);
            return new ResultItem(true);
        }
        public ResultItem Update(JobAdvert jobAdvert)
        {
            jobAdvertDal.Update(jobAdvert);
            return new ResultItem(true);
        }
        /*public ResultItem GetPaginationData(PaginationItem<JobAdvert> pi)
        {
            var rows = jobAdvertDal.GetAll().AsQueryable();

            rows.OrderByDescending(x => x.Id);

            //Grid kolonlarından veya global searchden arama geldiyse
            if (pi.Filters != null && pi.Filters.Count() > 0)
            {
                List<JobAdvert> globalList = new List<JobAdvert>();
                foreach (var item in pi.Filters)
                {
                    dynamic obj = JObject.Parse(Convert.ToString(item.Value));
                    string val = obj.value;
                    //string oVal = val.ToLowerEng();
                    string matchMode = obj.matchMode;
                    //matchMode = matchMode.ToFirstCharUpper(true);

                    if (!val.IsNullOrEmpty())
                    {
                        if (item.Key == "global")
                        {
                            var props = typeof(JobAdvert).GetProperties();
                            foreach (var prop in props)
                            {
                                var res = new List<JobAdvert>();

                                switch (prop.Name)
                                {
                                    case nameof(JobAdvert.PositionName):
                                        {
                                            res = rows.AsEnumerable().Where(x => x.PositionName.Contains(val)).ToList();
                                            break;
                                        }
                                    case nameof(JobAdvert.WorkType):
                                        {
                                            res = rows.AsEnumerable().Where(x => x.WorkType.Contains(val)).ToList();
                                            break;
                                        }
                                    case nameof(JobAdvert.QualificationLevel):
                                        {
                                            res = rows.AsEnumerable().Where(x => x.QualificationLevel.Contains(val)).ToList();
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

                    }
                }
            }
            return new ResultItem(true, rows, null, System.Net.HttpStatusCode.OK, "1");
        }

        //public 
    }*/

        public ResultItem GetPaginationData(PaginationItem<JobAdvert> pi)
        {
            var rows = jobAdvertDal.GetAll().AsQueryable();

            rows.OrderByDescending(x => x.Id);

            //Grid kolonlarından veya global searchden arama geldiyse
            if (pi.Filters != null && pi.Filters.Count() > 0)
            {

                List<JobAdvert> globalList = new List<JobAdvert>();
                foreach (var item in pi.Filters)
                {
                    var key = item.Key; 
                    dynamic obj = JObject.Parse(Convert.ToString(item.Value));
                    string val = obj.value;
                    string oVal = val.ToLower();
                    string matchMode = obj.matchMode;

                    if (!val.IsNullOrEmpty())
                    {
                        if (item.Key == "global")
                        {
                            var props = typeof(JobAdvert).GetProperties();
                            foreach (var prop in props)
                            {
                                var res = new List<JobAdvert>();

                                switch (prop.Name)
                                {
                                    case nameof(JobAdvert.PositionName):
                                        {
                                            res = rows.AsEnumerable().Where(x => x.PositionName.Contains(val)).ToList();
                                            break;
                                        }
                                    case nameof(JobAdvert.WorkType):
                                        {
                                            res = rows.AsEnumerable().Where(x => x.WorkType.Contains(val)).ToList();
                                            break;
                                        }
                                    case nameof(JobAdvert.QualificationLevel):
                                        {
                                            res = rows.AsEnumerable().Where(x => x.QualificationLevel.Contains(val)).ToList();
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
                                case "workType":
                                    {
                                        switch (matchMode.ToLower())
                                        {
                                            case "startswith":
                                                {
                                                    rows = rows.AsEnumerable().Where(x => x.WorkType.ToLower().StartsWith(oVal)).AsQueryable();
                                                    break;
                                                }
                                            case "endswith":
                                                {
                                                    rows = rows.AsEnumerable().Where(x => x.WorkType.ToLower().EndsWith(oVal)).AsQueryable();
                                                    break;
                                                }
                                            case "contains":
                                                {
                                                    rows = rows.AsEnumerable().Where(x => x.WorkType.ToLower().Contains(oVal)).AsQueryable();
                                                    break;
                                                }
                                            case "equals":
                                                {
                                                    rows = rows.AsEnumerable().Where(x => x.WorkType.ToLower().Equals(oVal)).AsQueryable();
                                                    break;
                                                }
                                            default:
                                                break;
                                        }
                                        break;
                                    }
                                case nameof(JobAdvert.PositionName):
                                    {
                                        switch (matchMode.ToLower())
                                        {
                                            case "startswith":
                                                {
                                                    rows = rows.AsEnumerable().Where(x => x.PositionName.ToLower().StartsWith(oVal)).AsQueryable();
                                                    break;
                                                }
                                            case "endswith":
                                                {
                                                    rows = rows.AsEnumerable().Where(x => x.PositionName.ToLower().EndsWith(oVal)).AsQueryable();
                                                    break;
                                                }
                                            case "contains":
                                                {
                                                    rows = rows.AsEnumerable().Where(x => x.PositionName.ToLower().Contains(oVal)).AsQueryable();
                                                    break;
                                                }
                                            case "equals":
                                                {
                                                    rows = rows.AsEnumerable().Where(x => x.PositionName.ToLower().Equals(oVal)).AsQueryable();
                                                    break;
                                                }
                                            default:
                                                break;
                                        }
                                        break;
                                    }

                                case nameof(JobAdvert.QualificationLevel):
                                    {
                                        switch (matchMode.ToLower())
                                        {
                                            case "startswith":
                                                {
                                                    rows = rows.AsEnumerable().Where(x => x.QualificationLevel.ToLower().StartsWith(oVal)).AsQueryable();
                                                    break;
                                                }
                                            case "endswith":
                                                {
                                                    rows = rows.AsEnumerable().Where(x => x.QualificationLevel.ToLower().EndsWith(oVal)).AsQueryable();
                                                    break;
                                                }
                                            case "contains":
                                                {
                                                    rows = rows.AsEnumerable().Where(x => x.QualificationLevel.ToLower().Contains(oVal)).AsQueryable();
                                                    break;
                                                }
                                            case "equals":
                                                {
                                                    rows = rows.AsEnumerable().Where(x => x.QualificationLevel.ToLower().Equals(oVal)).AsQueryable();
                                                    break;
                                                }
                                            default:
                                                break;
                                        }
                                        break;
                                    }

                                case "id":
                                    {
                                        rows = rows.Where(x => x.Id == Convert.ToInt32(oVal));
                                        break;
                                    }

                            }

                        }
                    }
                }
            }

            //for (int i = 0; i < pi.MultiSortMeta.Count; i++)
            //{
            //    var item = pi.MultiSortMeta[i];
            //    string key = item.Field;



            //    string orderBy = item.Order == 1 ? "ThenBy" : "ThenByDescending";
            //    if (i == 0)
            //    {
            //        orderBy = item.Order == 1 ? "OrderBy" : "OrderByDescending";
            //    }
            //    rows = rows.ToApplyOrder<JobAdvert>(key, orderBy);
            //}
            //pi.TotalRowCount = rows.Count();

            // Pagination uygula
           // pi.Rows = rows.ToPagination(pi.PageSize, pi.CurPage);

            return new ResultItem(true, pi);
            return new ResultItem(true, rows, null, System.Net.HttpStatusCode.OK, "1");
        }


    }


}
