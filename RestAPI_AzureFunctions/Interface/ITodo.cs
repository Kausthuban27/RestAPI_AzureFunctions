﻿using RestAPI_AzureFunctions.Model;
using RestAPI_AzureFunctions.ViewModel;

namespace RestAPI_AzureFunctions.Interface
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
