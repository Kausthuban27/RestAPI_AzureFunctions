using Grpc.Core;
using System.Net;
using Todo_CRUD.Model;
using Todo_CRUD.ViewModel;

namespace Todo_CRUD.Interface
{
    public interface ITodo
    {
        Task<(HttpStatusCode statusCode, string)> AddTodoTasks(TodoData todos);
        Task<IEnumerable<Todo>> GetTodoTasks(string username);
        Task<(HttpStatusCode statusCode, Todo todoTask, string)> UpdateTodoTasks(Todo todos);
        Task<(HttpStatusCode statusCode, string)> DeleteTodoTasks(string username);
        void SaveChanges();
    }
}
