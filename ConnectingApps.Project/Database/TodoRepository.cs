using System.Linq;
using System.Threading.Tasks;
using ConnectingApps.Project.Models;

namespace ConnectingApps.Project.Database
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ProjectContext _projectContext;

        public TodoRepository(ProjectContext projectContext)
        {
            _projectContext = projectContext;
        }

        public async Task<Entities.TodoItem> SaveItem(TodoItem item)
        {
            var newItem = new Entities.TodoItem()
            {
                Todo = item.Todo
            };
            var beforeAdded = _projectContext.ChangeTracker.Entries().ToList();
            _projectContext.TodoItems.Add(newItem);
            var afterAdded = _projectContext.ChangeTracker.Entries().ToList();
            await _projectContext.SaveChangesAsync();

            return newItem;
        }
    }
}
