using System;
using System.Collections.Generic;

namespace DL;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public decimal? PrecioProducto { get; set; }

    public string? Tamaño { get; set; }

    public string? Sabor { get; set; }

    public int? IdCategoria { get; set; }

    public int? IdProveedor { get; set; }

    public virtual Categorium? IdCategoriaNavigation { get; set; }

    public virtual Proveedor? IdProveedorNavigation { get; set; }
}
