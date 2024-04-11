using AutoMapper;
using Contracts;
using Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TestProject.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger<DepartmentsController> _logger;
        private readonly IMapper _mapper;
        public DepartmentsController(IRepositoryManager repository, ILogger<DepartmentsController> logger, IMapper mapper)
        {
            _repository = repository;
           _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetDepartments()
        {
            try
            {
                var departments = _repository.Department.GetAllDepartments(trackChanges: false);

                /* var departmentsDto = departments.Select(d => new DepartmentDTO
                 {
                     Id = d.Id,
                     Name = d.Name,
                     Description = d.Description,
                 }).ToList();*/

                var departmentsDto = departments.Select(department => _mapper.Map<DepartmentDTO>(department));

                return Ok(departmentsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetDepartments)} action {ex} ");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
