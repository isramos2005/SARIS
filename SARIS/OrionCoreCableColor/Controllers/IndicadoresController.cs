using OrionCoreCableColor.DbConnection;
using OrionCoreCableColor.Models.Indicadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrionCoreCableColor.Controllers
{
    public class IndicadoresController : BaseController
    {
        // GET: Indicadores
        public ActionResult Index()
        {
            return View();
        }




        public JsonResult CargarListaIndicadores()
        {
            try
            {
                using (var context = new SARISEntities1())
                {
                    var jsonResult = Json(context.sp_Indicadores_Lista().Select(x => new ListaIndicadoresViewModel
                    {
                        fiIDTipoRequerimiento = x.fiIDTipoRequerimiento,
                        fcTipoRequerimiento = x.fcTipoRequerimiento,
                        fcToken = x.fcToken


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
            return PartialView(new IndicadoresCrearViewModel());
        }



        [HttpPost]
        public ActionResult Crear(IndicadoresCrearViewModel model)
        {
            using (var context = new SARISEntities1())
            {
                var newModel = context.sp_Indicadores_Insertar(model.fcTipoRequerimiento.Trim());

                Console.WriteLine(newModel);

                return EnviarResultado(true, "Crear Indicador", "Se Creó Satisfactoriamente");

            }


        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            using (var context = new SARISEntities1())
            {
                var indicador = context.sp_Indicadores_Lista().FirstOrDefault(x => x.fiIDTipoRequerimiento == id);
                if (indicador != null)
                {
                    return PartialView("Crear", new IndicadoresCrearViewModel { fiIDTipoRequerimiento = indicador.fiIDTipoRequerimiento, fcTipoRequerimiento = indicador.fcTipoRequerimiento.Trim() , EsEditar = true});

                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        public ActionResult Editar(IndicadoresCrearViewModel model)
        {
            using (var context = new SARISEntities1())
            {
                var indicador = context.sp_Indicadores_Lista().FirstOrDefault(x => x.fcTipoRequerimiento == model.fcTipoRequerimiento && x.fiIDTipoRequerimiento != model.fiIDTipoRequerimiento);

                var newModel = context.sp_Indicadores_Editar(model.fiIDTipoRequerimiento, model.fcTipoRequerimiento.Trim());
                var result = context.SaveChanges() > 0;

                return EnviarResultado(true, "Editar Indicador", "Se edito Satisfactoriamente");

            }

        }

        public ActionResult EliminarIndicador(int id)
        {
            using (var context = new SARISEntities1())
            {
                var area = context.sp_Indicadores_Desactivar(id);
                return EnviarResultado(true, "Eliminar Indicador");
            }
        }

    }
}