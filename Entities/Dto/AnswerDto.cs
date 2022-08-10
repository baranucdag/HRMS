using Entities.Abstract;

namespace Entities.Dto
{
    public  class AnswerDto:IDto
    {
        public int Id { get; set; }
        public int  QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string Text { get; set; }
        public int IsDeleted { get; set; }
    }
}
