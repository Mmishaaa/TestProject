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

            //var workers = await _repository.Worker.GetWorkersAsync(trackChanges: false);

            var workersDto = workers.Select(worker =>
            {
                var departmentsDto = worker.Departments.Select(department => _mapper.Map<DepartmentDtoForWorker>(department)).ToList();

                var workersDto = _mapper.Map<WorkerDto>(worker);

                workersDto.Departments = departmentsDto;

                return workersDto;
            });

            return Ok(workersDto);
        }

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

            //var workerDto = _mapper.Map<WorkerDto>(createWorkerDto);

            /* var departmentsForWorker = createWorkerDto.Departments
                                             .Select(department => _mapper.Map<DepartmentDtoForWorker>(department));
             workerDto.Departments = departmentsForWorker;*/

            var worker = _mapper.Map<Worker>(createWorkerDto);

            _repository.Worker.CreateWorkerForDepartment(departmentId, worker);
            await _repository.SaveAsync();

            var workerToReturn = _mapper.Map<WorkerDto>(worker);
            return CreatedAtRoute("GetWorkerForDepartment", new { departmentId, id = workerToReturn.Id }, workerToReturn);
        }

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
