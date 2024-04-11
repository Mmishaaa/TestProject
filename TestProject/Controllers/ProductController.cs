using AutoMapper;
using Contracts;
using Entities.DTO;
using Entitties.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace TestProject.Controllers
{
    [Route("api/departments/{departmentId}/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger<DepartmentsController> _logger;
        private readonly IMapper _mapper;
        public ProductController(IRepositoryManager repository, ILogger<DepartmentsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetWorkersForDepartment(Guid departmentId)
        {
            var department = _repository.Department.GetDepartment(departmentId, trackChanges: false);

            if (department == null)
            {
                _logger.LogInformation($"Department with id: {departmentId} doesn't exist in the database.");
                return NotFound();
            }

            var productsFromDb = _repository.Product.GetProducts(departmentId, trackChanges: false);


            var productsDto = productsFromDb.Select(product => _mapper.Map<ProductDto>(product));

            return Ok(productsDto);
        }

        [HttpGet("{id}", Name = "GetProductForDepartment")]
        public IActionResult GetWorkerForDepartment(Guid departmentId, Guid id)
        {
            var department = _repository.Department.GetDepartment(departmentId, trackChanges: false);

            if (department == null)
            {
                _logger.LogInformation($"Department with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var productFromDb = _repository.Product.GetProduct(departmentId, id, trackChanges: false);

            if (productFromDb == null)
            {
                _logger.LogInformation($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var product = _mapper.Map<ProductDto>(productFromDb);
            return Ok(product);
        }

        [HttpPost]
        //[Route("api/departments/{departmentId}/products")]
        public IActionResult CreateProductForDepartment(Guid departmentId, [FromBody] CreateProductDto createProductDto)
        {
            if(createProductDto == null)
            {
                _logger.LogInformation("CreateProductDto object sent from client is null");
                return BadRequest("CreateProductDto object is null");
            }

            var department = _repository.Department.GetDepartment(departmentId, trackChanges: false);

            if(department == null)
            {
                _logger.LogInformation($"Department with id: ${departmentId} doesn't exist in database");
                return NotFound();
            }

            var product = _mapper.Map<Product>(createProductDto);
            _repository.Product.CreateProductForDepartment(departmentId, product);
            _repository.Save();

            var productToReturn = _mapper.Map<ProductDto>(product);
            return CreatedAtRoute("GetProductForDepartment", new { departmentId, id = productToReturn.Id }, productToReturn);
        }
    }
}
