using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Testing.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;

        public IEnumerable<TodoItem> GetAll()
        {
            return TodoItems;
        }

        public TodoItem? Add(TodoItem todo)
        {
            this.TodoItems.Add(todo);
            var result = this.SaveChanges();
            return result == 1 ? todo : null;
        }

        public int Update(TodoItem todo)
        {
            var todoDb = TodoItems.Find(todo.Id);
            var result = 0;
            if (todoDb == null)
            {
                return result;
            }
            todoDb.Name = todo.Name;
            todoDb.IsComplete = todo.IsComplete;
            result = this.SaveChanges();
            return result;
        }

        public int Delete(long id)
        {
            var result = 0;
            var todoDb = TodoItems.Find(id);
            if (todoDb == null)
            {
                return result;
            }

            TodoItems.Remove(todoDb);
            result = this.SaveChanges();
            return result;
        }
    }
}
