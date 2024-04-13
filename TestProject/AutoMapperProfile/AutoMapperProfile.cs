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
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateDepartmentDto, Department>().ReverseMap();
            CreateMap<CreateDepartmentDto, DepartmentDTO>();
            CreateMap<CreateProductDto, Product>().ReverseMap();
            CreateMap<CreateProductDto, ProductDto>();
            CreateMap<UpdateProductDto, Product>().ReverseMap();
            CreateMap<UpdateDepartmentDto, Department>().ReverseMap();
            CreateMap<Worker, WorkerDtoForDepartment>().ReverseMap();
            CreateMap<Department, DepartmentDtoForWorker>();
            CreateMap<Worker, WorkerDto>();
            CreateMap<Worker, CreateWorkerDtoForDepartment>();
            CreateMap<WorkerDtoForDepartment, CreateWorkerDto>().ReverseMap();
            CreateMap<CreateWorkerDtoForDepartment, WorkerDtoForDepartment>();
            CreateMap<CreateWorkerDtoForDepartment, Worker>();
        }
    }
}
