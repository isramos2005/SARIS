using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using OrionCoreCableColor.DbConnection;
using OrionCoreCableColor.Models.Ticket;
using System.IO;
using OrionCoreCableColor.App_Helper;

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
                    command.CommandText = $"EXEC sp_Requerimientos_Bandeja {1},{1},{GetIdUser()}";
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
                    command.CommandText = $"EXEC sp_Requerimientos_Bandeja {1},{1},{GetIdUser()}";
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

        public ActionResult ActualizarTicket(int idticket)
        {
            using (var contexto = new SARISEntities1())
            {
                try
                {

                    var cont = contexto.sp_Requerimiento_Maestro_Detalle(1, 1, GetIdUser(), idticket).FirstOrDefault();
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
                        var idarea = (ticket.fiAreaAsignada == 0) ? 6 : ticket.fiAreaAsignada; // aqui decimo que si el idarea no es asignada que lo ponga en pendiente y en dado casi si es asignada entonces que lo deje tal cual
                        //cambiar despues Los datos que se envian en duro para que sea mas dinamico las cosas 
                        var save = contexto.sp_Requerimiento_Alta(1, 1, GetIdUser(), ticket.fcTituloRequerimiento, ticket.fcDescripcionRequerimiento, ticket.fiIDEstadoRequerimiento,1, idarea).FirstOrDefault();

                        agregarCreacionTicket((int)save.IdIngresado);
                        return EnviarListaJson(save);

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

        public JsonResult ActualizarDatos(TicketMiewModel ticket,string comentario)
        {
            //var resultado = new Resultado_ViewModel() { ResultadoExitoso = false };
            //var mensajeError = string.Empty;
            //var estado = Requerimiento.fiIDEstadoRequerimiento;
            try
            {
                using (var contexto = new SARISEntities1())
                {
                    var result = contexto.sp_Requerimiento_Bitacoras_Agregar(GetIdUser(), ticket.fiIDRequerimiento, GetIdUser(), ticket.fcDescripcionRequerimiento, 1, ticket.fiIDEstadoRequerimiento);
                    var actua = contexto.sp_Requerimiento_Maestro_Actualizar(GetIdUser(), ticket.fiIDRequerimiento, ticket.fcTituloRequerimiento, ticket.fcDescripcionRequerimiento, ticket.fiIDEstadoRequerimiento, DateTime.Now, ticket.fiIDUsuarioAsignado, 0, ticket.fiTipoRequerimiento, 1, ticket.fiAreaAsignada);
                    return EnviarResultado(true, "", "Ticket Actualizado exitosamente");
                }
            }
            catch (Exception ex)
            {
                return EnviarResultado(false, ex.Message.ToString(), "No se pudo Actualizar el ticket");
                throw;
            }
        }

        static HttpPostedFileBase ConvertirBase64AImagen(string base64String, string nombreArchivo)
        {
            
            HttpPostedFileBase archivo = new ByteArrayHttpPostedFile(base64String, nombreArchivo);
            //GuardarImagen(bytes, archivo);
            return archivo;

        }

        public  JsonResult Guardardocumentos(List<TicketAdjuntarImagenViewModel>modelo)
        {
            try
            {
                using (var contexto = new SARISEntities1())
                {
                    foreach (var item in modelo)
                    {

                        var arch = ConvertirBase64AImagen(item.pcRutaArchivo, item.pcNombreArchivo);
                        var guardardocumentos = contexto.sp_Requerimientos_Adjuntos_Guardar(item.piIDRequerimiento, item.pcNombreArchivo, item.pcTipoArchivo, MemoryLoadManager.UrlWeb + @"/Documentos\Ticket\Ticket_" + arch.FileName, MemoryLoadManager.UrlWeb + @"/Documentos/Ticket/Ticket_" + arch.FileName, item.piIDSesion, item.piIDApp,GetIdUser());
                        
                        string carpeta = @"\Documentos\Ticket\Ticket_" + item.piIDRequerimiento ;
                        var urlPdf = MemoryLoadManager.URL + carpeta;
                        var ruta = carpeta + @"\";
                        ruta = ruta.Replace("*", "").Replace("/", "").Replace("\\", "").Replace(":", "").Replace("?", "").Replace("<", "").Replace(">", "").Replace("|", "");

                        var exists = System.IO.Directory.Exists(urlPdf);

                        //string nombreArchivo = "archivo.txt";
                        UploadFileServer148(@"Documentos\Ticket\Ticket_" + item.piIDRequerimiento, arch);
                        
                    }
                    return  EnviarResultado(true,"","Documentos guardados con Exito");
                }
            }
            catch (Exception ex)
            {
                return EnviarResultado(true, ex.Message.ToString(), "No se pudo Actualizar el ticket");
                throw;
            }
            
            
        }

        public ActionResult VistaActualizarArea(int idticket,int estadoticket)
        {
            ViewBag.IdTicket = idticket;
            ViewBag.EstadoTicket = estadoticket;
            using (var contexto = new SARISEntities1())
            { 
                var datosticket = contexto.sp_Requerimientos_Bandeja_ByID(1, 1, GetIdUser(), idticket).FirstOrDefault();
                //ViewBag.idArea = 
            }

            return PartialView();
        }

        public JsonResult ActualizarArea(int idticket, int idArea,int estadoTicket)
        {
            try
            {
                using (var contexto = new SARISEntities1())
                {
                    var areaasignada = contexto.sp_Requerimiento_Areas(1,1,GetIdUser()).FirstOrDefault(a => a.fiIDArea == idArea).fcDescripcion; // buscar el area a la cual se le asigno
                    var usuarioLogueado = contexto.sp_Usuarios_Maestro_PorIdUsuario(GetIdUser()).FirstOrDefault();

                    var result = contexto.sp_Requerimiento_Bitacoras_Agregar(GetIdUser(), idticket, GetIdUser(), $"El Usuario {usuarioLogueado.fcPrimerNombre} {usuarioLogueado.fcPrimerApellido}Se Asigno El Area a " + areaasignada, 1, estadoTicket);
                    var datosticket = contexto.sp_Requerimientos_Bandeja_ByID(1, 1, GetIdUser(), idticket).FirstOrDefault();
                    var actua = contexto.sp_Requerimiento_Maestro_Actualizar(GetIdUser(), datosticket.fiIDRequerimiento, datosticket.fcTituloRequerimiento, datosticket.fcDescripcionRequerimiento, Convert.ToByte(estadoTicket), DateTime.Now, datosticket.fiIDUsuarioAsignado, 0, datosticket.fiTipoRequerimiento, 1, idArea);
                    return EnviarResultado(true, "", "Ticket Actualizado exitosamente");
                }
            }
            catch (Exception ex)
            {
                return EnviarResultado(false, ex.Message.ToString(), "No se pudo Actualizar el ticket");
                throw;
            }
        }

        public ActionResult VistaAsignarUsuario(int idticket, int estadoticket,int idarea)
        {
            ViewBag.IdTicket = idticket;
            ViewBag.EstadoTicket = estadoticket;
            ViewBag.Area = idarea;
            using (var contexto = new SARISEntities1())
            {
                var datosticket = contexto.sp_Requerimientos_Bandeja_ByID(1, 1, GetIdUser(), idticket).FirstOrDefault();
                //ViewBag.idArea = 
            }

            return PartialView();
        }

        public JsonResult ActualizarUsuario(int idticket, int usuario, int estadoTicket)
        {
            try
            {
                using (var contexto = new SARISEntities1())
                {
                    //saber el string del nombre del usuario
                    var UsuarioAsignado = contexto.sp_Usuarios_Maestro_PorIdUsuario(usuario).FirstOrDefault(); // buscar el area a la cual se le asigno
                    var usuarioLogueado = contexto.sp_Usuarios_Maestro_PorIdUsuario(GetIdUser()).FirstOrDefault();
                    //guardar la bitacora 
                    var result = contexto.sp_Requerimiento_Bitacoras_Agregar(GetIdUser(), idticket, GetIdUser(), $"El Usuario {usuarioLogueado.fcPrimerNombre} {usuarioLogueado.fcPrimerApellido} Asigno al Usuario {UsuarioAsignado.fcPrimerNombre} {UsuarioAsignado.fcPrimerApellido}" , 1, 7);//el estado de ticket esta en 7 para que pueda guardar la bitacora
                    
                    var datosticket = contexto.sp_Requerimientos_Bandeja_ByID(1, 1, GetIdUser(), idticket).FirstOrDefault();
                    
                    var actua = contexto.sp_Requerimiento_Maestro_Actualizar(GetIdUser(), datosticket.fiIDRequerimiento, datosticket.fcTituloRequerimiento, datosticket.fcDescripcionRequerimiento, 7, DateTime.Now, usuario, 0, datosticket.fiTipoRequerimiento, 1, datosticket.fiAreaAsignada);//el estado de ticket esta en 7 para que pueda guardar la bitacora
                    
                    return EnviarResultado(true, "", "Ticket Usuario Actualizado exitosamente");
                }
            }
            catch (Exception ex)
            {
                return EnviarResultado(false, ex.Message.ToString(), "No se pudo Actualizar el usuario");
                throw;
            }
        }


    }
}