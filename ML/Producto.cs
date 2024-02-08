using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
	public class Producto
	{
		public int IdProducto { get; set; }

		public string Nombre { get; set; }

		public decimal PrecioProducto { get; set; }

		public string Tamaño { get; set; }

		public string Sabor { get; set; }

		public ML.Categoria Categoria { get; set; }

		public ML.Proveedor  Proveedor { get; set; }

		public List<object> Productos { get; set; }

		
	}
}
