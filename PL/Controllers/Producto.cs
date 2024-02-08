using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
	public class Producto : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			Dictionary<string,object> respuesta = BL.Producto.GetAll();
			bool resultado = (bool)respuesta["Resultado"];
			string mensaje = (string)respuesta["Mensaje"];
			if (resultado)
			{
				ML.Producto producto = (ML.Producto)respuesta["Producto"];

				return View(producto);
			}
			else
			{
				ViewBag.Mensaje = mensaje;
				return PartialView("Modal");
			}
		}
	}
}
