using System.Net.Http.Headers;

namespace BL
{
	public class Usuario
	{
		public static Dictionary<string,object> GetUsuarioByEmail(string email)
		{
			ML.Usuario usuario = new ML.Usuario();
			Dictionary<string, object> Diccionario = new Dictionary<string, object> { { "Usuario", usuario }, { "Resultado", false },{"Mensaje","" } };
			try
			{
				using (DL.LhernandezGallegosMoviesContext context = new DL.LhernandezGallegosMoviesContext())
				{
					var query = (from Usuario in context.Usuarios
								 join Rol in context.Rols on Usuario.IdRol equals Rol.IdRol
								 where Usuario.Email == email
								 select new
								 {
									 IdUsuario = Usuario.IdUsuario,
									 UserName = Usuario.UserName,
									 Email = Usuario.Email,
									 Password = Usuario.Password
								 }
								 ).SingleOrDefault();

					if ( query != null )
					{
						
						usuario.IdUsuario = query.IdUsuario;
						usuario.UserName = query.UserName;
						usuario.Email = query.Email;
						usuario.Password = query.Password;

						Diccionario["Usuario"] = usuario;
						Diccionario["Resultado"] = true;
						Diccionario["Mensaje"] = "Se han obtenido los datos";

					}
					else
					{
						Diccionario["Resultado"] = false;
						Diccionario["Mensaje"] = "No se han obtenido los datos";
					}
				}

			}
			catch(Exception e)
			{
				Diccionario["Resultado"] = false;
				Diccionario["Mensaje"] = "No se han obtenido los datos" + e;
			}

			return Diccionario;
		}

		
	}
}