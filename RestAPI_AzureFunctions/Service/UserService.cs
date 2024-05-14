using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User_Validation.Interface;
using User_Validation.Model;
using User_Validation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace User_Validation.Service
{
    public class UserService : IUser
    {
        private readonly TodoContext _todoContext;
        private readonly IMapper _userMapper;
        public UserService(TodoContext todoContext, IMapper mapper)
        {
            _todoContext = todoContext;
            _userMapper = mapper;
        }
        
        public async Task<(HttpStatusCode StatusCode, bool)> AddUsers(UserData user)
        {
            var newUser = _userMapper.Map<User>(user);
            if (newUser != null)
            {
                var userData = await _todoContext.Users.FirstOrDefaultAsync(u => u.Username == newUser.Username);
                if (userData == null)
                {
                    _todoContext.Add(newUser);
                    return (HttpStatusCode.OK, true);
                }
                else
                {
                    return (HttpStatusCode.BadRequest, false);
                }
            }
            else
            {
                return (HttpStatusCode.BadRequest, false);
            }
        }

        public async Task<(HttpStatusCode StatusCode, bool)> GetUser(string username, string password)
        {
            var user = await _todoContext.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if(user != null)
            {
                return (HttpStatusCode.OK, true);
            }
            return (HttpStatusCode.BadRequest, false);
        }

        public void SaveChanges()
        {
            _todoContext.SaveChanges();
        }
    }
}
