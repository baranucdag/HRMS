using System;

namespace Entities.Concrete
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
