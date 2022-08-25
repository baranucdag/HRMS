using Entities.Abstract;

namespace Entities.Concrete
{
    public class Answer : BaseEntity,IEntity
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
    }
}
