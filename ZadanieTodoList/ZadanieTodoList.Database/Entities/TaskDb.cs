namespace ZadanieTodoList.Database.Entities
{
    public class TaskDb
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public bool Priority { get; set; }
        public bool Completed { get; set; }
    }
}
