using AutoMapper;
using Contracts;
using Entities.DTO;
using Entitties.Models;
using Microsoft.AspNetCore.Mvc;
using TestProject.ModelBinders;

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
        public IActionResult CreateDepartment([FromBody] CreateDepartmentDto createDepartmentDto)
        {
            if (createDepartmentDto == null)
            {
                _logger.LogError("CreateDepartmentDto object sent from client is null.");
                return BadRequest("CreateDepartmentDto object is null");
            }

            var department = _mapper.Map<Department>(createDepartmentDto);

            _repository.Department.CreateDepartment(department);
            _repository.Save();

            var departmentToReturn = _mapper.Map<DepartmentDTO>(department);
            return CreatedAtRoute("DepartmentById", new { id = departmentToReturn.Id }, departmentToReturn);
        }


        [HttpGet("collection/({ids})", Name = "DepartmentCollection")]
        public IActionResult GetDepartmentCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var departmentCollection = _repository.Department.GetByIds(ids, trackChanges: false);

            if (ids.Count() != departmentCollection.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var departmentsToReturn = departmentCollection.Select(department => _mapper.Map<DepartmentDTO>(department));
            return Ok(departmentsToReturn);
        }

        [HttpPost("collection")]

        public IActionResult CreateDepartmentCollection([FromBody] IEnumerable<CreateDepartmentDto> departmentCollection)
        {
            if (departmentCollection == null)
            {
                _logger.LogError("Department collection sent from client is null.");
                return BadRequest("Department collection is null");
            }


            var departments = _mapper.Map<IEnumerable<Department>>(departmentCollection);

            foreach (var department in departments)
            {
                _repository.Department.CreateDepartment(department);
            }

            _repository.Save();

            var departmentCollectionToReturn = _mapper.Map<IEnumerable<DepartmentDTO>>(departments);

            var ids = string.Join(",", departmentCollectionToReturn.Select(d => d.Id));

            return CreatedAtRoute("DepartmentCollection", new { ids }, departmentCollectionToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(Guid id)
        {
            var departmentFromDb = _repository.Department.GetDepartment(id, trackChanges: false);

            if(departmentFromDb == null)
            {
                _logger.LogInformation($"Departmnet with id: {id} doesn't exist in the database");
                return NotFound();
            }

            _repository.Department.DeleteDepartment(departmentFromDb);
            _repository.Save();

            return NoContent();
        }
    }
}
