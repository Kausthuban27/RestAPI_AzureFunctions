using Microsoft.AspNetCore.Mvc;
using RestAPI_AzureFunctions.Model;
using RestAPI_AzureFunctions.ViewModel;
using System.Net;

namespace RestAPI_AzureFunctions.Interface
{
    public interface IUser
    {
        Task<(HttpStatusCode StatusCode, bool)> GetUser(string username, string password);
        Task<(HttpStatusCode StatusCode, bool)> AddUsers(UserData user);
        void SaveChanges();
    }
}
