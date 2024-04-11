using AutoMapper;
using Contracts;
using Entities.DTO;
using Entitties.Models;
using Microsoft.AspNetCore.Mvc;

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
            var departments = _repository.Department.GetAllDepartments(trackChanges: false);

            var departmentsDto = departments.Select(department => _mapper.Map<DepartmentDTO>(department));

            return Ok(departmentsDto);
        }

        [HttpGet("{id}", Name = "DepartmentById")]

        public IActionResult GetDepartment(Guid id)
        {
            var department = _repository.Department.GetDepartment(id, trackChanges: false);

            if (department == null)
            {
                _logger.LogInformation($"Department with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var departmentDto = _mapper.Map<DepartmentDTO>(department);
            return Ok(departmentDto);
        }

        [HttpPost]
        public IActionResult CreateDepartment([FromBody]CreateDepartmentDto createDepartmentDto)
        {
            if(createDepartmentDto == null)
            {
                _logger.LogError("CreateDepartmentDto object sent from client is null.");
                return BadRequest("CreateDepartmentDto object is null");
            }

            var department = _mapper.Map<Department>(createDepartmentDto);

            _repository.Department.CreateDepartment(department);
            _repository.Save();

            var departmentToReturn = _mapper.Map<DepartmentDTO>(department);
            return CreatedAtRoute("DepartmentById", new { id = departmentToReturn.Id}, departmentToReturn);
        }

    }
}
