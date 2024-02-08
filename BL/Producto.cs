using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
	public class Producto
	{
		public static Dictionary<string,object> GetAll()
		{
			ML.Producto producto = new ML.Producto();
			Dictionary<string,object>Diccionario = new Dictionary<string, object> { {"Producto", producto },{"Resultado", false },{"Mensaje","" } };
			try
			{
				using (DL.LhernandezGallegosMoviesContext context = new DL.LhernandezGallegosMoviesContext())
				{
					var query = (from Productos in context.Productos
								 join Provedores in context.Proveedors on Productos.IdProveedor equals Provedores.IdProveedor
								 join Categorias in context.Categoria on Productos.IdCategoria equals Categorias.IdCategoria
								 select new
								 {
									 IdProducto = Productos.IdProducto,
									 NombreProducto = Productos.Nombre,
									 Precio = Productos.PrecioProducto,
									 Tamaño = Productos.Tamaño,
									 Sabor = Productos.Sabor,
									 IdProveedor = Provedores.IdProveedor,
									 NombreProveedor = Provedores.Nombre,
									 IdCategoria = Categorias.IdCategoria,
									 NombreCategoria = Categorias.Nombre
								 }
					).ToList();

					if ( query != null)
					{
						producto.Productos = new List<object>();
						foreach ( var item in query ) 
						{ 
							ML.Producto p = new ML.Producto();
							p.IdProducto = item.IdProducto;
							p.Nombre = item.NombreProducto;
							p.PrecioProducto = item.Precio.Value;
							p.Tamaño = item.Tamaño;
							p.Proveedor = new ML.Proveedor();
							p.Proveedor.IdProveedor = item.IdProveedor;
							p.Proveedor.Nombre = item.NombreProveedor;
							p.Categoria = new ML.Categoria();
							p.Categoria.IdCategoria = item.IdCategoria;
							p.Categoria.Nombre = item.NombreCategoria;
							producto.Productos.Add( p );
						}

						Diccionario["Producto"] = producto;
						Diccionario["Resultado"] = true;
						Diccionario["Mensaje"] = "Se han recuperado tus datos";
					}
					else
					{
						Diccionario["Resultado"] = false;
						Diccionario["Mensaje"] = "No se han recuperado tus datos";
					}
				}
			}
			catch(Exception ex)
			{
				Diccionario["Resultado"] = false;
				Diccionario["Mensaje"] = "No se han recuperado tus datos" + ex;
			}
			
			return Diccionario;
		}
	}
}
