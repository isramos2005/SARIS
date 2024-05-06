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
        public ActionResult ActualizarTicket(int idticket)
        {
            using (var contexto = new SARISEntities1())
            {
                try
                {

                    var cont = contexto.sp_Requerimiento_Maestro_Detalle(1, 1, 1, idticket).FirstOrDefault();
                    var tick = new TicketMiewModel();
                    tick.fcDescripcionRequerimiento = cont.fcDescripcionRequerimiento;
                    tick.fiIDRequerimiento = cont.fiIDRequerimiento;
                    tick.fdFechaCreacion = cont.fdFechaCreacion;
                    tick.fcTituloRequerimiento = cont.fcTituloRequerimiento;
                    tick.fiIDAreaSolicitante = cont.fiIDAreaSolicitante;
                    tick.fiIDEstadoRequerimiento = cont.fiIDEstadoRequerimiento;
                    tick.fiIDUsuarioAsignado = cont.fiIDUsuarioAsignado;
                    tick.fdFechaAsignacion = cont.fdFechaAsignacion;
                    tick.fdFechadeCierre = cont.fdFechadeCierre;
                    
                    ViewBag.ListarArea = contexto.sp_Areas_Lista().Select(x => new SelectListItem { Value = x.fiIDArea.ToString(), Text = x.fcDescripcion}).ToList();
                    ViewBag.Estados = contexto.sp_Estados_Lista().Select(x => new SelectListItem { Value = x.fiIDEstado.ToString(), Text = x.fcDescripcionEstado }).ToList();
                    ViewBag.Usuario = contexto.sp_Usuarios_Maestro_PorIdUsuarioSupervisor(1).Select(x => new SelectListItem { Value = x.fiIDUsuario.ToString(), Text = x.fcPrimerNombre + " " + x.fcPrimerApellido }).ToList();
                    ViewBag.idticket = idticket;
                    return PartialView(tick);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        [HttpPost]
        public JsonResult GuardarTicket(TicketMiewModel ticket)
        {

            try
            {
                using (var contexto = new SARISEntities1()) {

                    try 
                    {
                        //cambiar despues Los datos que se envian en duro para que sea mas dinamico las cosas 
                        var save = contexto.sp_Requerimiento_Alta(1, 1, 1, ticket.fcTituloRequerimiento, ticket.fcDescripcionRequerimiento, ticket.fiIDEstadoRequerimiento,1);


                        return EnviarResultado(true, "", "Ticket guardado exitosamente");

                    }
                    catch (Exception ex)
                    {
                        return EnviarResultado(false, ex.Message.ToString(), "No se pudo registrar el ticket");
                        throw;
                    }
                    

                }
            }
            catch (Exception ex)
            {
                return EnviarResultado(false, ex.Message.ToString(), "No se pudo registrar el ticket");
            }
        }


        public ActionResult BandejaTicket()
        {
            return View();
        }

        public ActionResult RegistrarTicket()
        {
            return PartialView();
        }

        public JsonResult ActualizarDatos(TicketMiewModel ticket)
        {
            //var resultado = new Resultado_ViewModel() { ResultadoExitoso = false };
            //var mensajeError = string.Empty;
            //var estado = Requerimiento.fiIDEstadoRequerimiento;
            try
            {
                using (var contexto = new SARISEntities1())
                {
                    var actua = contexto.sp_Requerimiento_Maestro_Actualizar(1, 1, ticket.fcTituloRequerimiento, ticket.fcDescripcionRequerimiento, ticket.fiIDEstadoRequerimiento, DateTime.Now, ticket.fiIDUsuarioAsignado, ticket.fiTiempodeDesarrollo, ticket.fiTipoRequerimiento, 1);
                    return EnviarResultado(true, "", "Ticket Actualizado exitosamente");
                }
            }
            catch (Exception ex)
            {
                return EnviarResultado(false, ex.Message.ToString(), "No se pudo Actualizar el ticket");
                throw;
            }
        }


    }
}