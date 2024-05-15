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
        public JsonResult ListaPersonal(DateTime Fecha)
        {
            var listaEquifaxGarantia = new List<DashboardViewModel>();

            try
            {
                using (var connection = (new SARISEntities1()).Database.Connection)
                {

                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"EXEC sp_Requerimiento_Indicadores {1},'{Fecha.ToString("yyyy-MM-dd")}'";
                    using (var reader = command.ExecuteReader())
                    {
                        var db = ((IObjectContextAdapter)new SARISEntities1());
                        reader.NextResult();
                        reader.NextResult();
                        listaEquifaxGarantia = db.ObjectContext.Translate<DashboardViewModel>(reader).ToList();
                    }

                    connection.Close();

                    return EnviarListaJson(listaEquifaxGarantia);
                }
            }
            catch (Exception e)
            {

                throw;
            }


        }


        [HttpGet]
        public JsonResult ProcesosIndicador(DateTime Fecha)
        {
            var listaEquifaxGarantia = new List<Requerimiento_Detalle_Hora_ViewModel>();

            try
            {
                using (var connection = (new SARISEntities1()).Database.Connection)
                {

                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"EXEC sp_Requerimiento_Indicadores {1},'{Fecha.ToString("yyyy-MM-dd")}'";
                    using (var reader = command.ExecuteReader())
                    {
                        var db = ((IObjectContextAdapter)new SARISEntities1());
                        listaEquifaxGarantia = db.ObjectContext.Translate<Requerimiento_Detalle_Hora_ViewModel>(reader).ToList();
                    }

                    connection.Close();

                    return EnviarListaJson(listaEquifaxGarantia);
                }
            }
            catch (Exception e)
            {

                throw;
            }


        }


        [HttpGet]
        public JsonResult ProcesosIndicadorGrafica(DateTime Fecha)
        {
            var listaEquifaxGarantia = new List<Indicadores_ViewModel>();

            try
            {
                using (var connection = (new SARISEntities1()).Database.Connection)
                {

                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"EXEC sp_Requerimiento_Indicadores {1},'{Fecha.ToString("yyyy-MM-dd")}'";
                    using (var reader = command.ExecuteReader())
                    {
                        var db = ((IObjectContextAdapter)new SARISEntities1());
                        reader.NextResult();
                        listaEquifaxGarantia = db.ObjectContext.Translate<Indicadores_ViewModel>(reader).ToList();
                    }

                    connection.Close();

                    return EnviarListaJson(listaEquifaxGarantia);
                }
            }
            catch (Exception e)
            {

                throw;
            }


        }


    }
}