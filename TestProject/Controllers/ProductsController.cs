using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.DTO.Product;
using Entitties.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace TestProject.Controllers
{
    [Route("api/departments/{departmentId}/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger<DepartmentsController> _logger;
        private readonly IMapper _mapper;
        public ProductsController(IRepositoryManager repository, ILogger<DepartmentsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsForDepartment(Guid departmentId)
        {
            var department = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges: false);

            if (department == null)
            {
                _logger.LogInformation($"Department with id: {departmentId} doesn't exist in the database.");
                return NotFound();
            }

            var productsFromDb = await _repository.Product.GetProductsAsync(departmentId, trackChanges: false);


            var productsDto = productsFromDb.Select(product => _mapper.Map<ProductDto>(product));

            return Ok(productsDto);
        }

        [HttpGet("{id}", Name = "GetProductForDepartment")]
        public async Task<IActionResult> GetProductForDepartment(Guid departmentId, Guid id)
        {
            var department = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges: false);

            if (department == null)
            {
                _logger.LogInformation($"Department with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var productFromDb = await _repository.Product.GetProductAsync(departmentId, id, trackChanges: false);

            if (productFromDb == null)
            {
                _logger.LogInformation($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var product = _mapper.Map<ProductDto>(productFromDb);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductForDepartment(Guid departmentId, [FromBody] CreateProductDto createProductDto)
        {
            if (createProductDto == null)
            {
                _logger.LogInformation("CreateProductDto object sent from client is null");
                return BadRequest("CreateProductDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid EmployeeForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var department = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges: false);

            if (department == null)
            {
                _logger.LogInformation($"Department with id: ${departmentId} doesn't exist in database");
                return NotFound();
            }

            var product = _mapper.Map<Product>(createProductDto);
            _repository.Product.CreateProductForDepartment(departmentId, product);
            await _repository.SaveAsync();

            var productToReturn = _mapper.Map<ProductDto>(product);
            return CreatedAtRoute("GetProductForDepartment", new { departmentId, id = productToReturn.Id }, productToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductForDepartment(Guid departmentId, Guid id)
        {
            var department = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges: false);

            if (department == null)
            {
                _logger.LogInformation($"Department with id {departmentId} doesn't exist in the database");
                return NotFound();
            }

            var productForDepartment = await _repository.Product.GetProductAsync(departmentId, id, trackChanges: false);

            if (productForDepartment == null)
            {
                _logger.LogInformation($"Product with id: {id} doesn't exist in the database");
                return NotFound();
            }

            _repository.Product.DeleteProduct(productForDepartment);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductForDepartment(Guid departmentId, Guid id, [FromBody] UpdateProductDto updateProductDto)
        {
            if (updateProductDto == null)
            {
                _logger.LogError("UpdateProductDto sent from client is null");
                return BadRequest("UpdateProductDro is null");
            }

            var departmentFromDb = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges: false);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid EmployeeForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            if (departmentFromDb == null)
            {
                _logger.LogInformation($"Department with id: {departmentId} doesn't exist in the database");
                return NotFound();
            }

            var productFromDb = await _repository.Product.GetProductAsync(departmentId, id, trackChanges: true);

            if (productFromDb == null)
            {
                _logger.LogInformation($"Product with id: {id} doesn't exist in the database");
                return NotFound();
            }
            _mapper.Map(updateProductDto, productFromDb);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> PartiallyUpdateProductForDepartment(Guid departmentId, Guid id, [FromBody] JsonPatchDocument<UpdateProductDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }


            var departmentFromDb = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges: false);


            if (departmentFromDb == null)
            {
                _logger.LogInformation($"Department with id: {departmentId} doesn't exist in the database");
                return NotFound();
            }

            var productFromDb = await _repository.Product.GetProductAsync(departmentId, id, trackChanges: true);
            if (productFromDb == null)
            {
                _logger.LogInformation($"Product with id: {id} doesn't exist in the database");
                return NotFound();
            }

            var productToPatch = _mapper.Map<UpdateProductDto>(productFromDb);

            patchDoc.ApplyTo(productToPatch, ModelState);

            TryValidateModel(productToPatch);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(productToPatch, productFromDb);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
