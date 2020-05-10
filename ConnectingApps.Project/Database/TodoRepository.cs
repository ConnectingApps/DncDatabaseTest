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

            _projectContext.TodoItems.Add(newItem);
            await _projectContext.SaveChangesAsync();
            return newItem;
        }
    }
}
