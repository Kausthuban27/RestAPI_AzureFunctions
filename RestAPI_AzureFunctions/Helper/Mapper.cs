using AutoMapper;
using RestAPI_AzureFunctions.ViewModel;
using RestAPI_AzureFunctions.Model;

namespace RestAPI_AzureFunctions.Helper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserData, User>();
        }
    }
}
