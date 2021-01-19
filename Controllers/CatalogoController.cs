using MiPrimerAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiPrimerAppMVC.Controllers
{
    public class CatalogoController : Controller
    {
        // GET: Catalogo
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult listarProducto()
        {
            using (var db = new EJEMPLOMVCEntities())
            {
                var lst = (from producto in db.Producto
                           join categoria in db.Categoria
                           on producto.IDCATEGORIA equals categoria.IDCATEGORIA
                           select new LstProducto
                           {
                               IDPRODUCTO = producto.IDPRODUCTO,
                               DESCRIPCION = producto.DESCRIPCION,
                               PRECIO = producto.PRECIO,
                               IMAGEN = producto.IMAGEN,
                               NOMBRECATEGORIA = categoria.NOMBRE
                           }).ToList();
                var json = Json(lst, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = int.MaxValue;
                return json;
            }
        }
    }
    public class LstProducto 
    {
        public int IDPRODUCTO { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal PRECIO { get; set; }
        public string IMAGEN { get; set; }
        public int IDCATEGORIA { get; set; }
        public string NOMBRECATEGORIA { get; set; }
    }
}