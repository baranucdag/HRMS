using Business.Abstract;
using Business.Constans;
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
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerDal answerDal;

        public AnswerService(IAnswerDal answerDal)
        {
            this.answerDal = answerDal;
        }
        public ResultItem Add(Answer answer)
        {
            answerDal.Add(answer);
            return new ResultItem(true);
        }

        public ResultItem Delete(int id)
        {
            var deletedEntity = answerDal.Get(x=>x.Id == id);
            answerDal.SoftDelete(deletedEntity);
            return new ResultItem(true);
        }

        public ResultItem GetAll()
        {
            
            var result = answerDal.GetAll().OrderByDescending(x=>x.Id);
            return new ResultItem(true,answerDal.GetAll(),Messages.DataListed);
        }

        public ResultItem GetById(int id)
        {
            return new ResultItem(true,answerDal.Get(x => x.Id == id),Messages.DataListed);
        }

        public ResultItem GetByQuestionId(int questionId)
        {
            return new ResultItem(true,answerDal.GetAll(x => x.QuestionId == questionId),Messages.DataListed);
        }

        public ResultItem Update(Answer answer)
        {
            answerDal.Update(answer);
            return new ResultItem(true);
        }

        public ResultItem GetPaginationData(PaginationItem<Answer> pi)
        {
            var rows = answerDal.GetAll().AsQueryable();

            rows.OrderByDescending(x => x.Id);

            //Grid kolonlarından veya global searchden arama geldiyse
            if (pi.Filters != null && pi.Filters.Count() > 0)
            {
                List<Answer> globalList = new List<Answer>();
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
                            var props = typeof(Answer).GetProperties();
                            foreach (var prop in props)
                            {
                                var res = new List<Answer>();

                                switch (prop.Name)
                                {
                                    case nameof(Answer.Text):
                                        {
                                            res = rows.AsEnumerable().Where(x => x.Text.Contains(val)).ToList();
                                            break;
                                        }

                                    default:
                                        break;
                                }
                                globalList.AddRange(res.ToList());
                            }
                            rows = globalList.GroupBy(x => x.Id).Select(x => x.First()).AsQueryable();
                            globalList = new List<Answer>();
                        }

                    }
                }
            }
            return new ResultItem(true, rows, null, System.Net.HttpStatusCode.OK, "1");
        }

       /* public async Task<ResultItem> GetPaginationData(PaginationItem<Answer> pi)
        {
            try
            {
                var list = answerDal.GetAll();
                var rows = list.AsQueryable();
               
                // Son girilen kayıt en üstte gözükecek
                rows = rows.OrderByDescending(x => x.Id);

                // Grid kolonlarından veya global searchden arama geldiyse
                if (pi.Filters != null && pi.Filters.Count() > 0)
                {
                    List<Answer> globalList = new List<Answer>();
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
                            if (item.Key.ToLower() == "global")
                            {
                                var props = typeof(Answer).GetProperties();

                                foreach (var prop in props)
                                {
                                    if (!pi.Filters.Keys.FirstOrDefault(x => x.ToLower() == prop.Name.ToLower()).IsNullOrEmpty())
                                    {
                                        var res = new List<Answer>();

                                        switch (prop.Name)
                                        {
                                            case nameof(Answer.Text):
                                                {
                                                    res = rows.AsEnumerable().Where(x => x.Text.ToLower().Contains(oVal)).ToList();
                                                    break;
                                                }
                                        }
                                        globalList.AddRange(res.ToList());
                                    }
                                }
                                rows = globalList.GroupBy(x => x.Id).Select(x => x.First()).AsQueryable();
                                globalList = new List<Answer>();
                            }
                            else // grid column search - spesifik kolon bazlı işlemler için
                            {
                                switch (key)
                                {
                                    case nameof(Answer.Text):
                                        {
                                            switch (matchMode.ToLower())
                                            {
                                                case "startswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Text.ToLower().StartsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "endswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Text.ToLower().EndsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "contains":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Text.ToLower().Contains(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "equals":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Text.ToLower().Equals(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case nameof(Answer.ClientCount):
                                        {
                                            rows = rows.Where(x => x.ClientCount == val.ToIntDef(-1));
                                            break;
                                        }
                                    case nameof(t_CustomersSummaryDto.CustomerName):
                                        {
                                            switch (matchMode.ToLowerEng())
                                            {
                                                case "startswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Text.ToLower().StartsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "endswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Text.ToLower().EndsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "contains":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Text.ToLower().Contains(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "equals":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.Text.ToLower().Equals(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case nameof(t_CustomersSummaryDto.PartnerName):
                                        {
                                            switch (matchMode.ToLowerEng())
                                            {
                                                case "startswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.PartnerName.ToLowerEng().StartsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "endswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.PartnerName.ToLowerEng().EndsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "contains":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.PartnerName.ToLowerEng().Contains(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "equals":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.PartnerName.ToLowerEng().Equals(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case nameof(t_CustomersSummaryDto.CustomerType):
                                    case nameof(t_CustomersSummaryDto.CustomerTypeText):
                                        {
                                            switch (val.ToIntDef(-1))
                                            {
                                                case 0: // getAll
                                                    {
                                                        break;
                                                    }
                                                case 1: // Individual
                                                    {
                                                        rows = rows.Where(x => x.CustomerType == 1);
                                                        break;
                                                    }
                                                case 2: // Corporate
                                                    {
                                                        rows = rows.Where(x => x.CustomerType == 2);
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }

                                    case nameof(t_CustomersSummaryDto.HasParent):
                                    case nameof(t_CustomersSummaryDto.HasParentText):
                                        {
                                            switch (val.ToIntDef(-1))
                                            {
                                                case 0: // getAll
                                                    {
                                                        break;
                                                    }
                                                case 1: // Has Parent
                                                    {
                                                        rows = rows.Where(x => x.HasParent == 1);
                                                        break;
                                                    }
                                                case 2: // Not Having Parent
                                                    {
                                                        rows = rows.Where(x => x.HasParent == 0);
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }

                                    case nameof(t_CustomersSummaryDto.CustomerPhone):
                                        {
                                            switch (matchMode.ToLowerEng())
                                            {
                                                case "startswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.CustomerPhone.ToLowerEng().StartsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "endswith":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.CustomerPhone.ToLowerEng().EndsWith(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "contains":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.CustomerPhone.ToLowerEng().Contains(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                case "equals":
                                                    {
                                                        rows = rows.AsEnumerable().Where(x => x.CustomerPhone.ToLowerEng().Equals(oVal)).AsQueryable();
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            break;
                                        }
                                    case nameof(t_CustomersSummaryDto.IsDeleted):
                                    case nameof(t_CustomersSummaryDto.IsDeletedText):
                                        {
                                            switch (val.ToIntDef(-1))
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
                        if (key == "IsConfirmText") key = "IsConfirm";
                        if (key == "IsDeletedText") key = "IsDeleted";
                        if (key == "HasParentText") key = "HasParent";
                        if (key == "CustomerTypeText") key = "CustomerType";
                        if (key == "HasParentText") key = "ParentId";


                        string orderBy = item.Order == 1 ? "ThenBy" : "ThenByDescending";
                        if (i == 0)
                        {
                            orderBy = item.Order == 1 ? "OrderBy" : "OrderByDescending";
                        }
                        rows = rows.ToApplyOrder<t_CustomersSummaryDto>(key, orderBy);
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
        }*/
    }
}
