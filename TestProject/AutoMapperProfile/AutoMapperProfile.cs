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
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<UpdateDepartmentDto, Department>();
        }
    }
}
