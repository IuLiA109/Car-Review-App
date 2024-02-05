using AutoMapper;
using CarReview.Dto;
using CarReview.Interfaces;
using CarReview.Models;
using CarReview.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : Controller 
    {
        private readonly IUserDetailsService _userdetailsService;
        private readonly IUserBigRepository _userbigRepository;
        private readonly IMapper _mapper;
        public UserDetailsController(IUserDetailsService userdetailsService, IMapper mapper, IUserBigRepository userbigRepository)
        {
            _userdetailsService = userdetailsService;
            _mapper = mapper;
            _userbigRepository = userbigRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDetails>))]
        public IActionResult GetUsersDetails()
        {
            var usersdetails = _mapper.Map<List<UserDetailsDto>>(_userdetailsService.GetUsersDetails());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(usersdetails);
        }

        [HttpGet("{userid}")]
        [ProducesResponseType(200, Type = typeof(UserDetails))]
        [ProducesResponseType(400)]
        public IActionResult GetUserDetailsByUserId(int userid)
        {
            if (!_userbigRepository.UserExists(userid))
                return NotFound();

            var userdetails = _mapper.Map<UserDetailsDto>(_userdetailsService.GetUserDetailsByUserId(userid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userdetails);
        }



    }
}
