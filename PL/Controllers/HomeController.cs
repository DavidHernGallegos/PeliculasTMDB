using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PL.Models;
using System.Diagnostics;
using System.Net;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetPopular()
        {
            Models.Movies movie = new Models.Movies();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                var responseTask = client.GetAsync("movie/popular?api_key=3af46a9ee44140a24fc4baaabbbf4eb5");
                responseTask.Wait();
                var respuesta = responseTask.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    var readTask = respuesta.Content.ReadAsStringAsync();
                    readTask.Wait();

                    movie.ListMovies = new List<object>();
                    dynamic JsonObject = JObject.Parse(readTask.Result);

                    foreach(var registro in JsonObject.results)
                    {
                        Models.Movies movieObj = new Models.Movies();
                        movieObj.Id = registro.id;
                        movieObj.Name = registro.original_title;
                        movieObj.Poster = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + registro.poster_path;
                        movie.ListMovies.Add(movieObj);
                    }
                  

                }
                else
                {
                    return View();
                }

                return View(movie);


            }
        }
    }
}