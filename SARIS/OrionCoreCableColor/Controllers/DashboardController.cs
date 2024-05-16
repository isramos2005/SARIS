using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using OrionCoreCableColor.DbConnection;
using OrionCoreCableColor.Models.DashBoard;

namespace OrionCoreCableColor.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Indicadores()
        {
            var Indicadores = new DashboardViewModel();

            try
            {
                using (var connection = (new SARISEntities1()).Database.Connection)
                {

                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"EXEC sp_DashboardGlobal";
                    using (var reader = command.ExecuteReader())
                    {
                        var db = ((IObjectContextAdapter)new SARISEntities1());
                        Indicadores.BulletViewModel = db.ObjectContext.Translate<IndicadoresViewModel>(reader).ToList();
                        reader.NextResult();
                        Indicadores.AreasIncidentes = db.ObjectContext.Translate<AreasIncidentesViewModel>(reader).ToList();
                        reader.NextResult();
                        Indicadores.AreasTiempo = db.ObjectContext.Translate<AreaTiempoViewModel>(reader).ToList();
                        reader.NextResult();
                        Indicadores.ConteoIncidentes = db.ObjectContext.Translate<ConteoIncidenteViewModel>(reader).ToList();
                    }

                    connection.Close();

                    return EnviarListaJson(Indicadores);
                }
            }
            catch (Exception e)
            {

                throw;
            }


        }


        


    }
}