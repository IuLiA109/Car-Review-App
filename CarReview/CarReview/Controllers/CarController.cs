using AutoMapper;
using CarReview.Dto;
using CarReview.Interfaces;
using CarReview.Models;
using CarReview.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public CarController(ICarRepository carRepository, IMapper mapper, IReviewRepository reviewRepository)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetCars()
        {
            var cars = _mapper.Map<List<CarDto>>(_carRepository.GetCars());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cars);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Car))]
        [ProducesResponseType(400)]
        public IActionResult GetCar(int id)
        {
            if(!_carRepository.CarExists(id))
                return NotFound();

            var car = _mapper.Map<CarDto>(_carRepository.GetCar(id));

            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            return Ok(car);
        }

        [HttpGet("{id}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetCarRating(int id)
        {
            if (!_carRepository.CarExists(id))
                return NotFound();

            var rating = _carRepository.GetCarRating(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rating);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCar([FromQuery] int categoryId, [FromBody] CarDto carCreate)
        {
            if (carCreate == null)
                return BadRequest(ModelState);

            var cars = _carRepository.GetCars().Where(c => c.Brand.Trim().ToUpper() == carCreate.Brand.Trim().ToUpper() &&
            c.Model.Trim().ToUpper() == carCreate.Model.Trim().ToUpper() && c.Year == carCreate.Year).FirstOrDefault();

            if (cars != null)
            {
                ModelState.AddModelError("", "Car already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carMap = _mapper.Map<Car>(carCreate);


            if (!_carRepository.CreateCar(categoryId, carMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{carId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCar(int carId, [FromBody] CarDto updatedCar)
        {
            if(updatedCar == null)
                return BadRequest(ModelState);
            if(carId != updatedCar.Id)
                return BadRequest(ModelState);
            if(!_carRepository.CarExists(carId))
                return NotFound();
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var carMap = _mapper.Map<Car>(updatedCar);
            if (!_carRepository.UpdateCar(carMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);

            }

            return Ok("Successfully updated");
        }

        [HttpDelete("{carId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCar(int carId)
        {
            if (!_carRepository.CarExists(carId))
                return NotFound();

            var reviews = _reviewRepository.GetReviewsOfCar(carId).ToList();
            var car = _carRepository.GetCar(carId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReviews(reviews))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
            }

            if (!_carRepository.DeleteCar(car))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
            }

            return Ok("Successfully deleted");
        }

    }
}
