using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace PL.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Login(string Email, string Password) {
			Dictionary<string, object> respuesta = BL.Usuario.GetUsuarioByEmail(Email);
			
			bool resultado = (bool)respuesta["Resultado"];
			if (resultado)
			{
				ML.Usuario usuario = (ML.Usuario)respuesta["Usuario"];
				if(usuario.Email != "")
				{
					string PassString = Encoding.UTF8.GetString(usuario.Password);
                    var passhash = ConvertToHash(usuario.Password.ToString());
					var passhashNueva = ConvertToHash(Password);
                    if (passhash == passhashNueva)
					{
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					ViewBag.Mensaje = "El email no existe";
					return View();
				}
			}
			else
			{
				ViewBag.Mensaje = "El email no existe";
				return View();

			}

			return View();
		}


		public static string ConvertToHash(string password) 
		{
            using (SHA256 s = SHA256.Create()) {

                byte[] bytes = s.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder b = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    b.Append(bytes[i].ToString("x2"));
                }
                return b.ToString();
            }
        }
		
	}
}
