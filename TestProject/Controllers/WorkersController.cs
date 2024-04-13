using AutoMapper;
using Contracts;
using Entities.DTO.Department;
using Entities.DTO.Worker;
using Entitties.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestProject.Controllers
{
    [Route("api/workers")]
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
        public async Task<IActionResult> GetAllWorkers()
        {
            var workers = await _repository.Worker.GetWorkersAsync(trackChanges: false);

            var workersDto = workers.Select(worker =>
            {
                var departmentsDto = worker.Departments.Select(department => _mapper.Map<DepartmentDtoForWorker>(department)).ToList();

                var workersDto = _mapper.Map<WorkerDto>(worker);

                workersDto.Departments = departmentsDto;

                return workersDto;
            });

            return Ok(workersDto);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetWorker(Guid id)
        {

            var worker = await _repository.Worker.GetWorkerAsync(id, trackChanges: false);

            if(worker == null)
            {
                _logger.LogInformation($"Worker with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var workerDto = _mapper.Map<WorkerDto>(worker);
            workerDto.Departments = worker.Departments.Select(department => _mapper.Map<DepartmentDtoForWorker>(department));

            return Ok(workerDto);
        }
      
    }
}
