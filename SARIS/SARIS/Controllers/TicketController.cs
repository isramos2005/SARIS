﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using OrionCoreCableColor.DbConnection;
using OrionCoreCableColor.Models.Ticket;
using System.IO;
using OrionCoreCableColor.App_Helper;
using System.Threading.Tasks;
using OrionCoreCableColor.App_Services.EmailService;
using OrionCoreCableColor.Models.EmailTemplateService;
using OrionCoreCableColor.Models.Indicadores;

namespace OrionCoreCableColor.Controllers
{
    public class TicketController : BaseController
    {
        // GET: Ticket
        public ActionResult Index()
        {
            ViewBag.idUsuario = GetIdUser();
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

        public ActionResult ModalBitacoraMejora(int id)
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
                    tick.fiTipoRequerimiento = (int)cont.fiTipoRequerimiento;
                    tick.fiCategoriadeDesarrollo = (int)cont.fiCategoriadeDesarrollo;

                    var estadosquenovan = contexto.sp_Configuraciones("NoMostrarEstados").FirstOrDefault().fcValorLlave.Split(',').Select(a => Convert.ToInt32(a)).ToList();
                    ViewBag.ListarArea = contexto.sp_Areas_Lista().Select(x => new SelectListItem { Value = x.fiIDArea.ToString(), Text = x.fcDescripcion}).ToList();
                    ViewBag.ListaCategorias = contexto.sp_Categorias_Indicidencias_Listado().Select(a => new SelectListItem { Value = a.fiIDCategoriaDesarrollo.ToString(), Text = a.fcDescripcionCategoria}).ToList();
                    ViewBag.IdIncidencia = tick.fiTipoRequerimiento;
                    var puede = false;
                    
                    var idrolestodopoderosos = contexto.sp_Configuraciones("RolesquePuedenverTodo").FirstOrDefault().fcValorLlave.Split(',').Select(a => Convert.ToInt32(a)).ToList();

                    var user = GetUser();
                    if (GetIdUser() == cont.fiIDUsuarioSolicitante  || idrolestodopoderosos.Contains(user.IdRol) )
                    {
                        ViewBag.Estados = contexto.sp_Estados_Lista().Where(a => !estadosquenovan.Any(b => b == a.fiIDEstado)).Select(x => new SelectListItem { Value = x.fiIDEstado.ToString(), Text = x.fcDescripcionEstado }).ToList();
                        puede = true;
                    }
                    else
                    {
                        ViewBag.Estados = contexto.sp_Estados_Lista().Where(a => a.fiIDEstado != 5 && !estadosquenovan.Any(b => b == a.fiIDEstado)).Select(x => new SelectListItem { Value = x.fiIDEstado.ToString(), Text = x.fcDescripcionEstado }).ToList();
                        puede = false;
                    }
                    ViewBag.PuedeEditarCategoria = puede;
                    ViewBag.Usuario = contexto.sp_Usuarios_Maestro_PorIdUsuarioSupervisor(1).Select(x => new SelectListItem { Value = x.fiIDUsuario.ToString(), Text = x.fcPrimerNombre + " " + x.fcPrimerApellido }).ToList();
                    ViewBag.idticket = idticket;
                    ViewBag.UsuarioLogueado = GetIdUser();
                    ViewBag.DatosDocumentoListado = contexto.sp_Requerimiento_Documentos_ObtenerPorIdRequerimiento(idticket, 1, 1, GetIdUser()).ToList();

                    return PartialView(tick);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> GuardarTicket(TicketMiewModel ticket,string comentarioticket)
        {
            try
            {
                using (var contexto = new SARISEntities1()) {

                    try 
                    {
                        var idarea = (ticket.fiAreaAsignada == 0) ? 6 : ticket.fiAreaAsignada; // aqui decimo que si el idarea no es asignada que lo ponga en pendiente y en dado casi si es asignada entonces que lo deje tal cual
                        //cambiar despues Los datos que se envian en duro para que sea mas dinamico las cosas 
                        var usuarioLogueado = contexto.sp_Usuarios_Maestro_PorIdUsuario(GetIdUser()).FirstOrDefault();

                        var save = contexto.sp_Requerimiento_Alta(1, 1, GetIdUser(), ticket.fcTituloRequerimiento, ticket.fcDescripcionRequerimiento, ticket.fiIDEstadoRequerimiento, ticket.fiTipoRequerimiento, idarea, $"El usuario {usuarioLogueado.fcPrimerNombre} {usuarioLogueado.fcPrimerApellido} a Creado El Ticket").FirstOrDefault();
                        var datosticket = Datosticket((int)save.IdIngresado);
                        //GuardarBitacoraGeneralhistorial(GetIdUser(),datosticket.fiIDRequerimiento,datosticket.fiIDUsuarioSolicitante, comentarioticket,1,datosticket.fiIDEstadoRequerimiento,datosticket.fiIDUsuarioAsignado);

                        
                        agregarCreacionTicket((int)save.IdIngresado);//esto es SignalR
                        var correo = contexto.sp_DatosTicket_Correo(datosticket.fiIDRequerimiento).FirstOrDefault();
                        var _emailTemplateService = new EmailTemplateService();
                        await _emailTemplateService.SendEmailToSolicitud(new EmailTemplateTicketModel
                        {
                            fiIDRequerimiento = correo.fiIDRequerimiento,
                            fcTituloRequerimiento = correo.fcTituloRequerimiento,
                            fcDescripcionRequerimiento = correo.fcDescripcionRequerimiento,
                            fdFechaCreacion = correo.fdFechaCreacion,
                            fiIDAreaSolicitante = correo.fiIDAreaSolicitante,
                            fcAreaSolicitante = correo.fcAreaSolicitante,
                            fiIDUsuarioSolicitante = correo.fiIDUsuarioSolicitante,
                            fcNombreCorto = correo.fcNombreCorto,
                            fiIDEstadoRequerimiento = correo.fiIDEstadoRequerimiento,
                            fcDescripcionEstado = correo.fcDescripcionEstado,
                            fcCorreoElectronico = correo.fcCorreoElectronico,
                            fcDescripcionCategoria = correo.fcDescripcionCategoria,
                            fcTipoRequerimiento = correo.fcTipoRequerimiento
                        });
                        //MensajeDeTexto.EnviarLinkGeoLocation(model.Nombre, model.IdCliente, model.Telefono, "");
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

        public async Task<JsonResult> ActualizarDatos(TicketMiewModel ticket,string comentario)
        {
            //var resultado = new Resultado_ViewModel() { ResultadoExitoso = false };
            //var mensajeError = string.Empty;
            //var estado = Requerimiento.fiIDEstadoRequerimiento;
            try
            {
                using (var contexto = new SARISEntities1())
                {
                    var datosticket = Datosticket(ticket.fiIDRequerimiento);//contexto.sp_Requerimientos_Bandeja_ByID(1, 1, GetIdUser(), ticket.fiIDRequerimiento).FirstOrDefault();

                    GuardarBitacoraGeneralhistorial(GetIdUser(), ticket.fiIDRequerimiento, GetIdUser(), comentario, 1, ticket.fiIDEstadoRequerimiento, datosticket.fiIDUsuarioAsignado);
                    var actua = contexto.sp_Requerimiento_Maestro_Actualizar(GetIdUser(), ticket.fiIDRequerimiento, ticket.fcTituloRequerimiento, ticket.fcDescripcionRequerimiento, ticket.fiIDEstadoRequerimiento, DateTime.Now, datosticket.fiIDUsuarioAsignado, 0, ticket.fiTipoRequerimiento, 1, datosticket.fiAreaAsignada);
                    if (ticket.fiIDEstadoRequerimiento == 5)
                    {
                        eliminarTicketAbierto(ticket.fiIDRequerimiento);
                        agregarDatosTicketCerrados(ticket.fiIDRequerimiento);
                    }
                    else
                    {
                        ObtenerDataTicket(ticket.fiIDRequerimiento); //Esto es el SignalR
                    }
                    var correo = contexto.sp_DatosTicket_Correo(ticket.fiIDRequerimiento).FirstOrDefault();
                    var _emailTemplateService = new EmailTemplateService();
                    await _emailTemplateService.SendEmailToSolicitud(new EmailTemplateTicketModel
                    {
                        fiIDRequerimiento = correo.fiIDRequerimiento,
                        fcTituloRequerimiento = correo.fcTituloRequerimiento,
                        fcDescripcionRequerimiento = correo.fcDescripcionRequerimiento,
                        fdFechaCreacion = correo.fdFechaCreacion,
                        fiIDAreaSolicitante = correo.fiIDAreaSolicitante,
                        fcAreaSolicitante = correo.fcAreaSolicitante,
                        fiIDUsuarioSolicitante = correo.fiIDUsuarioSolicitante,
                        fcNombreCorto = correo.fcNombreCorto,
                        fiIDEstadoRequerimiento = correo.fiIDEstadoRequerimiento,
                        fcDescripcionEstado = correo.fcDescripcionEstado,
                        fcCorreoElectronico = correo.fcCorreoElectronico,
                        fcDescripcionCategoria = correo.fcDescripcionCategoria,
                        fcTipoRequerimiento = correo.fcTipoRequerimiento
                    });
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
                        
                        var arch = ConvertirBase64AImagen(item.pcRutaArchivo, item.pcNombreArchivo);//esto funciona tan bien que convierte imagenes, pdf, word, pdf, gatos, perros y los sube bien
                        
                        var guardardocumentos = contexto.sp_Requerimientos_Adjuntos_Guardar(item.piIDRequerimiento, item.pcNombreArchivo, item.pcTipoArchivo, MemoryLoadManager.UrlWeb + @"/Documentos\Ticket\Ticket_" + item.piIDRequerimiento+ "/" + arch.FileName, MemoryLoadManager.UrlWeb + @"/Documentos/Ticket/Ticket_" + item.piIDRequerimiento + "/" + arch.FileName, item.piIDSesion, item.piIDApp,GetIdUser());
                        
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
                var datosticket = Datosticket(idticket); //contexto.sp_Requerimientos_Bandeja_ByID(1, 1, GetIdUser(), idticket).FirstOrDefault();
                //ViewBag.idArea = 
            }

            return PartialView();
        }

        public async Task<JsonResult> ActualizarArea(int idticket, int idArea,int estadoTicket, string comenta)
        {
            try
            {
                using (var contexto = new SARISEntities1())
                {
                    var areaasignada = contexto.sp_Requerimiento_Areas(1,1,GetIdUser()).FirstOrDefault(a => a.fiIDArea == idArea).fcDescripcion; // buscar el area a la cual se le asigno
                    var usuarioLogueado = contexto.sp_Usuarios_Maestro_PorIdUsuario(GetIdUser()).FirstOrDefault();

                    var datosticket = Datosticket(idticket);//contexto.sp_Requerimientos_Bandeja_ByID(1, 1, GetIdUser(), idticket).FirstOrDefault();
                    var actua = contexto.sp_Requerimiento_Maestro_Actualizar(GetIdUser(), datosticket.fiIDRequerimiento, datosticket.fcTituloRequerimiento, datosticket.fcDescripcionRequerimiento, Convert.ToByte(7), DateTime.Now, 0, 0, datosticket.fiTipoRequerimiento, 1, idArea);
                    ObtenerDataTicket(idticket); // aqui va el signalR
                    
                    GuardarBitacoraGeneralhistorial(GetIdUser(), idticket, GetIdUser(), $"El Usuario {usuarioLogueado.fcPrimerNombre} {usuarioLogueado.fcPrimerApellido} reasigna por: " + comenta, 1, 7,0);//se manda 0 por que se asigno una nueva area y por lo tanto el usuario asignado no puede ser otro

                    if (datosticket.fiAreaAsignada != idArea)
                    {
                        eliminarTicketAbierto(datosticket.fiIDRequerimiento);
                    }
                    var correo = contexto.sp_DatosTicket_Correo(datosticket.fiIDRequerimiento).FirstOrDefault();
                    var _emailTemplateService = new EmailTemplateService();
                    await _emailTemplateService.SendEmailToSolicitud(new EmailTemplateTicketModel
                    {
                        fiIDRequerimiento = correo.fiIDRequerimiento,
                        fcTituloRequerimiento = correo.fcTituloRequerimiento,
                        fcDescripcionRequerimiento = correo.fcDescripcionRequerimiento,
                        fdFechaCreacion = correo.fdFechaCreacion,
                        fiIDAreaSolicitante = correo.fiIDAreaSolicitante,
                        fcAreaSolicitante = correo.fcAreaSolicitante,
                        fiIDUsuarioSolicitante = correo.fiIDUsuarioSolicitante,
                        fcNombreCorto = correo.fcNombreCorto,
                        fiIDEstadoRequerimiento = correo.fiIDEstadoRequerimiento,
                        fcDescripcionEstado = correo.fcDescripcionEstado,
                        fcCorreoElectronico = correo.fcCorreoElectronico,
                        fcDescripcionCategoria = correo.fcDescripcionCategoria,
                        fcTipoRequerimiento = correo.fcTipoRequerimiento
                    });

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

        public async Task<JsonResult> ActualizarUsuario(int idticket, int usuario, int estadoTicket, string comenta)
        {
            try
            {
                using (var contexto = new SARISEntities1())
                {
                    //saber el string del nombre del usuario
                    var UsuarioAsignado = contexto.sp_Usuarios_Maestro_PorIdUsuario(usuario).FirstOrDefault(); // buscar el area a la cual se le asigno
                    var usuarioLogueado = contexto.sp_Usuarios_Maestro_PorIdUsuario(GetIdUser()).FirstOrDefault();

                    var datosticket = Datosticket(idticket);//contexto.sp_Requerimientos_Bandeja_ByID(1, 1, GetIdUser(), idticket).FirstOrDefault();
                    //guardar la bitacora 
                    GuardarBitacoraGeneralhistorial(GetIdUser(), idticket, datosticket.fiIDUsuarioSolicitante, comenta, 1, 7, usuario);//el estado de ticket esta en 7 para que pueda guardar la bitacora

                    var actua = contexto.sp_Requerimiento_Maestro_Actualizar(GetIdUser(), datosticket.fiIDRequerimiento, datosticket.fcTituloRequerimiento, datosticket.fcDescripcionRequerimiento, 7, DateTime.Now, usuario, 0, datosticket.fiTipoRequerimiento, 1, datosticket.fiAreaAsignada);//el estado de ticket esta en 7 para que pueda guardar la bitacora
                    ObtenerDataTicket(idticket);//aqui esta el signalR
                    if (GetIdUser() != usuario) //aqui el signalR por si al reasignar un usuario se le quite de la bandeja de el 
                    {
                        eliminarTicketAbierto(datosticket.fiIDRequerimiento);
                    }

                    var correo = contexto.sp_DatosTicket_Correo(datosticket.fiIDRequerimiento).FirstOrDefault();
                    var _emailTemplateService = new EmailTemplateService();
                    await _emailTemplateService.SendEmailToSolicitud(new EmailTemplateTicketModel
                    {
                        fiIDRequerimiento = correo.fiIDRequerimiento,
                        fcTituloRequerimiento = correo.fcTituloRequerimiento,
                        fcDescripcionRequerimiento = correo.fcDescripcionRequerimiento,
                        fdFechaCreacion = correo.fdFechaCreacion,
                        fiIDAreaSolicitante = correo.fiIDAreaSolicitante,
                        fcAreaSolicitante = correo.fcAreaSolicitante,
                        fiIDUsuarioSolicitante = correo.fiIDUsuarioSolicitante,
                        fcNombreCorto = correo.fcNombreCorto,
                        fiIDEstadoRequerimiento = correo.fiIDEstadoRequerimiento,
                        fcDescripcionEstado = correo.fcDescripcionEstado,
                        fcCorreoElectronico = correo.fcCorreoElectronico,
                        fcDescripcionCategoria = correo.fcDescripcionCategoria,
                        fcTipoRequerimiento = correo.fcTipoRequerimiento
                    });

                    return EnviarResultado(true, "", "Ticket Usuario Actualizado exitosamente");
                }
            }
            catch (Exception ex)
            {
                return EnviarResultado(false, ex.Message.ToString(), "No se pudo Actualizar el usuario");
                throw;
            }
        }

        public JsonResult EliminarTicket(int idticket)
        {
            try
            {
                using (var contexto = new SARISEntities1()) {
                    var result = contexto.sp_Eliminar_Requerimiento(idticket).FirstOrDefault();
                    eliminarTicketAbierto(idticket);
                    return EnviarResultado(true, "Eliminado!", "Ticket Eliminado Exitosamente");
                }
            }
            catch (Exception ex)
            {
                return EnviarResultado(false, ex.Message.ToString(), "No se pudo Eliminar el ticket");
                throw;
            }
        }

        public JsonResult GuardarBitacoraGeneralhistorial(int idusuario, int idticket, int idusuariosolicitante, string comentario, int idapp, int idestado, int idusuarioasignado)
        {
            try
            {
                using (var contexto = new SARISEntities1())
                {
                    var result = contexto.sp_Requerimiento_Bitacoras_Agregar(GetIdUser(), idticket, idusuariosolicitante, comentario, idapp, idestado, idusuarioasignado);
                    return EnviarListaJson(result);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public sp_Requerimientos_Bandeja_ByID_Result Datosticket(int idticket)
        {
            try
            {
                using (var contexto = new SARISEntities1())
                {
                    var datosticket = contexto.sp_Requerimientos_Bandeja_ByID(1, 1, GetIdUser(), idticket).FirstOrDefault();
                    return datosticket;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ActionResult DetalleTicket(int idticket)
        {

            //var cont = contexto.sp_Requerimiento_Maestro_Detalle(1, 1, GetIdUser(), idticket).FirstOrDefault();
            return PartialView();
        }


        ///////////////////////////// LLenar campos
        public JsonResult SelectCategorias()
        {
            using (var contexto = new SARISEntities1())
            {
                var jsonResult = Json(contexto.sp_Categorias_Indicidencias_Listado().Where(a => a.fiEstado == 1).Select(x => new ListaIndicadoresViewModel
                {
                    fiIDTipoRequerimiento = x.fiIDCategoriaDesarrollo,
                    fcTipoRequerimiento = x.fcDescripcionCategoria,
                    fcToken = x.fcToken


                }).ToList(), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = Int32.MaxValue;
                return jsonResult;
            }
        }


    }
}