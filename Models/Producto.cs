//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MiPrimerAppMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Producto
    {
        public int IDPRODUCTO { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal PRECIO { get; set; }
        public string IMAGEN { get; set; }
        public int IDCATEGORIA { get; set; }
    
        public virtual Categoria Categoria { get; set; }
    }
}
