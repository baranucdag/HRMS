using Entities.Abstract;

namespace Entities.Concrete
{
    public class Question : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDeleted { get; set; }
    }
}
