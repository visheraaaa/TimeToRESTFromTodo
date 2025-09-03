namespace TimeToRESTFromTodo.Models
{
    public class TaskItem
    {
        public TaskItem(string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            CreatedAt = DateTime.Now;
            IsCompleted = false;
        }


        public Guid Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; }
    }
}
