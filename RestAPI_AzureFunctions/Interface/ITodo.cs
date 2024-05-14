using User_Validation.Model;
using User_Validation.ViewModel;

namespace User_Validation.Interface
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
