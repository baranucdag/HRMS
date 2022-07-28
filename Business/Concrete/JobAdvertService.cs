using Business.Abstract;
using Core.Utilities.Helpers.PaginationHelper;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }


}
