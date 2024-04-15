using AutoMapper;
using Contracts;
using Entities.DTO.Department;
using Entities.DTO.Worker;
using Entitties.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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

        /// <summary>
        /// Gets All Departments
        /// </summary>
        /// <returns>All departments</returns>
        /// <response code="200">returns all departments</response>
        [ProducesResponseType(StatusCodes.Status200OK)]

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _repository.Department.GetAllDepartmentsAsync(trackChanges: false);

            var departmentsDto = departments.Select(department =>
            {
                var workersDto = department.Workers.Select(worker => _mapper.Map<WorkerDtoForDepartment>(worker)).ToList();

                var departmentDto = _mapper.Map<DepartmentDTO>(department);
                departmentDto.Workers = workersDto;

                return departmentDto;
            });

            return Ok(departmentsDto);
        }

        /// <summary>
        /// Gets a specific department.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A specific department</returns>
        /// <response code="200">returns specific department</response>
        /// <response code="404">If department does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpGet("{id}", Name = "DepartmentById")]
        public async Task<IActionResult> GetDepartment(Guid id)
        {
            var department = await _repository.Department.GetDepartmentAsync(id, trackChanges: false);

            if (department == null)
            {
                _logger.LogInformation($"Department with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var departmentDto = _mapper.Map<DepartmentDTO>(department);
            departmentDto.Workers = department.Workers.Select(worker => _mapper.Map<WorkerDtoForDepartment>(worker));

            return Ok(departmentDto);
        }

        /// <summary>
        /// Creates a department.
        /// </summary>
        /// <param name="createDepartmentDto"></param>
        /// <returns>A newly created department</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///         POST /api/departments
        ///         {
        ///             "Name": "DepartmentName",
        ///             "Description": "Description For Department",
        ///             "Workers": [
        ///                 {
        ///                     "FirstName": "Anton",
        ///                     "LastName": "Antonov",
        ///                     "Age": 20
        ///                 },
        ///                 {
        ///                     "FirstName": "Petr",
        ///                     "LastName": "Petrov",
        ///                     "Age": 20
        ///                 }  
        ///             ],
        ///             "Products": [
        ///                 {
        ///                     "Name": "ProductName",
        ///                     "Description": "Description For Product",
        ///                     "Weight": 0.001
        ///                 }
        ///             ]
        ///         }
        /// 
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the dto is null</response>
        /// <response code="422">If the dto is invalid</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDto createDepartmentDto)
        {
            if (createDepartmentDto == null)
            {
                _logger.LogError("CreateDepartmentDto object sent from client is null.");
                return BadRequest("CreateDepartmentDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid CreateDepartmentDto object");
                return UnprocessableEntity(ModelState);
            }

            var departmentDto = _mapper.Map<DepartmentDTO>(createDepartmentDto);

            var workersForDepartment = createDepartmentDto.Workers.Select(worker => _mapper.Map<WorkerDtoForDepartment>(worker));

            departmentDto.Workers = workersForDepartment;

            var department = _mapper.Map<Department>(departmentDto);


            _repository.Department.CreateDepartment(department);
            await _repository.SaveAsync();

            var departmentToReturn = _mapper.Map<DepartmentDTO>(department);
            return CreatedAtRoute("DepartmentById", new { id = departmentToReturn.Id }, departmentToReturn);
        }

        /// <summary>
        /// Gets collection Of Departments
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>All collection of fepartments</returns>
        /// <response code="200">Returns collection</response>
        /// <response code="400">If ids collection is null</response>
        /// <response code="404">If some ids re invalidt</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpGet("collection", Name = "DepartmentCollection")]
        public async Task<IActionResult> GetDepartmentCollection([FromQuery][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var departmentCollection = await _repository.Department.GetByIdsAsync(ids, trackChanges: false);

            if (ids.Count() != departmentCollection.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var departmentsDto = departmentCollection.Select(department =>
            {
                var workersDto = department.Workers.Select(worker => _mapper.Map<WorkerDtoForDepartment>(worker)).ToList();

                var departmentDto = _mapper.Map<DepartmentDTO>(department);
                departmentDto.Workers = workersDto;

                return departmentDto;
            });


            return Ok(departmentsDto);
        }

        /// <summary>
        /// Creates many departments
        /// </summary>
        /// <param name="departmentCollection"></param>
        /// <returns>newly created departments</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post/api/departments/collection
        ///     [
        ///         {
        ///             "Name": "DepartmentName1",
        ///             "Description": "Description For Department1",
        ///              "Workers": [
        ///                     {
        ///                        "FirstName": "Anton1",
        ///                         "LastName": "Antonov1",
        ///                          "Age": 20
        ///                     },
        ///                     {
        ///                        "FirstName": "Petr1",
        ///                        "LastName": "Petrov1",
        ///                        "Age": 20
        ///                     }
        ///                  ],
        ///             "Products": [
        ///                     {
        ///                         "Name": "ProductName1",
        ///                         "Description": "Description For Product1",
        ///                         "Weight": 0.001
        ///                     }
        ///                   ]
        ///         },
        ///         {
        ///             "Name": "DepartmentName2",
        ///             "Description": "Description For Department2",
        ///             "Workers": [
        ///                     {
        ///                        "FirstName": "Anton2",
        ///                         "LastName": "Antonov2",
        ///                          "Age": 20
        ///                     },
        ///                     {
        ///                        "FirstName": "Petr2",
        ///                        "LastName": "Petrov2",
        ///                        "Age": 20
        ///                     }
        ///                  ],
        ///             "Products": [
        ///                     {
        ///                         "Name": "ProductName2",
        ///                         "Description": "Description For Product2",
        ///                         "Weight": 0.001
        ///                     }
        ///                  ]
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="201">Returns the newly created items</response>
        /// <response code="400">If the departmentCollection is null</response>
        /// <response code="422">If the departmentCollection is invalid</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]

        [HttpPost("collection")]
        public async Task<IActionResult> CreateDepartmentCollection([FromBody] IEnumerable<CreateDepartmentDto> departmentCollection)
        {
            if (departmentCollection == null)
            {
                _logger.LogError("Department collection sent from client is null.");
                return BadRequest("Department collection is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid CreateDepartmentDto object");
                return UnprocessableEntity(ModelState);
            }

            var departments = _mapper.Map<IEnumerable<Department>>(departmentCollection);

            foreach (var department in departments)
            {
                _repository.Department.CreateDepartment(department);
            }

            await _repository.SaveAsync();

            var departmentCollectionToReturn = _mapper.Map<IEnumerable<DepartmentDTO>>(departments);

            var ids = string.Join(",", departmentCollectionToReturn.Select(d => d.Id));

            return CreatedAtRoute("DepartmentCollection", new { ids }, departmentCollectionToReturn);
        }

        /// <summary>
        /// Deletes a specific department.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">If department does not exist</response>
        /// <response code="204">successfully deleted</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            var departmentFromDb = await _repository.Department.GetDepartmentAsync(id, trackChanges: false);

            if (departmentFromDb == null)
            {
                _logger.LogInformation($"Departmnet with id: {id} doesn't exist in the database");
                return NotFound();
            }

            _repository.Department.DeleteDepartment(departmentFromDb);
            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Updates a department.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDepartmentDto"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///         Put /api/departments/{id}
        ///         {
        ///             "Name": "New DepartmentName",
        ///             "Description": "New Description For Department",
        ///         }
        /// 
        /// </remarks>
        /// <response code="204">Successfully updated</response>
        /// <response code="400">If the dto is null</response>
        /// <response code="422">If the dto is invalid</response>
        /// <response code="404">If department does not exist</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(Guid id, [FromBody] UpdateDepartmentDto updateDepartmentDto)
        {
            if (updateDepartmentDto == null)
            {
                _logger.LogError("updateDepartmentDto sent from client is null");
                return BadRequest("updateDepartmentDto is null");
            }

            var departmentFromDb = await _repository.Department.GetDepartmentAsync(id, trackChanges: true);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid CreateDepartmentDto object");
                return UnprocessableEntity(ModelState);
            }

            if (departmentFromDb == null)
            {
                _logger.LogInformation($"Department with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(updateDepartmentDto, departmentFromDb);
            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Partially updates a department.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///       [
        ///               {
        ///                 "operationType": 0,
        ///                 "path": "string",
        ///                 "op": "string",
        ///                 "from": "string",
        ///                 "value": "string"
        ///             }
        ///       ]
        /// 
        /// </remarks>
        /// <response code="204">Successfully updated</response>
        /// <response code="400">If the patchdoc is null</response>
        /// <response code="422">If the patchdoc is invalid</response>
        /// <response code="404">If department does not exist</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateDepartment(Guid id, [FromBody] JsonPatchDocument<UpdateDepartmentDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var departmentFromDb = await _repository.Department.GetDepartmentAsync(id, trackChanges: true);


            if (departmentFromDb == null)
            {
                _logger.LogInformation($"Department with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var departmentToPatch = _mapper.Map<UpdateDepartmentDto>(departmentFromDb);
            patchDoc.ApplyTo(departmentToPatch);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(departmentToPatch, departmentFromDb);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
