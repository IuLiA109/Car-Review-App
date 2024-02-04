/*using AutoMapper;
using CarReview.Dto;
using CarReview.Interfaces;
using CarReview.Models;
using CarReview.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CarReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilController : Controller
    {
        private readonly IProfilRepository _profilRepository;
        public readonly IMapper _mapper;
        public ProfilController(IProfilRepository profilRepository, IMapper mapper)
        {
            _mapper = mapper;
            _profilRepository = profilRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Profil>))]
        public IActionResult GetProfiles()
        {
            var profiles = _mapper.Map<List<ProfilDto>>(_profilRepository.GetProfiles());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(profiles);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Profil))]
        [ProducesResponseType(400)]
        public IActionResult GetProfile(int id)
        {
            if (!_profilRepository.ProfileExists(id))
                return NotFound();

            var profile = _mapper.Map<ProfilDto>(_profilRepository.GetProfile(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(profile);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProfile([FromBody] ProfilDto profilCreate) 
        {
            if (profilCreate == null)
                return BadRequest(ModelState);

            var profil = _profilRepository.GetProfiles().Where(c => c.Email.Trim() == profilCreate.Email.Trim()).FirstOrDefault();

            if (profil != null)
            {
                ModelState.AddModelError("", "Profile already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var profileMap = _mapper.Map<Profil>(profilCreate);

            if (!_profilRepository.CreateProfile(profileMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

    }
}*/
