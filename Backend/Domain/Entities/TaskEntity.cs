namespace TEST.Domain.Entities
{
    public partial class TaskEntity : BaseEntity
    {
        public TaskEntity()
        {
           
        }

        public string? Name { get; set; }
        public string? Description { get; set; }

      
    }
}