using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace PL.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult GetPopular()
        {
            Models.Movies movie = new Models.Movies();
            movie.respuesta = true;
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

                    foreach (var registro in JsonObject.results)
                    {
                        Models.Movies movieObj = new Models.Movies();
                        movieObj.Id = registro.id;
                        movieObj.Name = registro.original_title;
                        movieObj.Poster = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + registro.poster_path;
                        movieObj.GenreIds = registro.genre_ids;
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

        public IActionResult AgregarFavorito(int idPelicula, bool r)
        {
			Dictionary<string, object> result = new Dictionary<string, object>();
			using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                var parametrosPeticion = new { media_type="movie", media_id= idPelicula, favorite= r };
                var responseTask = client.PostAsJsonAsync("account/20961223/favorite?api_key=3af46a9ee44140a24fc4baaabbbf4eb5&session_id=116d8eef3015aa569eeb35910d5aa0ed021e51e8", parametrosPeticion);
                responseTask.Wait();
                var respuesta = responseTask.Result;
                if (respuesta.IsSuccessStatusCode)
                {
					var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
					readTask.Wait();
					result = readTask.Result;
				}
            }

            return View();
        }

		public IActionResult QuitarFavorito(int idPelicula)
		{
			Dictionary<string, object> result = new Dictionary<string, object>();
			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
				var parametrosPeticion = new { media_type = "movie", media_id = idPelicula, favorite = false };
				var responseTask = client.PostAsJsonAsync("account/20961223/favorite?api_key=3af46a9ee44140a24fc4baaabbbf4eb5&session_id=116d8eef3015aa569eeb35910d5aa0ed021e51e8", parametrosPeticion);
				responseTask.Wait();
				var respuesta = responseTask.Result;
				if (respuesta.IsSuccessStatusCode)
				{
					var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
					readTask.Wait();
					result = readTask.Result;
				}
			}

			return View();
		}

		public IActionResult ListaFavoritos()
        {
            Models.Movies movie = new Models.Movies();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                var responseTask = client.GetAsync("account/20961223/favorite/movies?api_key=3af46a9ee44140a24fc4baaabbbf4eb5&session_id=116d8eef3015aa569eeb35910d5aa0ed021e51e8");
                responseTask.Wait();
                var respuesta = responseTask.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    var readTask = respuesta.Content.ReadAsStringAsync();
                    readTask.Wait();
                    movie.ListMovies = new List<object>();

                    dynamic jsonObject = JObject.Parse(readTask.Result);
                    foreach(var registro in jsonObject.results)
                    {
                        Models.Movies movieObj = new Models.Movies();
						movieObj.Id = registro.id;
						movieObj.Name = registro.original_title;
						movieObj.Poster = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + registro.poster_path;
						movieObj.GenreIds = registro.genre_ids;
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
