namespace Testing.Models
{
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
        public TodoItem ToEntity()
        {
            return new TodoItem { Id = this.Id, Name = this.Name, IsComplete = this.IsComplete };
        }
    }    
}
