using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SWP391.Models;

namespace SWP391.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly motelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BookingController(
           motelContext context,
           IWebHostEnvironment hostEnvironment
           )
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        [Route("takeBooking")]

        public IActionResult TakeBooking()
        {
            return Ok(_context.Users.ToList());
        }
    }
}
