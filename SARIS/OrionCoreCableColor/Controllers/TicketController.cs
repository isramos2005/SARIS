using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using OrionCoreCableColor.DbConnection;
using OrionCoreCableColor.Models.Ticket;

namespace OrionCoreCableColor.Controllers
{
    public class TicketController : BaseController
    {
        // GET: Ticket
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult ListarTicket()
        {            
            var listaEquifaxGarantia = new List<TicketMiewModel>();

            try
            {
                using (var connection = (new SoporteOPEntities()).Database.Connection)
                {

                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"EXEC sp_BandejaRequerimiento {1},{1},{1}";
                    using (var reader = command.ExecuteReader())
                    {
                        var db = ((IObjectContextAdapter)new SoporteOPEntities());
                        
                        listaEquifaxGarantia = db.ObjectContext.Translate<TicketMiewModel>(reader).ToList();
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