using AutoMapper;
using Contracts;
using Entities.DTO.Department;
using Entities.DTO.Worker;
using Entitties.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TestProject.Controllers;

namespace UnitTesting
{
    public class WorkerControllerTests
    {
        private readonly WorkersController _sut;

        private readonly Mock<IRepositoryManager> _workerRepoMock = new Mock<IRepositoryManager>();
        private readonly Mock<ILogger<WorkersController>> _loggerMock = new Mock<ILogger<WorkersController>>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public WorkerControllerTests()
        {
            _sut = new WorkersController(_workerRepoMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        private async Task GetWorkersForDepartment_ShouldReturnWorkersForDepartment_WhenDepartmentExists()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var workerId = Guid.NewGuid();

            var worker1 = new Worker
            {
                Id = workerId,
                FirstName = "Worker 1",
                LastName = "Worker 1.1",
                Age = 21,
                Departments = new List<Department> { new Department { Id = departmentId, Name = "Test", Description = "Test" } }
            };
            var worker2 = new Worker
            {
                Id = workerId,
                FirstName = "Worker 2",
                LastName = "Worker 2.1",
                Age = 22,
                Departments = new List<Department> { new Department { Id = departmentId, Name = "Test", Description = "Test" } }
            };
            var workers = new List<Worker> {
                    worker1,
                    worker2
                };

            var department = new Department
            {
                Id = departmentId,
                Name = "Test",
                Description = "Test",
                Products = new List<Product> { new Product { Id = Guid.NewGuid(), Name = "Product1", Description = "Product1.1", Weight = 0.3, DepartmentId = departmentId } },
                Workers = workers,
            };

            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(department);

            _workerRepoMock.Setup(repo => repo.Worker.GetWorkersAsync(departmentId, false))
                .ReturnsAsync(workers);


            _mapperMock.Setup(x => x.Map<WorkerDto>(It.IsAny<Worker>()))
                .Returns((Worker worker) =>
                {
                    var departmentsDto = worker.Departments.Select(department => _mapperMock.Object.Map<DepartmentDtoForWorker>(department)).ToList();

                    return new WorkerDto
                    {
                        Id = worker.Id,
                        FirstName = worker.FirstName,
                        LastName = worker.LastName,
                        Age = worker.Age,
                        Departments = departmentsDto
                    };
                });

            // Act
            var returnedResult = await _sut.GetWorkersForDepartment(departmentId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(returnedResult);
            var returnedDepartments = Assert.IsAssignableFrom<IEnumerable<WorkerDto>>(okObjectResult.Value).ToList();
            Assert.Equal(workers.Count(), returnedDepartments.Count());
        }

        [Fact]
        private async Task GetWorkersForDepartment_ShouldReturnNotFound_WhenDepartmentDoesNotExist()
        {
            // Arrange
            _workerRepoMock.Setup(x => x.Department.GetDepartmentAsync(It.IsAny<Guid>(), false))
                 .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetWorkersForDepartment(Guid.NewGuid());

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        private async Task GetWorkerForDepartment_ShouldReturnWorker_WhenDepartmentExistsAndWorkerExists()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var workerId = Guid.NewGuid();

            var worker1 = new Worker
            {
                Id = workerId,
                FirstName = "Worker 1",
                LastName = "Worker 1.1",
                Age = 20,
                Departments = new List<Department> { new Department { Id = departmentId, Name = "Test", Description = "Test" } }
            };
            var workers = new List<Worker> {
                    worker1,
                    new Worker { Id = Guid.NewGuid(), FirstName = "Worker 1", LastName = "Worker 1.1", Age = 20 }
                };

            var department = new Department
            {
                Id = departmentId,
                Name = "Test",
                Description = "Test",
                Products = new List<Product> { new Product { Id = Guid.NewGuid(), Name = "Product1", Description = "Product1.1", Weight = 0.3, DepartmentId = departmentId } },
                Workers = workers,
            };

            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(department);

            _workerRepoMock.Setup(repo => repo.Worker.GetWorkerAsync(departmentId, workerId, false))
                .ReturnsAsync(worker1);


            _mapperMock.Setup(x => x.Map<WorkerDto>(It.IsAny<Worker>()))
                .Returns((Worker worker) =>
                {
                    var departmentsDto = worker.Departments.Select(department => _mapperMock.Object.Map<DepartmentDtoForWorker>(department)).ToList();

                    return new WorkerDto
                    {
                        Id = worker.Id,
                        FirstName = worker.FirstName,
                        LastName = worker.LastName,
                        Age = worker.Age,
                        Departments = departmentsDto
                    };
                });

            // Act
            var returnedResult = await _sut.GetWorkerForDepartment(departmentId, workerId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(returnedResult);
            var workerDto = Assert.IsAssignableFrom<WorkerDto>(okObjectResult.Value);
        }
        [Fact]
        private async Task GetWorkerForDepartment_ShouldReturnNotFound_WhenDepartmentDoesNotExist()
        {
            // Arrange
            _workerRepoMock.Setup(x => x.Department.GetDepartmentAsync(It.IsAny<Guid>(), false))
                 .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetWorkerForDepartment(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        private async Task GetWorkerForDepartment_ShouldReturnNotFound_WhenWorkerDoesNotExist()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var department = new Department
            {
                Id = departmentId,
                Name = "Test",
                Description = "Test",
                Products = new List<Product> { new Product { Id = Guid.NewGuid(), Name = "Product1", Description = "Product1.1", Weight = 0.3, DepartmentId = departmentId } },
                Workers = new List<Worker> {
                    new Worker { Id = Guid.NewGuid(), FirstName = "Name", LastName = "lastName", Age = 20 },
                    new Worker { Id = Guid.NewGuid(), FirstName = "Name", LastName = "lastName", Age = 20 }
                },

            };
            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(It.IsAny<Guid>(), false))
                 .ReturnsAsync(department);
            _workerRepoMock.Setup(repo => repo.Worker.GetWorkerAsync(departmentId, Guid.NewGuid(), false))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetWorkerForDepartment(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        private async Task CreateWorker_ShouldReturnWorker_WhenCreateWorkerDtoIsValid()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var workerId = Guid.NewGuid();
            var createWorkerDto = new CreateWorkerDto
            {
                FirstName = "newWorker",
                LastName = "newWorker lastname",
                Age = 20,
            };

            var departmentFromDb = new Department
            {
                Id = departmentId,
                Name = "Test Department",
                Description = "Department description"
            };

            var workerCreated = new Worker
            {
                Id = Guid.NewGuid(),
                FirstName = createWorkerDto.FirstName,
                LastName = createWorkerDto.LastName,
                Age = createWorkerDto.Age,
                Departments = new List<Department> { departmentFromDb }
            };

            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(departmentFromDb);

            _mapperMock.Setup(mapper => mapper.Map<Worker>(createWorkerDto)).Returns(workerCreated);
            _mapperMock.Setup(mapper => mapper.Map<WorkerDto>(workerCreated))
                .Returns(new WorkerDto
                {
                    Id = workerCreated.Id,
                    FirstName = workerCreated.FirstName,
                    LastName = workerCreated.LastName,
                    Age = workerCreated.Age
                });

            _workerRepoMock.Setup(repo => repo.Worker.CreateWorkerForDepartment(departmentId, workerCreated));

            // Act
            var result = await _sut.CreateWorker(departmentId, createWorkerDto);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            var workerDto = Assert.IsAssignableFrom<WorkerDto>(createdAtRouteResult.Value);
            Assert.Equal(workerDto.FirstName, createWorkerDto.FirstName);
            Assert.Equal(workerDto.LastName, createWorkerDto.LastName);
            Assert.Equal(workerDto.Age, createWorkerDto.Age);
        }

        [Fact]
        private async Task CreateWorker_ShouldReturnBadRequest_WhenCreateWorkerDtoIsNull()
        {
            // Arrange
            CreateWorkerDto createWorkerDto = null;
            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(Guid.NewGuid(), false))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.CreateWorker(Guid.NewGuid(), createWorkerDto);

            // Assert
            var createdAtRouteResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        private async Task CreateWorker_ShouldUnprocessableEntity_WhenCreateWorkerDtoIsNotValid()
        {
            // Arrange
            var createWorker = new CreateWorkerDto
            {
                FirstName = "Firstname",
                Age = 22
            };

            _sut.ModelState.AddModelError("LastName", "LastName is a required field.");

            // Act
            var result = await _sut.CreateWorker(Guid.NewGuid(), createWorker);

            // Assert
            var createdAtRouteResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

        [Fact]
        private async Task CreateWorker_ShouldReturnNotFound_WhenDepartmentDoesNotExist()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var workerId = Guid.NewGuid();
            var createWorkerDto = new CreateWorkerDto
            {
                FirstName = "newWorker",
                LastName = "newWorker lastname",
                Age = 20,
            };

            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(Guid.NewGuid(), false))
                .ReturnsAsync(() => null);
            // Act
            var result = await _sut.CreateWorker(departmentId, createWorkerDto);

            // Assert
            var returnedResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteWorker_ShouldReturnNoContent_WhenWorkerExists()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var workerId = Guid.NewGuid();

            var departmentFromDb = new Department
            {
                Id = departmentId,
                Name = "Test Department",
                Description = "Department description"
            };

            var workerFromDb = new Worker
            {
                Id = workerId,
                FirstName = "John",
                LastName = "Doe",
                Age = 30,
                Departments = new List<Department> { departmentFromDb }
            };

            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(departmentFromDb);

            _workerRepoMock.Setup(repo => repo.Worker.GetWorkerAsync(departmentId, workerId, false))
                .ReturnsAsync(workerFromDb);


            // Act
            var result = await _sut.DeleteWorker(departmentId, workerId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteWorker_ShouldReturnNotFound_WhenDepartmentDoesNotExist()
        {
            // Arrange
            var departmentId = Guid.NewGuid();

            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.DeleteWorker(departmentId, Guid.NewGuid());

            // Assert
            var noContentResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        private async Task DeleteWorker_ShouldReturnNotFound_WhenWorkerDoesNotExist()
        {
            // Arrange
            var departmentId = Guid.NewGuid();

            var departmentFromDb = new Department
            {
                Id = departmentId,
                Name = "Test Department",
                Description = "Department description"
            };

            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(departmentFromDb);

            _workerRepoMock.Setup(repo => repo.Worker.GetWorkerAsync(departmentId, Guid.NewGuid(), false))
                .ReturnsAsync(() => null);


            // Act
            var result = await _sut.DeleteWorker(departmentId, Guid.NewGuid());

            // Assert
            var noContentResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateWorkerForDepartment_ShouldReturnNoContent_WhenWorkerExists()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var workerId = Guid.NewGuid();

            var updateWorkerDto = new UpdateWorkerDto
            {
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Age = 25
            };

            var departmentFromDb = new Department
            {
                Id = departmentId,
                Name = "Test Department",
                Description = "Department description"
            };

            var workerFromDb = new Worker
            {
                Id = workerId,
                FirstName = "John",
                LastName = "Doe",
                Age = 30,
                Departments = new List<Department> { departmentFromDb }
            };

            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(departmentFromDb);

            _workerRepoMock.Setup(repo => repo.Worker.GetWorkerAsync(departmentId, workerId, true))
                .ReturnsAsync(workerFromDb);

            _mapperMock.Setup(mapper => mapper.Map(updateWorkerDto, workerFromDb))
                .Callback((UpdateWorkerDto dto, Worker worker) =>
                {
                    worker.FirstName = dto.FirstName;
                    worker.LastName = dto.LastName;
                    worker.Age = dto.Age;
                });


            // Act
            var result = await _sut.UpdateWorkerForDepartment(departmentId, workerId, updateWorkerDto);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(updateWorkerDto.FirstName,workerFromDb.FirstName);
            Assert.Equal(updateWorkerDto.LastName,workerFromDb.LastName);
            Assert.Equal(updateWorkerDto.Age,workerFromDb.Age);
        }

        [Fact]
        public async Task UpdateWorkerForDepartment_ShouldReturnBadRequest_WhenUpdateWorkerDtoIsNull()
        {

            // Arrange
            UpdateWorkerDto updateWorkerDto = null;

            // Act
            var result = await _sut.UpdateWorkerForDepartment(Guid.NewGuid(), Guid.NewGuid(), updateWorkerDto);

            // Assert
            var noContentResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact] 
        private async Task UpdateWorkerForDepartment_ShouldReturnUnprocessableEntity_WhenDtoIsNotValid()
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

            var updateWorkerDto = new UpdateWorkerDto
            {
                FirstName = "Test",
                Age = 20,
            };

            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, true))
                .ReturnsAsync(departmentFromDb);
            _sut.ModelState.AddModelError("LastName", "LastName is a required field.");

            // Act
            var result = await _sut.UpdateWorkerForDepartment(departmentId, workerId, updateWorkerDto);

            // Assert
            var createdAtRouteResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

        [Fact]
        public async Task UpdateWorkerForDepartment_ShouldReturnNotFound_WhenDepartmentDoesNotExists()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var workerId = Guid.NewGuid();

            var updateWorkerDto = new UpdateWorkerDto
            {
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Age = 25
            };

            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.UpdateWorkerForDepartment(departmentId, workerId, updateWorkerDto);

            // Assert
            var noContentResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateWorkerForDepartment_ShouldReturnNotFound_WhenWorkerDoesNotExist()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var workerId = Guid.NewGuid();

            var updateWorkerDto = new UpdateWorkerDto
            {
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Age = 25
            };

            var departmentFromDb = new Department
            {
                Id = departmentId,
                Name = "Test Department",
                Description = "Department description"
            };


            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(departmentFromDb);

            _workerRepoMock.Setup(repo => repo.Worker.GetWorkerAsync(departmentId, workerId, true))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.UpdateWorkerForDepartment(departmentId, workerId, updateWorkerDto);

            // Assert
            var noContentResult = Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task PartiallyUpdateWorkerFromDepartment_ShouldReturnNoContent_WhenWorkerExists()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var workerId = Guid.NewGuid();

            var newFirstName = "UpdatedFirstName";
            var newLastName = "UpdatedLastName";
            var newAge = 25;

            var patchDoc = new JsonPatchDocument<UpdateWorkerDto>();
            patchDoc.Replace(w => w.FirstName, newFirstName);
            patchDoc.Replace(w => w.LastName, newLastName);
            patchDoc.Replace(w => w.Age, newAge);

            var departmentFromDb = new Department
            {
                Id = departmentId,
                Name = "Test Department",
                Description = "Department description"
            };

            var workerFromDb = new Worker
            {
                Id = workerId,
                FirstName = "John",
                LastName = "Doe",
                Age = 30,
                Departments = new List<Department> { departmentFromDb }
            };

            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(departmentFromDb);

            _workerRepoMock.Setup(repo => repo.Worker.GetWorkerAsync(departmentId, workerId, true))
                .ReturnsAsync(workerFromDb);

            _mapperMock.Setup(mapper => mapper.Map<UpdateWorkerDto>(workerFromDb))
                .Returns(new UpdateWorkerDto
                {
                    FirstName = workerFromDb.FirstName,
                    LastName = workerFromDb.LastName,
                    Age = workerFromDb.Age
                });

            _mapperMock.Setup(mapper => mapper.Map(It.IsAny<UpdateWorkerDto>(), workerFromDb))
              .Callback<UpdateWorkerDto, Worker>((updateDto, worker) =>
              {
                  worker.FirstName = updateDto.FirstName;
                  worker.LastName = updateDto.LastName;
                  worker.Age = updateDto.Age;
              });

            // Act
            var result = await _sut.PartiallyUpdateWorkerFromDepartment(departmentId, workerId, patchDoc);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(newFirstName, workerFromDb.FirstName);
            Assert.Equal(newLastName, workerFromDb.LastName);
            Assert.Equal(newAge, workerFromDb.Age);
        }

        [Fact]
        public async Task PartiallyUpdateWorkerFromDepartment_ShouldReturnNoContent_WhenPatchDocIsNull()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var workerId = Guid.NewGuid();

            JsonPatchDocument<UpdateWorkerDto> patchDoc = null;

            // Act
            var result = await _sut.PartiallyUpdateWorkerFromDepartment(departmentId, workerId, patchDoc);

            // Assert
            var noContentResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PartiallyUpdateWorkerFromDepartment_ShouldReturnNotFound_WhenDepartmentDoesNotEist()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var workerId = Guid.NewGuid();

            var newFirstName = "UpdatedFirstName";
            var newLastName = "UpdatedLastName";
            var newAge = 25;

            var patchDoc = new JsonPatchDocument<UpdateWorkerDto>();
            patchDoc.Replace(w => w.FirstName, newFirstName);
            patchDoc.Replace(w => w.LastName, newLastName);
            patchDoc.Replace(w => w.Age, newAge);

            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(() => null);


            // Act
            var result = await _sut.PartiallyUpdateWorkerFromDepartment(departmentId, workerId, patchDoc);

            // Assert
            var noContentResult = Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task PartiallyUpdateWorkerFromDepartment_ShouldReturnNotFound_WhenWorkerDoesNotExist()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var workerId = Guid.NewGuid();

            var newFirstName = "UpdatedFirstName";
            var newLastName = "UpdatedLastName";
            var newAge = 25;

            var patchDoc = new JsonPatchDocument<UpdateWorkerDto>();
            patchDoc.Replace(w => w.FirstName, newFirstName);
            patchDoc.Replace(w => w.LastName, newLastName);
            patchDoc.Replace(w => w.Age, newAge);

            var departmentFromDb = new Department
            {
                Id = departmentId,
                Name = "Test Department",
                Description = "Department description"
            };


            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(departmentFromDb);

            _workerRepoMock.Setup(repo => repo.Worker.GetWorkerAsync(departmentId, workerId, true))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.PartiallyUpdateWorkerFromDepartment(departmentId, workerId, patchDoc);

            // Assert
            var noContentResult = Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task PartiallyUpdateWorkerFromDepartment_ShouldReturnUnprocessableEntity_WhenModelStateIsInvalid()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var workerId = Guid.NewGuid();

            var patchDoc = new JsonPatchDocument<UpdateWorkerDto>();
            patchDoc.Replace(w => w.FirstName, "UpdatedFirstName");
            patchDoc.Replace(w => w.LastName, "UpdatedLastName");
            patchDoc.Replace(w => w.Age, -5);

            var departmentFromDb = new Department
            {
                Id = departmentId,
                Name = "Test Department",
                Description = "Department description"
            };

            var workerFromDb = new Worker
            {
                Id = workerId,
                FirstName = "John",
                LastName = "Doe",
                Age = 30,
                Departments = new List<Department> { departmentFromDb }
            };

            _workerRepoMock.Setup(repo => repo.Department.GetDepartmentAsync(departmentId, false))
                .ReturnsAsync(departmentFromDb);

            _workerRepoMock.Setup(repo => repo.Worker.GetWorkerAsync(departmentId, workerId, true))
                .ReturnsAsync(workerFromDb);

            _mapperMock.Setup(mapper => mapper.Map<UpdateWorkerDto>(workerFromDb))
                .Returns(new UpdateWorkerDto
                {
                    FirstName = workerFromDb.FirstName,
                    LastName = workerFromDb.LastName,
                    Age = workerFromDb.Age
                });

            _sut.ModelState.AddModelError("Age", "Age is required and it must be greater than 1 and less than 100");

            // Act
            var result = await _sut.PartiallyUpdateWorkerFromDepartment(departmentId, workerId, patchDoc);

            // Assert
            var unprocessableEntityResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
            var modelStateDictionary = Assert.IsAssignableFrom<SerializableError>(unprocessableEntityResult.Value);
            Assert.True(modelStateDictionary.ContainsKey("Age"));
        }
    }
}
