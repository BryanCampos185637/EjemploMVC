using System;
using System.Linq;
using System.Web.Mvc;
using MiPrimerAppMVC.Models;

namespace MiPrimerAppMVC.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult listarProducto()
        {
            using (var db = new EJEMPLOMVCEntities())
            {
                var lst = db.Producto
                    .Select(p => new { p.DESCRIPCION, p.PRECIO, p.IDPRODUCTO, p.IMAGEN,p.IDCATEGORIA }).ToList();
                var json = Json(lst, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = int.MaxValue;
                return json;
            }
        }
        [HttpPost]
        public string guardarProducto(Producto producto)
        {
            try
            {
                using (var db = new EJEMPLOMVCEntities())
                {
                    int nveces = db.Producto.Where(p => p.DESCRIPCION.Equals(producto.DESCRIPCION) && p.IDCATEGORIA.Equals(producto.IDCATEGORIA) 
                    && p.IDPRODUCTO!=producto.IDPRODUCTO).Count();
                    if (nveces == 0)
                    {
                        if (producto.IDPRODUCTO == 0)
                        {
                            db.Producto.Add(producto);
                            db.SaveChanges();
                        }
                        else
                        {
                            var data = db.Producto.Where(p => p.IDPRODUCTO == producto.IDPRODUCTO).First();
                            data.DESCRIPCION = producto.DESCRIPCION;
                            data.PRECIO = producto.PRECIO;
                            data.IMAGEN = producto.IMAGEN;
                            data.IDCATEGORIA = producto.IDCATEGORIA;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        return "repetido";
                    }
                }
                return "ok";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
        [HttpGet]
        public JsonResult obtenerProductoPorId(int id)
        {
            using(var db = new EJEMPLOMVCEntities())
            {
                var data = db.Producto.Where(p => p.IDPRODUCTO.Equals(id))
                    .Select(p => new { p.DESCRIPCION, p.PRECIO, p.IDCATEGORIA, p.IMAGEN,p.IDPRODUCTO }).First();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public string eliminarProducto(int id)
        {
            try
            {
                using (var db = new EJEMPLOMVCEntities())
                {
                    var data = db.Producto.Where(p => p.IDPRODUCTO.Equals(id)).First();
                    db.Producto.Remove(data);
                    db.SaveChanges();
                }
                return "ok";

            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}