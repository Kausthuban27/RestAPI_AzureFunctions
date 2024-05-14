using Microsoft.AspNetCore.Mvc;
using User_Validation.Model;
using User_Validation.ViewModel;
using System.Net;

namespace User_Validation.Interface
{
    public interface IUser
    {
        Task<(HttpStatusCode StatusCode, bool)> GetUser(string username, string password);
        Task<(HttpStatusCode StatusCode, bool)> AddUsers(UserData user);
        void SaveChanges();
    }
}
