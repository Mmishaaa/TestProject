using AutoMapper;
using Contracts;
using Entities.DTO.Department;
using Entities.DTO.Worker;
using Entitties.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace TestProject.Controllers
{
    //[Route("api/workers")]
    [Route("api/departments/{departmentId}/workers")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger<WorkersController> _logger;
        private readonly IMapper _mapper;

        public WorkersController(IRepositoryManager repository, ILogger<WorkersController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        /// <summary>
        /// Gets All Workers
        /// </summary>
        /// <returns>All workers</returns>
        /// <response code="200">returns all workers</response>
        /// <response code="404">if department does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpGet]
        public async Task<IActionResult> GetWorkersForDepartment(Guid departmentId)
        {
            var department = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges: false);
            if (department == null)
            {
                _logger.LogInformation($"Department with id: {departmentId} doesn't exist in the database.");
                return NotFound();
            }

            var workers = await _repository.Worker.GetWorkersAsync(departmentId, trackChanges: false);

            var workersDto = workers.Select(worker =>
            {
                var departmentsDto = worker.Departments.Select(department => _mapper.Map<DepartmentDtoForWorker>(department)).ToList();

                var workersDto = _mapper.Map<WorkerDto>(worker);

                workersDto.Departments = departmentsDto;

                return workersDto;
            });

            return Ok(workersDto);
        }

        /// <summary>
        /// Gets a specific worker from department.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="id"></param>
        /// <returns>a specific worker from department.t</returns>
        /// <response code="200">returns specific worker</response>
        /// <response code="404">If department / worker does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpGet("{id}", Name = "GetWorkerForDepartment")]

        public async Task<IActionResult> GetWorkerForDepartment(Guid departmentId, Guid id)
        {

            var department = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges: false);

            if (department == null)
            {
                _logger.LogInformation($"Department with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var workerFromDb = await _repository.Worker.GetWorkerAsync(departmentId, id, trackChanges: false);

            if (workerFromDb == null)
            {
                _logger.LogInformation($"Worker with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var workerDto = _mapper.Map<WorkerDto>(workerFromDb);
            workerDto.Departments = workerFromDb.Departments.Select(department => _mapper.Map<DepartmentDtoForWorker>(department));

            return Ok(workerDto);
        }

        /// <summary>
        /// Creates a worker for department
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="createWorkerDto"></param>
        /// <returns>A newly created worker</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///         POST /api/departments/{departmentId}/workers
        ///         {
        ///             "firstname": "string",
        ///             "lastname": "string",
        ///             "age": 25
        ///         }
        /// 
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the dto is null</response>
        /// <response code="404">If department does not exist</response>
        /// <response code="422">If the dto is invalid</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]

        [HttpPost]
        public async Task<IActionResult> CreateWorker(Guid departmentId, [FromBody] CreateWorkerDto createWorkerDto)
        {
            if (createWorkerDto == null)
            {
                _logger.LogError("CreateWorkerDto object sent from client is null.");
                return BadRequest("CreateWorkerDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid CreateWorkerDto object");
                return UnprocessableEntity(ModelState);
            }

            var department = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges: false);

            if (department == null)
            {
                _logger.LogInformation($"Department with id: ${departmentId} doesn't exist in database");
                return NotFound();
            }

            var worker = _mapper.Map<Worker>(createWorkerDto);

            _repository.Worker.CreateWorkerForDepartment(departmentId, worker);
            await _repository.SaveAsync();

            var workerToReturn = _mapper.Map<WorkerDto>(worker);
            return CreatedAtRoute("GetWorkerForDepartment", new { departmentId, id = workerToReturn.Id }, workerToReturn);
        }

        /// <summary>
        /// Deletes a specific worker from department.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">If department does not exist</response>
        /// <response code="404">If worker does not exist</response>
        /// <response code="204">successfully deleted</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(Guid departmentId, Guid id)
        {
            var department = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges: false);

            if (department == null)
            {
                _logger.LogInformation($"Department with id {departmentId} doesn't exist in the database");
                return NotFound();
            }

            var workerFromDb = await _repository.Worker.GetWorkerAsync(departmentId, id, trackChanges: false);

            if (workerFromDb == null)
            {
                _logger.LogInformation($"Worker with id: {id} doesn't exist in the database");
                return NotFound();
            }

            _repository.Worker.DeleteWorker(workerFromDb);
            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Updates a worker from department.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="id"></param>
        /// <param name="updateWorkerDto"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///         Put /api/departments/{departmentId}/workers/{id}
        ///         {
        ///             "firstname": "string",
        ///             "lastname": "string",
        ///             "age": 25
        ///         }
        /// 
        /// </remarks>
        /// <response code="204">Successfully updated</response>
        /// <response code="400">If the dto is null</response>
        /// <response code="422">If the dto is invalid</response>
        /// <response code="404">If department / worker does not exist</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkerForDepartment(Guid departmentId, Guid id, [FromBody] UpdateWorkerDto updateWorkerDto)
        {
            if (updateWorkerDto == null)
            {
                _logger.LogError("UpdateWorkerDto sent from client is null");
                return BadRequest("UpdateWorkerDto is null");
            }

            var department = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges: false);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid EmployeeForCreationDto object");
                return UnprocessableEntity(ModelState);
            }


            if (department == null)
            {
                _logger.LogInformation($"Department with id: {departmentId} doesn't exist in the database");
                return NotFound();
            }

            var workerFromDb = await _repository.Worker.GetWorkerAsync(departmentId, id, trackChanges: true);

            if (workerFromDb == null)
            {
                _logger.LogInformation($"Worker with id: {id} doesn't exist in the database");
                return NotFound();
            }

            _mapper.Map(updateWorkerDto, workerFromDb);
            await _repository.SaveAsync();

            return NoContent();

        }

        /// <summary>
        /// Partially updates worker from department.
        /// </summary>
        /// <param name="departmentId"></param>
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
        /// <response code="404">If department / worker does not exist</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateWorkerFromDepartment(Guid departmentId, Guid id, [FromBody] JsonPatchDocument<UpdateWorkerDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var departmentFromDb = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges: false);

            if(departmentFromDb == null)
            {
                _logger.LogInformation($"Department with id: {departmentId} doesn't exist in the database");
                return NotFound();
            }

            var workerFromDb = await _repository.Worker.GetWorkerAsync(departmentId, id, trackChanges: true);

            if(workerFromDb == null)
            {
                _logger.LogInformation($"Worker with id: {id} doesn't exist in the database");
                return NotFound();
            }

            var workerToPatch = _mapper.Map<UpdateWorkerDto>(workerFromDb);

            patchDoc.ApplyTo(workerToPatch, ModelState);

            TryValidateModel(workerToPatch);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(workerToPatch, workerFromDb);
            await _repository.SaveAsync();
            return NoContent();
        }


    }
}
