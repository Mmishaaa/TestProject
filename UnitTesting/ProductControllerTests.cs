using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.DTO.Product;
using Entitties.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TestProject.Controllers;

namespace UnitTesting
{
    public class ProductControllerTests
    {
        private readonly ProductsController _sut;

        private readonly Mock<IRepositoryManager> _productRepoMock = new Mock<IRepositoryManager>();
        private readonly Mock<ILogger<ProductsController>> _loggerMock = new Mock<ILogger<ProductsController>>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public ProductControllerTests()
        {
            _sut = new ProductsController(_productRepoMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        private async Task GetProductsForDepartment_ShouldReturnProducts_WhenDepartmentExists()
        {
            var departmentId = Guid.NewGuid();
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product 1", Description = "Description For Product 1", Weight = 0.1, DepartmentId = departmentId },
                new Product { Id = Guid.NewGuid(), Name = "Product 2", Description = "Description For Product 2", Weight = 0.2, DepartmentId = departmentId },
                new Product { Id = Guid.NewGuid(), Name = "Product 3", Description = "Description For Product 3", Weight = 0.3, DepartmentId = departmentId }
            };

            _productRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(new Department { Id = departmentId, Name = "Department", Products = products });

            _productRepoMock.Setup(repo => repo.Product.GetProductsAsync(departmentId, false))
                .ReturnsAsync(products);

            var productDtos = products.Select(product => new ProductDto { Id = product.Id, Name = product.Name, Description = product.Description, Weight = product.Weight });

            _mapperMock.Setup(mapper => mapper.Map<ProductDto>(It.IsAny<Product>()))
                .Returns((Product product) => productDtos.FirstOrDefault(dto => dto.Id == product.Id));

            // Act
            var result = await _sut.GetProductsForDepartment(departmentId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProductDtos = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(okResult.Value);
            Assert.Equal(products.Count, returnedProductDtos.Count());
        }

        [Fact]
        private async Task GetProductsForDepartment_ShouldReturnNotFound_WhenDepartmentDoesNotExist()
        {
            var departmentId = Guid.NewGuid();
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product 1", Description = "Description For Product 1", Weight = 0.1, DepartmentId = departmentId },
                new Product { Id = Guid.NewGuid(), Name = "Product 2", Description = "Description For Product 2", Weight = 0.2, DepartmentId = departmentId },
                new Product { Id = Guid.NewGuid(), Name = "Product 3", Description = "Description For Product 3", Weight = 0.3, DepartmentId = departmentId }
            };

            _productRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetProductsForDepartment(departmentId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        private async Task GetProductForDepartment_ShouldReturnProduct_WhenDepartmentExists()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Name = "Product 1", Description = "Description For Product 1", Weight = 0.1, DepartmentId = departmentId };

            _productRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(new Department { Id = departmentId, Name = "Department", Products = new List<Product> { product } });

            _productRepoMock.Setup(repo => repo.Product.GetProductAsync(departmentId, productId, false))
                .ReturnsAsync(product);

            var productDto = new ProductDto { Id = product.Id, Name = product.Name, Description = product.Description, Weight = product.Weight };

            _mapperMock.Setup(mapper => mapper.Map<ProductDto>(product))
                .Returns(productDto);

            // Act
            var result = await _sut.GetProductForDepartment(departmentId, productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProductDto = Assert.IsAssignableFrom<ProductDto>(okResult.Value);
            Assert.Equal(productDto.Id, returnedProductDto.Id);
            Assert.Equal(productDto.Name, returnedProductDto.Name);
            Assert.Equal(productDto.Description, returnedProductDto.Description);
            Assert.Equal(productDto.Weight, returnedProductDto.Weight);

        }
        [Fact]
        private async Task GetProductForDepartment_ShouldReturnNotFound_WhenDepartmentDoesNotExist()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Name = "Product 1", Description = "Description For Product 1", Weight = 0.1, DepartmentId = departmentId };

            _productRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(() => null);
            // Act
            var result = await _sut.GetProductForDepartment(departmentId, productId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        private async Task GetProductForDepartment_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var departmentId = Guid.NewGuid();

            _productRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(new Department { Id = departmentId, Name = "Department", Products = new List<Product>() });

            _productRepoMock.Setup(repo => repo.Product.GetProductAsync(departmentId, Guid.NewGuid(), false))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetProductForDepartment(departmentId, Guid.NewGuid());

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task CreateProductForDepartment_ShouldCreateProduct_WhenDepartmentExistsAndValidDto()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var createProductDto = new CreateProductDto { Name = "New Product", Description = "Description For New Product", Weight = 0.345 };

            var department = new Department { Id = departmentId, Name = "Department", Description = "Description For Department 1", Products = new List<Product>(), Workers = new List<Worker>() };

            _productRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(department);

            var createdProduct = new Product { Id = Guid.NewGuid(), Name = createProductDto.Name, Description = createProductDto.Description, Weight = createProductDto.Weight, DepartmentId = departmentId };
            _mapperMock.Setup(mapper => mapper.Map<Product>(createProductDto))
                .Returns(createdProduct);

            _productRepoMock.Setup(repo => repo.Product.CreateProductForDepartment(departmentId, createdProduct));

            _mapperMock.Setup(mapper => mapper.Map<ProductDto>(It.IsAny<Product>()))
                .Returns(new ProductDto
                {
                    Id = createdProduct.Id,
                    Name = createProductDto.Name,
                    Description = createdProduct.Description,
                    Weight = createdProduct.Weight,
                });

            // Act
            var result = await _sut.CreateProductForDepartment(departmentId, createProductDto);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            var productDto = Assert.IsAssignableFrom<ProductDto>(createdAtRouteResult.Value);
            Assert.Equal(createdProduct.Id, productDto.Id);
            Assert.Equal(createdProduct.Name, productDto.Name);
            Assert.Equal("GetProductForDepartment", createdAtRouteResult.RouteName);
            Assert.Equal(departmentId, createdAtRouteResult.RouteValues["departmentId"]);
            Assert.Equal(productDto.Id, createdAtRouteResult.RouteValues["id"]);
        }

        [Fact]
        public async Task CreateProductForDepartment_ShouldReturnBadRequest_WhenDtoIsNull()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            CreateProductDto createProductDto = null;

            // Act
            var result = await _sut.CreateProductForDepartment(departmentId, createProductDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateProductForDepartment_ShouldReturnUnprocessableEntity_WhenModelStateIsNotValid()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var createProductDto = new CreateProductDto { Name = "New Product", Weight = 0.345 };

            _sut.ModelState.AddModelError("Description", "Product Description is a required field.");

            // Act
            var result = await _sut.CreateProductForDepartment(departmentId, createProductDto);

            // Assert
            var unprocessableEntityResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, unprocessableEntityResult.StatusCode);
            Assert.IsType<SerializableError>(unprocessableEntityResult.Value);
        }

        [Fact]
        public async Task CreateProductForDepartment_ShouldReturnNotFound_WhenDepartmentDoesNotExist()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var createProductDto = new CreateProductDto { Name = "New Product", Description = "Description For New Product", Weight = 0.345 };

            _productRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.CreateProductForDepartment(departmentId, createProductDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        private async Task DeleteProductForDepartment_ShouldReturnNoContent_WhenProductExists()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var product = new Product { Id = productId, Name = "Product 1", Description = "Description For Product", Weight = 0.345 };

            var department = new Department
            {
                Id = departmentId,
                Name = "Department",
                Description = "Description For Department 1",
                Products = new List<Product> { },
                Workers = new List<Worker>()
            };

            _productRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(department);


            _productRepoMock.Setup(repo => repo.Product.GetProductAsync(departmentId, productId, false))
                .ReturnsAsync(product);

            // Act
            var result = await _sut.DeleteProductForDepartment(departmentId, productId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        private async Task DeleteProductForDepartment_ShouldReturnNotFound_WhenDepartmentDoesNotExist()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var product = new Product { Id = productId, Name = "Product 1", Description = "Description For Product", Weight = 0.345 };

            _productRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.DeleteProductForDepartment(departmentId, productId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        private async Task DeleteProductForDepartment_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var department = new Department
            {
                Id = departmentId,
                Name = "Department",
                Description = "Description For Department 1",
                Products = new List<Product> { },
                Workers = new List<Worker>()
            };

            _productRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(department);


            _productRepoMock.Setup(repo => repo.Product.GetProductAsync(departmentId, productId, false))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.DeleteProductForDepartment(departmentId, productId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        private async Task UpdateProductForDepartment_ShouldReturnNoContent_WhenProductEsists()
        {
            // Arrange 
            var departmentId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var updateProductDto = new UpdateProductDto
            {
                Name = "New Name",
                Description = "New Description",
                Weight = 1.111,
            };

            var product = new Product { Id = productId, Name = "Name", Description = "Description", Weight = 0.01, DepartmentId = departmentId };
            var department = new Department { Id = departmentId, Name = "Department", Description = "Description For Department 1", Products = new List<Product> { product }, Workers = new List<Worker>() };

            _productRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(department);
            _productRepoMock.Setup(repo => repo.Product.GetProductAsync(departmentId, productId, true))
                .ReturnsAsync(product);
            _mapperMock.Setup(mapper => mapper.Map(updateProductDto, product))
             .Callback((UpdateProductDto dto, Product product) =>
             {
                 product.Name = dto.Name;
                 product.Description = dto.Description;
                 product.Weight = dto.Weight;
             });

            // Act
            var result = await _sut.UpdateProductForDepartment(departmentId, productId, updateProductDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(updateProductDto.Name, product.Name);
            Assert.Equal(updateProductDto.Description, product.Description);
            Assert.Equal(updateProductDto.Weight, product.Weight);
        }
        [Fact]
        private async Task UpdateProductForDepartment_ShouldReturnBadRequest_WhenDtoIsNull()
        {
            // Arrange 
            var departmentId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            UpdateProductDto updateProductDto = null;

            // Act
            var result = await _sut.UpdateProductForDepartment(departmentId, productId, updateProductDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        private async Task UpdateProductForDepartment_ShouldReturnNoContent_WhenDtoIsNotValid()
        {
            /// Arrange
            var workerId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var departmentId = Guid.NewGuid();

            var departmentFromDb = new Department
            {
                Id = departmentId,
                Name = "Test",
                Description = "Description",
                Products = new List<Product>
                {
                   new Product { Id = productId, Name = "Product1", Description = "Product1.1", Weight = 0.3 }
                },
                Workers = new List<Worker> { new Worker { Id = workerId, FirstName = "Worker 1", LastName = "Worker 1.1", Age = 20 } }
            };

            var updateProductDto = new UpdateProductDto
            {
                Name = "Test",
                Weight = 2000,
            };

            _productRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, true))
                .ReturnsAsync(departmentFromDb);
            _sut.ModelState.AddModelError("Description", "Description is a required field.");

            // Act
            var result = await _sut.UpdateProductForDepartment(departmentId, productId, updateProductDto);

            // Assert
            var createdAtRouteResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

        [Fact]
        private async Task UpdateProductForDepartment_ShouldReturnNotFound_WhenDepartmentDoesNotExist()
        {
            // Arrange 
            var departmentId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var updateProductDto = new UpdateProductDto
            {
                Name = "New Name",
                Description = "New Description",
                Weight = 1.111,
            };

            _productRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.UpdateProductForDepartment(departmentId, productId, updateProductDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        private async Task UpdateProductForDepartment_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange 
            var departmentId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var updateProductDto = new UpdateProductDto
            {
                Name = "New Name",
                Description = "New Description",
                Weight = 1.111,
            };

            var department = new Department { Id = departmentId, Name = "Department", Description = "Description For Department 1", Products = new List<Product> { }, Workers = new List<Worker>() };

            _productRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(department);
            _productRepoMock.Setup(repo => repo.Product.GetProductAsync(departmentId, productId, true))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.UpdateProductForDepartment(departmentId, productId, updateProductDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        private async Task PartiallyUpdateProductForDepartment_ShouldReturnBadRequest_WhenPatchDocIsNUll()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            JsonPatchDocument<UpdateProductDto> patchDoc = null;

            // Act
            var result = await _sut.PartiallyUpdateProductForDepartment(departmentId, productId, patchDoc);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        private async Task PartiallyUpdateProductForDepartment_ShouldReturnNotFound_WhenDepartmentDoesNotExist()
        {
            // Arrange 
            var departmentId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var patchDoc = new JsonPatchDocument<UpdateProductDto>();
            patchDoc.Replace(p => p.Name, "Updated Product Name");


            _productRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(() => null);
        
            // Act
            var result = await _sut.PartiallyUpdateProductForDepartment(departmentId, productId, patchDoc);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        private async Task PartiallyUpdateProductForDepartment_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange 
            var departmentId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var patchDoc = new JsonPatchDocument<UpdateProductDto>();
            patchDoc.Replace(p => p.Name, "Updated Product Name");

            var department = new Department { Id = departmentId, Name = "Department", Description = "Description For Department 1", Products = new List<Product> { }, Workers = new List<Worker>() };
            _productRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(department);

            _productRepoMock.Setup(repo => repo.Product.GetProductAsync(departmentId, productId, true))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.PartiallyUpdateProductForDepartment(departmentId, productId, patchDoc);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
