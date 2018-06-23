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
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactsController(IContactRepository contactRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var contact = _mapper.Map<ContactRequest, Contact>(request);

            _contactRepository.Add(contact);
            await _unitOfWork.Complete();

            var result = _mapper.Map<Contact, ContactResponse>(contact);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IEnumerable<ContactResponse>> GetContacts()
        {
            var contacts = await _contactRepository.List();
            return _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactResponse>>(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            var contact = await _contactRepository.Get(id);

            if (contact == null) return NotFound();

            var result = _mapper.Map<Contact, ContactResponse>(contact);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] ContactRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var contact = await _contactRepository.Get(id);
            if (contact == null) return NotFound();

            _mapper.Map<ContactRequest, Contact>(request, contact);
            await _unitOfWork.Complete();

            var result = _mapper.Map<Contact, ContactResponse>(contact);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _contactRepository.Get(id);
            if (contact == null) return NotFound();

            _contactRepository.Remove(contact);

            await _unitOfWork.Complete();

            return Ok(id);
        }
    }
}