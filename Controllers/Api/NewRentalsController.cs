using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;
using WebApplication2.Dtos;


namespace WebApplication2.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        public ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            /*
            if (newRental.MovieIds.Count == 0)
                return BadRequest("No MovieIds have been given.");
            */
            
            var customer = _context.Customer.Single(c => c.Id == newRental.CustomerId);
            /*
            if (customer == null)
                return BadRequest("CustomerId is not valid.");
            */
            // SELECT * FROM MOVIES WHERE ID IN(1, 2, 3)
            var movies = _context.Movie.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();

            /*
            if (movies.Count != newRental.MovieIds.Count)
                return BadRequest("One or more MovieIds are invalid."); 
            */

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie not available.");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rental.Add(rental);
            }

            _context.SaveChanges();

            throw new NotImplementedException();
        }
    }
}
