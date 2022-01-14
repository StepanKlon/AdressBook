using AdressBook.Models.Entities;
using AdressBook.Models.ViewModels;
using AutoMapper;

namespace AdressBook.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Contact, ContactViewModel>().ReverseMap();
        }
    }
}
