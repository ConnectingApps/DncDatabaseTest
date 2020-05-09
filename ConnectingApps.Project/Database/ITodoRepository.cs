using System.Threading.Tasks;
using ConnectingApps.Project.Models;

namespace ConnectingApps.Project.Database
{
    public interface ITodoRepository
    {
        Task<Entities.TodoItem> SaveItem(TodoItem item);
    }
}