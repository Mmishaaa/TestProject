using AutoMapper;
using Contracts;
using Entities.DTO.Department;
using Entitties;
using Entitties.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Repository;
using TestProject.Controllers;

namespace UnitTest
{
    public class DepartmentControllerTest
    {
        private readonly DepartmentsController _departmentsController;
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly Mock<ILogger<DepartmentsController>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        public DepartmentControllerTest()
        {
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _loggerMock = new Mock<ILogger<DepartmentsController>>();
            _mapperMock = new Mock<IMapper>();
            _departmentsController = new DepartmentsController(_repositoryManagerMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetDepartments_ExistingDepartments_ReturnsDepartments()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new RepositoryContext(optionsBuilder.Options);

            var repository = new DepartmentRepository(context);
            repository.CreateDepartment(new Department
            {
                Name = "q",
                Description = "w",

            });
            context.SaveChanges();
            Assert.Single(context.Departments);
        }
    }
}
