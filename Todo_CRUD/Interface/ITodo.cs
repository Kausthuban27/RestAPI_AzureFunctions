using Todo_CRUD.Model;
using Todo_CRUD.ViewModel;

namespace Todo_CRUD.Interface
{
    public interface ITodo
    {
        bool AddTodoTasks(TodoData todos);
        IEnumerable<Todo> GetTodoTasks(string username);
        bool UpdateTodoTasks(Todo todos);
        bool DeleteTodoTasks(string username);
        void SaveChanges();
    }
}
