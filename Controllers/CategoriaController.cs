using MiPrimerAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiPrimerAppMVC.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public JsonResult lstCategoria()
        {
            try
            {
                using (var db = new EJEMPLOMVCEntities())
                {
                    var lst = db.Categoria.Select(p => new { p.NOMBRE, p.IDCATEGORIA }).ToList();
                    return Json(lst, JsonRequestBehavior.AllowGet);
                }   
            }
            catch(Exception e)
            {
                return null;
            }
        }

        [HttpGet]
        public JsonResult obtenerCategoriaPorId(int id)
        {
            using (var db = new EJEMPLOMVCEntities())
            {
                var dta = db.Categoria.Where(y => y.IDCATEGORIA.Equals(id))
                    .Select(p => new { p.NOMBRE, p.IDCATEGORIA }).First();
                return Json(dta, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public string eliminarCategoria(int id)
        {
            try
            {
                using (var db = new EJEMPLOMVCEntities())
                {
                    var dta = db.Categoria.Where(y => y.IDCATEGORIA.Equals(id)).First();
                    db.Categoria.Remove(dta);
                    db.SaveChanges();
                    return "ok";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpPost]
        public string guardarCategoria(Categoria categoria)
        {
            try
            {
                using (var db = new EJEMPLOMVCEntities())
                {
                    var nveces = db.Categoria.Where(p => p.NOMBRE.Equals(categoria.NOMBRE) && p.IDCATEGORIA != categoria.IDCATEGORIA).Count();
                    if (nveces > 0)
                    {
                        return "repetido";
                    }
                    else
                    {
                        if (categoria.IDCATEGORIA == 0)
                        {
                            db.Categoria.Add(categoria);
                            db.SaveChanges();
                        }
                        else
                        {
                            var data = db.Categoria.Where(x => x.IDCATEGORIA.Equals(categoria.IDCATEGORIA)).First();
                            data.NOMBRE = categoria.NOMBRE;
                            db.SaveChanges();
                        }
                        return "ok";
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}