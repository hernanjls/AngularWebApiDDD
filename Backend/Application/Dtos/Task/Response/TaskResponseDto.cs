namespace TEST.Application.Dtos.Task.Response
{
    public class TaskResponseDto
    {
        public int TaskId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime AuditCreateDate { get; set; }
        public int State { get; set; }
        public string? StateTask { get; set; }
    }
}