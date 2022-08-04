using Entities.Abstract;

namespace Entities.Concrete
{
    public class Answer : IEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int IsDeleted { get; set; }
    }
}
