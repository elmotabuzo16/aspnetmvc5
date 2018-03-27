using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModel;
using System.Data.Entity.Validation;



namespace WebApplication2.Controllers
{
	public class MoviesController : Controller
	{

		private ApplicationDbContext _context;

		public MoviesController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET: Movies
		public ActionResult Random()
		{
			var movie = new Movie() { Name = "Elmo" };

			var customer = new List<Customer>
			{
				new Customer { Name = "Elmo" },
				new Customer { Name = "Leslie" }
			};

			var viewModel = new RandomMovieViewModel
			{
				Movie = movie,
				Customer = customer
			};

			return View(viewModel);
		}

		public ActionResult Released(int id)
		{
			return Content("id = " + id);
		}

		[Route("movies/released/{year}/{month}")]
		public ActionResult ByReleaseYear(int year, int month)
		{
			return Content(year + " / " + month);
		}

		/*
		// ? means nullable type
		public ActionResult Index(int? pageIndex, string sortBy)
		{
			if (!pageIndex.HasValue)
			{
				pageIndex = 1;
			}

			if (String.IsNullOrWhiteSpace(sortBy))
			{
				sortBy = "default-name";
			}

			return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));

			// /movies?pageIndex=1
			// /movies?pageIndex=1&sortBy=elmo
		}
		*/

		
		public ViewResult Index()
		{
			if (User.IsInRole(RoleName.CanManageMovies))
			{
				return View("List");
			}

			return View("ReadOnlyList");
		}

		public ActionResult Details(int id)
		{
			var movies = _context.Movie.Include(m => m.Genre).SingleOrDefault(c => c.Id == id);

			if (movies == null)
			{
				return HttpNotFound();
			}

			return View(movies);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(Movie movie)
		{
			if (!ModelState.IsValid)
			{
				var viewModel = new MoviesFormViewModel(movie)
				{
					Genre = _context.Genre.ToList()
				};

				return View("MoviesForm", viewModel);
			}

			if (movie.Id == 0)
			{
				_context.Movie.Add(movie);
			}
			else
			{
				var movieInDb = _context.Movie.Single(c => c.Id == movie.Id);

				movieInDb.Name = movie.Name;
				movieInDb.ReleaseDate = movie.ReleaseDate;
				movieInDb.GenreId = movie.GenreId;
				movieInDb.NumberInStock = movie.NumberInStock;
			}

			try
			{
				_context.SaveChanges();
			}
			catch (DbEntityValidationException e)
			{
				Console.WriteLine(e);
			}
			

			return RedirectToAction("Index", "Movies");
		}


		public ActionResult Edit(int id)
		{
			var movie = _context.Movie.SingleOrDefault(m => m.Id == id);

			if (movie == null)
			{
				return HttpNotFound();
			}

			var viewModel = new MoviesFormViewModel(movie)
			{
				Genre = _context.Genre.ToList()
			};

			return View("MoviesForm", viewModel);
		}


		public ActionResult New()
		{
			var genre = _context.Genre.ToList();

			var viewModel = new MoviesFormViewModel
			{
				Genre = genre
			};
			
			return View("MoviesForm", viewModel);
		}
	}
}