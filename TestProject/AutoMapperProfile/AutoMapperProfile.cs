using AutoMapper;
using Entities.DTO;
using Entitties.Models;

namespace TestProject.AutoMapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Department, DepartmentDTO>();
            CreateMap<Product, ProductDto>();
        }
    }
}
