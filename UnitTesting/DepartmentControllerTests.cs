using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.DTO.Department;
using Entities.DTO.Product;
using Entities.DTO.Worker;
using Entitties.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TestProject.Controllers;
namespace UnitTesting
{
    public class DepartmentControllerTests
    {
        private readonly DepartmentsController _sut;

        private readonly Mock<IRepositoryManager> _departmentRepoMock = new Mock<IRepositoryManager>();
        private readonly Mock<ILogger<DepartmentsController>> _loggerMock = new Mock<ILogger<DepartmentsController>>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public DepartmentControllerTests()
        {
            _sut = new DepartmentsController(_departmentRepoMock.Object, _loggerMock.Object, _mapperMock.Object);
        }
        [Fact]
        public async Task GetDepartment_ShouldReturnDepartment_WhenDepartmentExists()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var department = new Department
            {
                Id = departmentId,
                Name = "Test",
                Description = "Test",
                Products = new List<Product> { new Product { Id = Guid.NewGuid(), Name = "Product1", Description = "Product1.1", Weight = 0.3, DepartmentId = departmentId } },
                Workers = new List<Worker> { new Worker { Id = Guid.NewGuid(), FirstName = "Worker 1", LastName = "Worker 1.1", Age = 20 } }
            };

            var departmentDTO = new DepartmentDTO
            {
                Id = departmentId,
                Name = "Test",
                Description = "Test",
                Products = new List<ProductDto> { new ProductDto { Id = Guid.NewGuid(), Name = "Product1", Description = "Product1.1", Weight = 0.3 } },
                Workers = new List<WorkerDtoForDepartment> { new WorkerDtoForDepartment { FirstName = "Worker 1", LastName = "Worker 1.1" } }
            };

            _departmentRepoMock.Setup(x => x.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(department);
            _mapperMock.Setup(x => x.Map<DepartmentDTO>(It.IsAny<Department>()))
                .Returns(departmentDTO);

            // Act
            var returnedDepartment = await _sut.GetDepartment(departmentId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(returnedDepartment);
            var departmentDto = Assert.IsAssignableFrom<DepartmentDTO>(okResult.Value);
        }

        [Fact]
        public async Task GetDepartment_ShouldReturnNotFound_WhenDepartmentDoesNotExists()
        {
            // Arrange
            _departmentRepoMock.Setup(x => x.Department.GetDepartmentAsync(It.IsAny<Guid>(), false))
                 .ReturnsAsync(() => null);

            // Act
            var returnedDepartment = await _sut.GetDepartment(Guid.NewGuid());

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(returnedDepartment);
            Assert.NotNull(notFoundResult);
        }

        [Fact]

        public async Task GetDepartments_ShouldReturnAllDepartments_WhenDepartmentsExist()
        {

            // Arrange
            var firstDepartmentId = Guid.NewGuid();
            var secondDepartmentId = Guid.NewGuid();
            var thirdDepartmentId = Guid.NewGuid();

            var firstWorkerId = Guid.NewGuid();
            var secondWorkerId = Guid.NewGuid();
            var thirdWorkerId = Guid.NewGuid();

            var firstProductId = Guid.NewGuid();
            var secondProductId = Guid.NewGuid();
            var thirdProductId = Guid.NewGuid();

            var departments = new List<Department>
            {
                new Department
                {
                    Id = firstDepartmentId,
                    Name = "Test Department 1",
                    Description = "Description For Test Department 1",
                    Products = new List<Product> { new Product { Id = firstProductId, Name = "Product 1", Description = "Product 1.1", Weight = 0.1, DepartmentId = firstDepartmentId } },
                    Workers = new List<Worker> { new Worker { Id = firstWorkerId, FirstName = "Worker 1", LastName = "Worker 1.1", Age = 21  } }
                },
                new Department
                {
                    Id = secondDepartmentId,
                    Name = "Test Department 2",
                    Description = "Description For Test Department 2",
                    Products = new List<Product> { new Product { Id = secondProductId, Name = "Product 2", Description = "Product 2.1", Weight = 0.2, DepartmentId = secondDepartmentId } },
                    Workers = new List<Worker> { new Worker { Id = secondWorkerId, FirstName = "Worker 2", LastName = "Worker 2.1", Age = 22 } }
                },
                new Department
                {
                    Id = thirdDepartmentId,
                    Name = "Test Department 3",
                    Description = "Description For Test Department 3",
                    Products = new List<Product> { new Product { Id = thirdProductId, Name = "Product 3", Description = "Product 3.1", Weight = 0.3, DepartmentId = thirdDepartmentId } },
                    Workers = new List<Worker> { new Worker { Id = thirdWorkerId, FirstName = "Worker 3", LastName = "Worker 3.1", Age = 23 } }
                },
            };

            var departmentsDto = new List<DepartmentDTO>
            {
                 new DepartmentDTO
                {
                    Id = firstDepartmentId,
                    Name = "Test Department 1",
                    Description = "Description For Test Department 1",
                    Products = new List<ProductDto> { new ProductDto { Id = firstProductId, Name = "Product 1", Description = "Product 1.1", Weight = 0.1 } },
                    Workers = new List<WorkerDtoForDepartment> { new WorkerDtoForDepartment { Id = firstWorkerId, FirstName = "Worker 1", LastName = "Worker 1.1", Age = 21  } }
                },
                new DepartmentDTO
                {
                    Id = secondDepartmentId,
                    Name = "Test Department 2",
                    Description = "Description For Test Department 2",
                    Products = new List<ProductDto> { new ProductDto { Id = secondProductId, Name = "Product 2", Description = "Product 2.1", Weight = 0.2 } },
                    Workers = new List<WorkerDtoForDepartment> { new WorkerDtoForDepartment { Id = secondWorkerId, FirstName = "Worker 2", LastName = "Worker 2.1", Age = 22 } }
                },
                new DepartmentDTO
                {
                    Id = thirdDepartmentId,
                    Name = "Test Department 3",
                    Description = "Description For Test Department 3",
                    Products = new List<ProductDto> { new ProductDto { Id = thirdProductId, Name = "Product 3", Description = "Product 3.1", Weight = 0.3 } },
                    Workers = new List<WorkerDtoForDepartment> { new WorkerDtoForDepartment { Id = thirdWorkerId, FirstName = "Worker 3", LastName = "Worker 3.1", Age = 23 } }
                },
            };

            _departmentRepoMock.Setup(x => x.Department.GetAllDepartmentsAsync(false))
                .ReturnsAsync(departments);

            _mapperMock.Setup(x => x.Map<DepartmentDTO>(It.IsAny<Department>()))
                .Returns((Department department) =>
                {
                    var workersDto = department.Workers.Select(worker => _mapperMock.Object.Map<WorkerDtoForDepartment>(worker)).ToList();

                    return new DepartmentDTO
                    {
                        Id = department.Id,
                        Name = department.Name,
                        Description = department.Description,
                        Products = department.Products.Select(product => new ProductDto
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Description = product.Description,
                            Weight = product.Weight
                        }).ToList(),
                        Workers = workersDto
                    };
                });

            // Act
            var returnedResult = await _sut.GetDepartments();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(returnedResult);
            var returnedDepartments = Assert.IsAssignableFrom<IEnumerable<DepartmentDTO>>(okObjectResult.Value).ToList();
            Assert.Equal(departmentsDto.Count(), returnedDepartments.Count());
        }

        [Fact]
        public async Task GetDepartments_ShouldReturnEmptyArray_WhenDepartmentsExist()
        {

            // Arrange
            var departments = new List<Department>();

            _departmentRepoMock.Setup(x => x.Department.GetAllDepartmentsAsync(false))
                 .ReturnsAsync(departments);
            // Act
            var returnedResult = await _sut.GetDepartments();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(returnedResult);
            var returnedDepartments = Assert.IsAssignableFrom<IEnumerable<DepartmentDTO>>(okObjectResult.Value).ToList();
            Assert.Empty(returnedDepartments);
        }

        [Fact]
        public async Task CreateDepartment_ShouldCreateDepartment_WhenCreateDepartmentDtoIsValid()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var workerId = Guid.NewGuid();
            var createDepartmentDto = new CreateDepartmentDto
            {
                Name = "newDepartment",
                Description = "Description for a new department",
                Products = new List<CreateProductDto> { new CreateProductDto { Name = "Product1", Description = "Product1.1", Weight = 0.3 } },
                Workers = new List<CreateWorkerDtoForDepartment> { new CreateWorkerDtoForDepartment { FirstName = "Worker 1", LastName = "Worker 1.1", Age = 20 } }
            };

            var departmentDto = new DepartmentDTO
            {
                Id = departmentId,
                Name = "newDepartment",
                Description = "Description for a new department",
                Products = new List<ProductDto> { new ProductDto { Id = productId, Name = "Product1", Description = "Product1.1", Weight = 0.3 } },
                Workers = new List<WorkerDtoForDepartment> { new WorkerDtoForDepartment { Id = workerId, FirstName = "Worker 1", LastName = "Worker 1.1", Age = 20 } }

            };

            var department = new Department
            {
                Id = departmentId,
                Name = "newDepartment",
                Description = "Description for a new department",
                Products = new List<Product> { new Product { Id = productId, Name = "Product1", Description = "Product1.1", Weight = 0.3, DepartmentId = departmentId } },
                Workers = new List<Worker> { new Worker { Id = workerId, FirstName = "Worker 1", LastName = "Worker 1.1", Age = 20 } }
            };

            _mapperMock.Setup(m => m.Map<DepartmentDTO>(createDepartmentDto)).Returns(departmentDto);
            _mapperMock.Setup(x => x.Map<WorkerDtoForDepartment>(It.IsAny<CreateWorkerDtoForDepartment>()))
               .Returns((CreateWorkerDtoForDepartment workerDtoForDepartment) =>
               {
                   return new WorkerDtoForDepartment
                   {
                       Id = workerId,
                       FirstName = workerDtoForDepartment.FirstName,
                       LastName = workerDtoForDepartment.LastName,
                       Age = workerDtoForDepartment.Age
                   };
               });

            _mapperMock.Setup(m => m.Map<Department>(departmentDto)).Returns(department);
            _mapperMock.Setup(m => m.Map<DepartmentDTO>(department)).Returns(departmentDto);

            _departmentRepoMock.Setup(x => x.Department.CreateDepartment(department));

            // Act
            var result = await _sut.CreateDepartment(createDepartmentDto);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(departmentDto, createdAtRouteResult.Value);

        }
        [Fact]
        public async Task CreateDepartment_ShouldReturnBadRequestObjectResult_WhenCreateDepartmentDtoIsNull()
        {
            // Arrange
            CreateDepartmentDto createDepartment = null;

            // Act
            var result = await _sut.CreateDepartment(createDepartment);

            // Assert
            var createdAtRouteResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        private async Task CreateDepartment_ShouldReturnUnprocessableEntityResult_WhenCreateDepartmentDtoIsInvalid()
        {
            // Arrange
            var createDepartment = new CreateDepartmentDto
            {
                Name = "newDepartment",
                Description = "Description for a new department",
                Workers = new List<CreateWorkerDtoForDepartment> { new CreateWorkerDtoForDepartment { FirstName = "Worker 1", LastName = "Worker 1.1", Age = 20 } }
            };

            _sut.ModelState.AddModelError("Products", "Products (stored in Department) is a required field.");

            // Act
            var result = await _sut.CreateDepartment(createDepartment);

            // Assert
            var createdAtRouteResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

        [Fact]
        private async Task DeleteDepartment_ShouldDeleteDepartment_WhenDepartmentExists()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var department = new Department
            {
                Id = departmentId,
                Name = "Test",
                Description = "Test",
                Products = new List<Product> { new Product { Id = Guid.NewGuid(), Name = "Product1", Description = "Product1.1", Weight = 0.3, DepartmentId = departmentId } },
                Workers = new List<Worker> { new Worker { Id = Guid.NewGuid(), FirstName = "Worker 1", LastName = "Worker 1.1", Age = 20 } }
            };

            _departmentRepoMock.Setup(r => r.Department.GetDepartmentAsync(departmentId, false)).ReturnsAsync(department);

            // Act
            var result = await _sut.DeleteDepartment(departmentId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        private async Task DeleteDepartment_ShouldReturnNotFoundResult_WhenDepartmentDoesnNotExist()
        {
            // Arrange
            var departmentId = Guid.NewGuid();

            _departmentRepoMock.Setup(r => r.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.DeleteDepartment(departmentId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        private async Task UpdateDepartment_ShouldUpdateDepartment_WhenUpdateDepartmentDtoIsValidAndDepartmentExists()
        {
            // Arrange
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

            var updateDepartmentDto = new UpdateDepartmentDto
            {
                Name = "Test",
                Description = "Description",
                Products = new List<CreateProductDto>
                {
                   new CreateProductDto { Name = "Product1", Description = "Product1.1", Weight = 0.3 }
                },
                Workers = new List<CreateWorkerDtoForDepartment> { new CreateWorkerDtoForDepartment { FirstName = "Worker 1", LastName = "Worker 1.1", Age = 20 } }
            };

            _mapperMock.Setup(mapper => mapper.Map(updateDepartmentDto, departmentFromDb)).Verifiable();

            _departmentRepoMock.Setup(x => x.Department.GetDepartmentAsync(departmentId, true))
                .ReturnsAsync(departmentFromDb);

            // Act
            var result = await _sut.UpdateDepartment(departmentId, updateDepartmentDto);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            _mapperMock.Verify(m => m.Map(updateDepartmentDto, departmentFromDb), Times.Once);
        }

        [Fact]
        private async Task UpdateDepartment_ShouldReturnBadRequest_WhenUpdateDepartmentDtoIsNull()
        {
            // Arrange
            UpdateDepartmentDto updateDepartmentDto = null;


            // Act
            var result = await _sut.UpdateDepartment(Guid.NewGuid(), updateDepartmentDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        private async Task UpdateDepartment_ShouldReturnUnprocessableEntity_WhenUpdateDepartmentDtoIsNotValid()
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

            var updateDepartmentDto = new UpdateDepartmentDto
            {
                Name = "Test",
                Description = "Description",
                Workers = new List<CreateWorkerDtoForDepartment> { new CreateWorkerDtoForDepartment { FirstName = "Worker 1", LastName = "Worker 1.1", Age = 20 } }
            };

            _departmentRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, true))
                .ReturnsAsync(departmentFromDb);
            _sut.ModelState.AddModelError("Products", "Products (stored in Department) is a required field.");

            // Act
            var result = await _sut.UpdateDepartment(departmentId, updateDepartmentDto);

            // Assert
            var createdAtRouteResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

        [Fact]
        private async Task UpdateDepartment_ShouldReturnNotFound_WhenDepartmentDoesNotExist()
        {
            // Arrange
            var updateDepartmentDto = new UpdateDepartmentDto
            {
                Name = "Test",
                Description = "Description",
                Products = new List<CreateProductDto>
                {
                   new CreateProductDto { Name = "Product1", Description = "Product1.1", Weight = 0.3 }
                },
                Workers = new List<CreateWorkerDtoForDepartment> { new CreateWorkerDtoForDepartment { FirstName = "Worker 1", LastName = "Worker 1.1", Age = 20 } }
            };

            _departmentRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(Guid.NewGuid(), true))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.UpdateDepartment(Guid.NewGuid(), updateDepartmentDto);

            // Assert
            var returnedResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        private async Task PartiallyUpdateDepartment_ShouldReturnNoContent_WhenPatchDocIsValidAndDepartmentExists()
        {
            var workerId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var departmentId = Guid.NewGuid();

            var newName = "Updated Department Name";

            var patchDoc = new JsonPatchDocument<UpdateDepartmentDto>();
            patchDoc.Replace(d => d.Name, newName);

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

            _mapperMock.Setup(x => x.Map<UpdateDepartmentDto>(It.IsAny<Department>()))
                 .Returns((Department department) =>
                 {
                     var workersDto = department.Workers.Select(worker => _mapperMock.Object.Map<CreateWorkerDtoForDepartment>(worker)).ToList();
                     var productsDto = department.Products.Select(product => _mapperMock.Object.Map<CreateProductDto>(product)).ToList();
                     return new UpdateDepartmentDto
                     {
                         Name = department.Name,
                         Description = department.Description,
                         Products = productsDto,
                         Workers = workersDto
                     };
                 });

            _departmentRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, true))
               .ReturnsAsync(departmentFromDb);

            _mapperMock.Setup(mapper => mapper.Map(It.IsAny<UpdateDepartmentDto>(), departmentFromDb))
               .Callback<UpdateDepartmentDto, Department>((updateDto, department) =>
               {
                   department.Name = updateDto.Name;
               });

            // Act
            var result = await _sut.PartiallyUpdateDepartment(departmentId, patchDoc);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(newName, departmentFromDb.Name);
        }

        [Fact]
        private async Task PartiallyUpdateDepartment_ShouldReturnBadRequest_WhenPatchDocIsNull()
        {
            // Arrange
            JsonPatchDocument<UpdateDepartmentDto> patchDoc = null;

            // Act
            var result = await _sut.PartiallyUpdateDepartment(Guid.NewGuid(), patchDoc);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        private async Task PartiallyUpdateDepartment_ShouldReturnNotFound_WhenDepartmentDoesNotExist()
        {
            // Arrange
            var newName = "Updated Department Name";

            var patchDoc = new JsonPatchDocument<UpdateDepartmentDto>();
            patchDoc.Replace(d => d.Name, newName);

            _departmentRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(Guid.NewGuid(), true))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.PartiallyUpdateDepartment(Guid.NewGuid(), patchDoc);

            // Assert
            var returnedResult = Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task PartiallyUpdateDepartment_ShouldReturnUnprocessableEntity_WhenModelStateIsInvalid()
        {
            // Arrange
            var departmentId = Guid.NewGuid();

            var patchDoc = new JsonPatchDocument<UpdateDepartmentDto>();
            patchDoc.Replace(d => d.Name, "Updated Department");
            patchDoc.Replace(d => d.Description, null); // Invalid: Description cannot be null

            var departmentFromDb = new Department
            {
                Id = departmentId,
                Name = "Test Department",
                Description = "Department description"
            };

            var departmentToPatch = new UpdateDepartmentDto
            {
                Name = "Updated Department",
                Description = null
            };

            _departmentRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, true))
                .ReturnsAsync(departmentFromDb);

            _mapperMock.Setup(mapper => mapper.Map<UpdateDepartmentDto>(departmentFromDb))
                .Returns(departmentToPatch);

            _sut.ModelState.AddModelError("Description", "The Description field is required.");

            // Act
            var result = await _sut.PartiallyUpdateDepartment(departmentId, patchDoc);

            // Assert
            var unprocessableEntityResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
            var modelStateDictionary = Assert.IsAssignableFrom<SerializableError>(unprocessableEntityResult.Value);
            Assert.True(modelStateDictionary.ContainsKey("Description"));
        }
    }
}
