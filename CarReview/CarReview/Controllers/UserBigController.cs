using AutoMapper;
using CarReview.Dto;
using CarReview.Interfaces;
using CarReview.Models;
using CarReview.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CarReview.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserBigController : Controller
    {
        private readonly IUserBigRepository _userRepository;
        private readonly IMapper _mapper;
        public UserBigController(IUserBigRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int id)
        {
            if (!_userRepository.UserExists(id))
                return NotFound();

            var user = _mapper.Map<UserDto>(_userRepository.GetUser(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("{username}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserByUsername(string username)
        {
            if (!_userRepository.UserExistsByUsername(username))
                return NotFound();

            var user = _mapper.Map<UserDto>(_userRepository.GetUserByUsername(username));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        //crearea unui user se face in AuthController la register

        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            var user = _userRepository.GetUser(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.DeleteUser(user))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return Ok("Successfully deleted");
        }
    }
}
