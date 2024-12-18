using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repository.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProject()
        {
            try
            {
                return Ok(await _projectRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id) 
        {
            try
            {
                var project = await _projectRepository.GetById(id);

                if (project == null) { return NoContent();  }

                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(Project project)
        {
            try
            {
                _projectRepository.Insert(project);

                return Created("", new object { });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProject(Project project)
        {
            try
            {
                var result = await _projectRepository.Update(project);

                if (!result)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                var projectExists = await _projectRepository.GetById(id);

                if (projectExists == null)
                {
                    return NoContent();
                }

                var result = await _projectRepository.Delete(projectExists);

                if (!result)
                {
                    return BadRequest();
                }

               return Ok();    
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
