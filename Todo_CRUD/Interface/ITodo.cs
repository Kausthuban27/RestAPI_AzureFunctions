using Grpc.Core;
using System.Net;
using Todo_CRUD.Model;
using Todo_CRUD.ViewModel;

namespace Todo_CRUD.Interface
{
    public interface ITodo
    {
        Task<(HttpStatusCode statusCode, string)> AddTodoTasks(TodoData todos);
        Task<List<string>> GetTodoTasks(string username);
        bool UpdateTodoTasks(Todo todos);
        bool DeleteTodoTasks(string username);
        void SaveChanges();
    }
}
