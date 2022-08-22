using Entities.Abstract;

namespace Entities.Concrete
{
    public class Question : BaseEntity, IEntity
    {
        public string Text { get; set; }
    }
}
