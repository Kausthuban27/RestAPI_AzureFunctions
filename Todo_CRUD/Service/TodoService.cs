using AutoMapper;
using Todo_CRUD.Interface;
using Todo_CRUD.Model;
using Todo_CRUD.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        public async Task<(HttpStatusCode statusCode, string)> AddTodoTasks(TodoData todos)
        {
            var todoTasks = _todoMapper.Map<Todo>(todos);
            if(todos != null)
            {
                var newTask = await _todoContext.Todos.Where(t => t.TaskName == todos.TaskName).ToListAsync();
                if(newTask == null || !newTask.Any())
                {
                    _todoContext.Todos.Add(todoTasks);
                    return (HttpStatusCode.OK, "Tasks Added Successfully");
                }
                else
                {
                    return (HttpStatusCode.BadRequest, "Tasks Already Exist");
                }
            }
            return (HttpStatusCode.BadRequest, "Failed To Add Tasks");
        }

        public bool DeleteTodoTasks(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetTodoTasks(string username)
        {
            var tasks = await _todoContext.Todos.Where(u => u.Username == username).ToListAsync();
            if(tasks != null)
            {
                return tasks.Select(task => task.TaskName).ToList(); ; 
            }
            else
            {
                return new List<string>();
            }
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
