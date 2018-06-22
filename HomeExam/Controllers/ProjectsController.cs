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
        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _projectRepository.List();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(request);

            var project = _mapper.Map<ProjectRequest, Project>(request);
            _projectRepository.Add(project);
            await _unitOfWork.Complete();

            var result = _mapper.Map<Project, ProjectResponse>(project);

            return Ok(result);
        }
    }
}