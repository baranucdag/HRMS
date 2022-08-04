using Business.Abstract;
using Business.Constans;
using Core.Extensions;
using Core.Utilities.Helpers.PaginationHelper;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateDal candidateDal;

        public CandidateService(ICandidateDal candidateDal)
        {
            this.candidateDal = candidateDal;
        }
        public ResultItem Add(Candidate candidate)
        {
            candidateDal.Add(candidate);
            return new ResultItem();
        }

        public ResultItem Delete(int id)
        {
            var deletedEntity = candidateDal.Get(x => x.Id == id);
            candidateDal.SoftDelete(deletedEntity);
            return new ResultItem();
        }

        public ResultItem UnDelete(int id)
        {
            var deletedEntity = candidateDal.Get(x => x.Id == id);
            candidateDal.UnDelete(deletedEntity);
            return new ResultItem(true);
        }

        public ResultItem GetById(int id)
        {
            var result =  candidateDal.Get(x => x.Id == id);
            return new ResultItem(true, result,Messages.DataListed);
        }

        public ResultItem Update(Candidate candidate)
        {
            candidateDal.Update(candidate);
            return new ResultItem();
        }

        public ResultItem GetCandidatesPaginated(PaginationItem<Candidate> pi)
        {
            try
            {
                var rows = candidateDal.GetAll().AsQueryable();

                // Grid kolonlarından veya global searchden arama geldiyse
                if (pi.Filters != null && pi.Filters.Count() > 0)
                {
                    List<Candidate> globalList = new List<Candidate>();
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
                                var props = typeof(Candidate).GetProperties();

                                foreach (var prop in props)
                                {
                                    var res = new List<Candidate>();

                                    switch (prop.Name)
                                    {
                                        case nameof(Candidate.EMail):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.EMail.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(Candidate.CandidateFullName):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.CandidateFullName.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(Candidate.Profession):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.Profession.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        case nameof(Candidate.PhoneNumber):
                                            {
                                                res = rows.AsEnumerable().Where(x => x.PhoneNumber.ToLowerEng().Contains(oVal)).ToList();
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    globalList.AddRange(res.ToList());

                                }
                                rows = globalList.GroupBy(x => x.Id).Select(x => x.First()).AsQueryable();
                                globalList = new List<Candidate>();
                            }
                            else // grid column search - spesifik kolon bazlı işlemler için
                            {
                                switch (key)
                                {
                                    case nameof(Candidate.EMail):
                                        {
                                            switch (matchMode.ToLowerEng())
                                            {
                                                case "startswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.EMail.ToLowerEng().StartsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "endswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.EMail.ToLowerEng().EndsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "contains":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.EMail.ToLowerEng().Contains(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "equals":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.EMail.ToLowerEng().Equals(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case nameof(Candidate.CandidateFullName):
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
                                    case nameof(Candidate.Profession):
                                        {
                                            switch (matchMode.ToLowerEng())
                                            {
                                                case "startswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Profession.ToLowerEng().StartsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "endswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Profession.ToLowerEng().EndsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "contains":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Profession.ToLowerEng().Contains(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "equals":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Profession.ToLowerEng().Equals(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case nameof(Candidate.PhoneNumber):
                                        {
                                            switch (matchMode.ToLowerEng())
                                            {
                                                case "startswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.PhoneNumber.ToLowerEng().StartsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "endswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.PhoneNumber.ToLowerEng().EndsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "contains":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.PhoneNumber.ToLowerEng().Contains(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "equals":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.PhoneNumber.ToLowerEng().Equals(oVal)).AsQueryable();
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
                        rows = rows.ToApplyOrder<Candidate>(key, orderBy);
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
    


