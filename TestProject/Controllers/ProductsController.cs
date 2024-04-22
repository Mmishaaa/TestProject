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
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        public ProductsController(IRepositoryManager repository, ILogger<ProductsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets All Products
        /// </summary>
        /// <returns>All products</returns>
        /// <response code="200">returns all products</response>
        /// <response code="404">if department does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

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

        /// <summary>
        /// Gets a specific product from department.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="id"></param>
        /// <returns>a specific product from department.t</returns>
        /// <response code="200">returns specific product</response>
        /// <response code="404">If product/department does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

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

        /// <summary>
        /// Creates a product for department
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="createProductDto"></param>
        /// <returns>A newly created product</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///         POST /api/departments/{departmentId}/products
        ///         {
        ///             "name": "string",
        ///             "description": "string",
        ///             "weight": 0.001
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
        public async Task<IActionResult> CreateProductForDepartment(Guid departmentId, [FromBody] CreateProductDto createProductDto)
        {
            if (createProductDto == null)
            {
                _logger.LogInformation("CreateProductDto object sent from client is null");
                return BadRequest("CreateProductDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid createProductDto object");
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

        /// <summary>
        /// Deletes a specific product from department.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">If department does not exist</response>
        /// <response code="404">If product does not exist</response>
        /// <response code="204">successfully deleted</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

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

        /// <summary>
        /// Updates a product from department.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="id"></param>
        /// <param name="updateProductDto"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///         Put /api/departments/{departmentId}/products/{id}
        ///        {
        ///             "name": "string",
        ///             "description": "string",
        ///             "weight": 0.001
        ///         }
        /// 
        /// </remarks>
        /// <response code="204">Successfully updated</response>
        /// <response code="400">If the dto is null</response>
        /// <response code="422">If the dto is invalid</response>
        /// <response code="404">If department / product does not exist</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

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

        /// <summary>
        /// Partially updates product from department.
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
        /// <response code="404">If department / product does not exist</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPatch("{id}")]
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
