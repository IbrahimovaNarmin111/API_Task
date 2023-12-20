using API_Task.Entities.Base;

namespace API_Task.Entities
{
    public class Category:BaseEntity
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get;set; }
        public string FullName { get; set; }
    }
}
