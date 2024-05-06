﻿using System;
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
                using (var connection = (new SARISEntities1()).Database.Connection)
                {

                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"EXEC sp_Requerimientos_Bandeja {1},{1},{1}";
                    using (var reader = command.ExecuteReader())
                    {
                        var db = ((IObjectContextAdapter)new SARISEntities1());
                        
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

        [HttpGet]
        public JsonResult ListarTicketCerrados()
        {
            var listaEquifaxGarantia = new List<TicketMiewModel>();

            try
            {
                using (var connection = (new SARISEntities1()).Database.Connection)
                {

                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"EXEC sp_Requerimientos_Bandeja {1},{1},{1}";
                    using (var reader = command.ExecuteReader())
                    {
                        var db = ((IObjectContextAdapter)new SARISEntities1());
                        reader.NextResult();
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

        [HttpGet]
        public ActionResult ModalBitacora(int id)
        {
            return PartialView(id);

        }

        [HttpGet]
        public JsonResult BitacoraEstado(int Ticket)
        {
            var listaEquifaxGarantia = new List<Estado_RequerimientoViewModal>();

            try
            {
                using (var connection = (new SARISEntities1()).Database.Connection)
                {

                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"EXEC sp_Requerimientos_Bitacoras_Estados {Ticket}";
                    using (var reader = command.ExecuteReader())
                    {
                        var db = ((IObjectContextAdapter)new SoporteOPEntities());
                        listaEquifaxGarantia = db.ObjectContext.Translate<Estado_RequerimientoViewModal>(reader).ToList();
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