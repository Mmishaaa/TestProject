using AutoMapper;
using Entities.DTO;
using Entities.DTO.Department;
using Entities.DTO.Product;
using Entities.DTO.Worker;
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
            CreateMap<UpdateProductDto, Product>().ReverseMap();
            CreateMap<UpdateDepartmentDto, Department>().ReverseMap();
            CreateMap<Worker, WorkerDtoForDepartment>();
            CreateMap<Department, DepartmentDtoForWorker>();
            CreateMap<Worker, WorkerDto>();
        }
    }
}
