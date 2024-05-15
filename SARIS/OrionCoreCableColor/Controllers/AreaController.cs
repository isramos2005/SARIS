using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using OrionCoreCableColor.DbConnection.CMContext;
using OrionCoreCableColor.Models;
using OrionCoreCableColor.Models.Usuario;
using System.Data.Entity;
using System.Threading.Tasks;
using OrionCoreCableColor.DbConnection;
using OrionCoreCableColor.Models.Areas;

namespace OrionCoreCableColor.Controllers
{
    public class AreaController : BaseController
    {
        // GET: Area
        public ActionResult Index()
        {
            return View();
        }



        public JsonResult CargarListaAreas()
        {
            try
            {
                using (var context = new SARISEntities1())
                {
                    var jsonResult = Json(context.sp_Areas_Lista().Select(x => new ListaAreasViewModel
                    {
                       fiIDArea = x.fiIDArea,
                       fcDescripcion  = x.fcDescripcion,
                       fcCorreoElectronico = x.fcCorreoElectronico,
                       fcNombreCorto = x.fcNombreCorto,
                       fiIDUsuarioResponsable = x.fiIDUsuarioResponsable


                    }).ToList(), JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = Int32.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        [HttpGet]
        public ActionResult Crear()
        {

            using (var contextSaris = new SARISEntities1())
            {

                ViewBag.ListaUsuarios = contextSaris.sp_Usuarios_Maestro_Lista().ToList().Select(x => new SelectListItem { Value = x.fiIDUsuario.ToString(), Text = $"{x.fcNombreCorto} - {x.fcPuesto}"}).ToList();
                return PartialView(new AreasCrearViewModel());
            }
        }



        [HttpPost]
        public ActionResult Crear(AreasCrearViewModel model)
        {
            using (var context = new SARISEntities1())
            {
                var newModel = context.sp_Areas_Insertar( model.fcDescripcion, model.fcCorreoElectronico, model.fiIDUsuarioResponsable);

                return EnviarResultado(true, "Editar Rol", "Se Creó Satisfactoriamente");

            }


        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            using (var context = new SARISEntities1())
            {
                var area = context.sp_Areas_Lista().FirstOrDefault(x => x.fiIDArea == id);
                if (area != null)
                {
                    ViewBag.ListaUsuarios = context.sp_Usuarios_Maestro_Lista().ToList().Select(x => new SelectListItem { Value = x.fiIDUsuario.ToString(), Text = $"{x.fcNombreCorto}  {x.fcPuesto}" }).ToList();

                    return PartialView("Crear", new AreasCrearViewModel { fiIDArea = area.fiIDArea, fcDescripcion = area.fcDescripcion, fcCorreoElectronico = area.fcCorreoElectronico, fcNombreCorto = area.fcNombreCorto, fiIDUsuarioResponsable = area.fiIDUsuarioResponsable , EsEditar = true});
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        public ActionResult Editar(AreasCrearViewModel model)
        {
            using (var context = new SARISEntities1())
            {
                var newModel = context.sp_Areas_Editar(model.fiIDArea, model.fcDescripcion, model.fcCorreoElectronico, model.fiIDUsuarioResponsable);
                var result = context.SaveChanges() > 0;

                return EnviarResultado(true, "Editar Rol", "Se edito Satisfactoriamente" );

            }

        }


        public ActionResult EliminarArea(int id)
        {
            using (var context = new SARISEntities1())
            {
                var area = context.sp_Areas_Desactivar(id);
                return EnviarResultado(true, "Eliminar Rol");
            }
        }

    }
}