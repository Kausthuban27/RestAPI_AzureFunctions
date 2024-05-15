using AutoMapper;
using Todo_CRUD.ViewModel;
using Todo_CRUD.Model;

namespace Todo_CRUD.Helper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<TodoData, Todo>();
        }
    }
}
