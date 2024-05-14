using AutoMapper;
using Todo_CRUD.Interface;
using Todo_CRUD.Model;
using Todo_CRUD.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_CRUD.Service
{
    public class TodoService : ITodo
    {
        private readonly TodoContext _todoContext;
        private readonly IMapper _todoMapper;
        public TodoService(TodoContext todoContext, IMapper mapper)
        {
            _todoContext = todoContext;
            _todoMapper = mapper;
        }

        public bool AddTodoTasks(TodoData todos)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTodoTasks(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Todo> GetTodoTasks(string username)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _todoContext.SaveChanges();
        }

        public bool UpdateTodoTasks(Todo todos)
        {
            throw new NotImplementedException();
        }
    }
}
