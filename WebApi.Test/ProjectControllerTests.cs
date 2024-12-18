using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using WebApi.Repository.Interfaces;
using WebApi.Models;
using Moq;
using Xunit;

namespace WebApi.Test
{
    public class ProjectControllerTests
    {
        private readonly Mock<IProjectRepository> _mockRepo;
        private readonly ProjectsController _controller;

        public ProjectControllerTests()
        {
            _mockRepo = new Mock<IProjectRepository>();
            _controller = new ProjectsController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllProject_ReturnsOkResult_WhenProjectsExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Project> { new Project(), new Project() });

            // Act
            var result = await _controller.GetAllProject();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<Project>>(okResult.Value);
        }

        [Fact]
        public async Task GetAllProject_ReturnsBadRequestResult_WhenExceptionThrown()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAll()).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetAllProject();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetProjectById_ReturnsOkResult_WhenProjectExists()
        {
            // Arrange
            var projectId = 1;
            _mockRepo.Setup(repo => repo.GetById(projectId)).ReturnsAsync(new Project());

            // Act
            var result = await _controller.GetProjectById(projectId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Project>(okResult.Value);
        }

        [Fact]
        public async Task GetProjectById_ReturnsNoContent_WhenProjectNotFound()
        {
            // Arrange
            var projectId = 1;
            _mockRepo.Setup(repo => repo.GetById(projectId)).ReturnsAsync((Project)null);

            // Act
            var result = await _controller.GetProjectById(projectId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetProjectById_ReturnsBadRequestResult_WhenExceptionThrown()
        {
            // Arrange
            var projectId = 1;
            _mockRepo.Setup(repo => repo.GetById(projectId)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetProjectById(projectId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateProject_ReturnsCreatedResult_WhenProjectIsCreated()
        {
            // Arrange
            var project = new Project();
            _mockRepo.Setup(repo => repo.Insert(project)).Verifiable();

            // Act
            var result = await _controller.CreateProject(project);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal("", createdResult.Location);
        }

        [Fact]
        public async Task CreateProject_ReturnsBadRequestResult_WhenExceptionThrown()
        {
            // Arrange
            var project = new Project();
            _mockRepo.Setup(repo => repo.Insert(project)).Throws(new Exception());

            // Act
            var result = await _controller.CreateProject(project);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateProject_ReturnsOkResult_WhenProjectIsUpdatedSuccessfully()
        {
            // Arrange
            var project = new Project();
            _mockRepo.Setup(repo => repo.Update(project)).ReturnsAsync(true); 

            // Act
            var result = await _controller.UpdateProject(project);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateProject_ReturnsBadRequestResult_WhenUpdateFails()
        {
            // Arrange
            var project = new Project();
            _mockRepo.Setup(repo => repo.Update(project)).ReturnsAsync(false); 

            // Act
            var result = await _controller.UpdateProject(project);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateProject_ReturnsBadRequestResult_WhenExceptionThrown()
        {
            // Arrange
            var project = new Project();
            _mockRepo.Setup(repo => repo.Update(project)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.UpdateProject(project);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteProject_ReturnsOkResult_WhenProjectIsDeletedSuccessfully()
        {
            // Arrange
            var projectId = 1;
            var project = new Project();
            _mockRepo.Setup(repo => repo.GetById(projectId)).ReturnsAsync(project);
            _mockRepo.Setup(repo => repo.Delete(project)).ReturnsAsync(true); 

            // Act
            var result = await _controller.DeleteProject(projectId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteProject_ReturnsNoContent_WhenProjectNotFound()
        {
            // Arrange
            var projectId = 1;
            _mockRepo.Setup(repo => repo.GetById(projectId)).ReturnsAsync((Project)null);

            // Act
            var result = await _controller.DeleteProject(projectId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteProject_ReturnsBadRequestResult_WhenDeleteFails()
        {
            // Arrange
            var projectId = 1;
            var project = new Project();
            _mockRepo.Setup(repo => repo.GetById(projectId)).ReturnsAsync(project);
            _mockRepo.Setup(repo => repo.Delete(project)).ReturnsAsync(false); 

            // Act
            var result = await _controller.DeleteProject(projectId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteProject_ReturnsBadRequestResult_WhenExceptionThrown()
        {
            // Arrange
            var projectId = 1;
            _mockRepo.Setup(repo => repo.GetById(projectId)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.DeleteProject(projectId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
