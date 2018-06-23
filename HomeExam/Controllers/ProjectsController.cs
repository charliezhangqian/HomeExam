using AutoMapper;
using HomeExam.Controllers.Request;
using HomeExam.Controllers.Response;
using HomeExam.Core;
using HomeExam.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectsController(IProjectRepository projectRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectResponse>> GetProjects()
        {
            var projects = await _projectRepository.List();
            return _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectResponse>>(projects);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var project = _mapper.Map<ProjectRequest, Project>(request);
            _projectRepository.Add(project);
            await _unitOfWork.Complete();

            project = await _projectRepository.Get(project.Id);

            var result = _mapper.Map<Project, ProjectResponse>(project);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _projectRepository.Get(id);

            if (project == null) return NotFound();

            var result = _mapper.Map<Project, ProjectResponse>(project);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var project = await _projectRepository.Get(id);
            if (project == null) return NotFound();

            _mapper.Map<ProjectRequest, Project>(request, project);

            await _unitOfWork.Complete();

            project = await _projectRepository.Get(id);

            var result = _mapper.Map<Project, ProjectResponse>(project);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectRepository.Get(id);
            if (project == null) return NotFound();

            _projectRepository.Remove(project);
            await _unitOfWork.Complete();

            return Ok(id);
        }
    }
}