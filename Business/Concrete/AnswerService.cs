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
            var deletedEntity = answerDal.Get(x => x.Id == id);
            answerDal.SoftDelete(deletedEntity);
            return new ResultItem(true);
        }


        public ResultItem GetById(int id)
        {
            return new ResultItem(true, answerDal.Get(x => x.Id == id), null);
        }

        public ResultItem GetByQuestionId(int questionId)
        {
            return new ResultItem(true, answerDal.GetAll(x => x.QuestionId == questionId), Messages.DataListed);
        }

        public ResultItem Update(Answer answer)
        {
            answerDal.Update(answer);
            return new ResultItem(true);
        }

        public ResultItem GetAnswerDetails()
        {
            return new ResultItem(true, answerDal.GetAnswerDetails(), Messages.DataListed);
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

    }
}
