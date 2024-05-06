﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrionCoreCableColor.DbConnection
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SARISEntities1 : DbContext
    {
        public SARISEntities1()
            : base("name=SARISEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<Nullable<int>> Multiplicacion(Nullable<int> numero1, Nullable<int> numero2)
        {
            var numero1Parameter = numero1.HasValue ?
                new ObjectParameter("numero1", numero1) :
                new ObjectParameter("numero1", typeof(int));
    
            var numero2Parameter = numero2.HasValue ?
                new ObjectParameter("numero2", numero2) :
                new ObjectParameter("numero2", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Multiplicacion", numero1Parameter, numero2Parameter);
        }
    
        public virtual int sp_NumeroConsecutivo(Nullable<int> piIDContador, ObjectParameter poiSiguienteNumero)
        {
            var piIDContadorParameter = piIDContador.HasValue ?
                new ObjectParameter("piIDContador", piIDContador) :
                new ObjectParameter("piIDContador", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_NumeroConsecutivo", piIDContadorParameter, poiSiguienteNumero);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_Requerimiento_Agregar(Nullable<int> piIDUsuario, string pcIP, string pcTituloRequerimiento, string pcDescripcionRequerimiento, Nullable<byte> piTipoRequerimiento, Nullable<int> piIDAreaSolicitante)
        {
            var piIDUsuarioParameter = piIDUsuario.HasValue ?
                new ObjectParameter("piIDUsuario", piIDUsuario) :
                new ObjectParameter("piIDUsuario", typeof(int));
    
            var pcIPParameter = pcIP != null ?
                new ObjectParameter("pcIP", pcIP) :
                new ObjectParameter("pcIP", typeof(string));
    
            var pcTituloRequerimientoParameter = pcTituloRequerimiento != null ?
                new ObjectParameter("pcTituloRequerimiento", pcTituloRequerimiento) :
                new ObjectParameter("pcTituloRequerimiento", typeof(string));
    
            var pcDescripcionRequerimientoParameter = pcDescripcionRequerimiento != null ?
                new ObjectParameter("pcDescripcionRequerimiento", pcDescripcionRequerimiento) :
                new ObjectParameter("pcDescripcionRequerimiento", typeof(string));
    
            var piTipoRequerimientoParameter = piTipoRequerimiento.HasValue ?
                new ObjectParameter("piTipoRequerimiento", piTipoRequerimiento) :
                new ObjectParameter("piTipoRequerimiento", typeof(byte));
    
            var piIDAreaSolicitanteParameter = piIDAreaSolicitante.HasValue ?
                new ObjectParameter("piIDAreaSolicitante", piIDAreaSolicitante) :
                new ObjectParameter("piIDAreaSolicitante", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_Requerimiento_Agregar", piIDUsuarioParameter, pcIPParameter, pcTituloRequerimientoParameter, pcDescripcionRequerimientoParameter, piTipoRequerimientoParameter, piIDAreaSolicitanteParameter);
        }
    
        public virtual ObjectResult<string> sp_Requerimiento_Alta(Nullable<int> piIDSesion, Nullable<short> piIDApp, Nullable<int> piIDUsuario, string pcTituloRequerimiento, string pcDetalleRequerimiento, Nullable<byte> piEstadoRequerimiento, Nullable<byte> piTipoRequerimiento)
        {
            var piIDSesionParameter = piIDSesion.HasValue ?
                new ObjectParameter("piIDSesion", piIDSesion) :
                new ObjectParameter("piIDSesion", typeof(int));
    
            var piIDAppParameter = piIDApp.HasValue ?
                new ObjectParameter("piIDApp", piIDApp) :
                new ObjectParameter("piIDApp", typeof(short));
    
            var piIDUsuarioParameter = piIDUsuario.HasValue ?
                new ObjectParameter("piIDUsuario", piIDUsuario) :
                new ObjectParameter("piIDUsuario", typeof(int));
    
            var pcTituloRequerimientoParameter = pcTituloRequerimiento != null ?
                new ObjectParameter("pcTituloRequerimiento", pcTituloRequerimiento) :
                new ObjectParameter("pcTituloRequerimiento", typeof(string));
    
            var pcDetalleRequerimientoParameter = pcDetalleRequerimiento != null ?
                new ObjectParameter("pcDetalleRequerimiento", pcDetalleRequerimiento) :
                new ObjectParameter("pcDetalleRequerimiento", typeof(string));
    
            var piEstadoRequerimientoParameter = piEstadoRequerimiento.HasValue ?
                new ObjectParameter("piEstadoRequerimiento", piEstadoRequerimiento) :
                new ObjectParameter("piEstadoRequerimiento", typeof(byte));
    
            var piTipoRequerimientoParameter = piTipoRequerimiento.HasValue ?
                new ObjectParameter("piTipoRequerimiento", piTipoRequerimiento) :
                new ObjectParameter("piTipoRequerimiento", typeof(byte));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_Requerimiento_Alta", piIDSesionParameter, piIDAppParameter, piIDUsuarioParameter, pcTituloRequerimientoParameter, pcDetalleRequerimientoParameter, piEstadoRequerimientoParameter, piTipoRequerimientoParameter);
        }
    
        public virtual ObjectResult<sp_Requerimiento_Areas_Result> sp_Requerimiento_Areas(Nullable<int> piIDSesion, Nullable<short> piIDApp, Nullable<int> piIDUsuario)
        {
            var piIDSesionParameter = piIDSesion.HasValue ?
                new ObjectParameter("piIDSesion", piIDSesion) :
                new ObjectParameter("piIDSesion", typeof(int));
    
            var piIDAppParameter = piIDApp.HasValue ?
                new ObjectParameter("piIDApp", piIDApp) :
                new ObjectParameter("piIDApp", typeof(short));
    
            var piIDUsuarioParameter = piIDUsuario.HasValue ?
                new ObjectParameter("piIDUsuario", piIDUsuario) :
                new ObjectParameter("piIDUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Requerimiento_Areas_Result>("sp_Requerimiento_Areas", piIDSesionParameter, piIDAppParameter, piIDUsuarioParameter);
        }
    
        public virtual ObjectResult<sp_Requerimiento_Bitacoras_Detalle_Result> sp_Requerimiento_Bitacoras_Detalle(Nullable<int> piIDUsuario, Nullable<int> piIDRequerimiento, Nullable<int> piIDApp)
        {
            var piIDUsuarioParameter = piIDUsuario.HasValue ?
                new ObjectParameter("piIDUsuario", piIDUsuario) :
                new ObjectParameter("piIDUsuario", typeof(int));
    
            var piIDRequerimientoParameter = piIDRequerimiento.HasValue ?
                new ObjectParameter("piIDRequerimiento", piIDRequerimiento) :
                new ObjectParameter("piIDRequerimiento", typeof(int));
    
            var piIDAppParameter = piIDApp.HasValue ?
                new ObjectParameter("piIDApp", piIDApp) :
                new ObjectParameter("piIDApp", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Requerimiento_Bitacoras_Detalle_Result>("sp_Requerimiento_Bitacoras_Detalle", piIDUsuarioParameter, piIDRequerimientoParameter, piIDAppParameter);
        }
    
        public virtual ObjectResult<sp_Requerimiento_Documentos_ObtenerPorIdRequerimiento_Result> sp_Requerimiento_Documentos_ObtenerPorIdRequerimiento(Nullable<int> piIDRequerimiento, Nullable<int> piIDSesion, Nullable<int> piIDApp, Nullable<int> piIDUsuario)
        {
            var piIDRequerimientoParameter = piIDRequerimiento.HasValue ?
                new ObjectParameter("piIDRequerimiento", piIDRequerimiento) :
                new ObjectParameter("piIDRequerimiento", typeof(int));
    
            var piIDSesionParameter = piIDSesion.HasValue ?
                new ObjectParameter("piIDSesion", piIDSesion) :
                new ObjectParameter("piIDSesion", typeof(int));
    
            var piIDAppParameter = piIDApp.HasValue ?
                new ObjectParameter("piIDApp", piIDApp) :
                new ObjectParameter("piIDApp", typeof(int));
    
            var piIDUsuarioParameter = piIDUsuario.HasValue ?
                new ObjectParameter("piIDUsuario", piIDUsuario) :
                new ObjectParameter("piIDUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Requerimiento_Documentos_ObtenerPorIdRequerimiento_Result>("sp_Requerimiento_Documentos_ObtenerPorIdRequerimiento", piIDRequerimientoParameter, piIDSesionParameter, piIDAppParameter, piIDUsuarioParameter);
        }
    
        public virtual ObjectResult<sp_Requerimiento_Estados_Result> sp_Requerimiento_Estados(Nullable<int> piIDSesion, Nullable<short> piIDApp, Nullable<int> piIDUsuario)
        {
            var piIDSesionParameter = piIDSesion.HasValue ?
                new ObjectParameter("piIDSesion", piIDSesion) :
                new ObjectParameter("piIDSesion", typeof(int));
    
            var piIDAppParameter = piIDApp.HasValue ?
                new ObjectParameter("piIDApp", piIDApp) :
                new ObjectParameter("piIDApp", typeof(short));
    
            var piIDUsuarioParameter = piIDUsuario.HasValue ?
                new ObjectParameter("piIDUsuario", piIDUsuario) :
                new ObjectParameter("piIDUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Requerimiento_Estados_Result>("sp_Requerimiento_Estados", piIDSesionParameter, piIDAppParameter, piIDUsuarioParameter);
        }
    
        public virtual int sp_Requerimiento_Indicadores(Nullable<int> piIDUsuario, Nullable<System.DateTime> pdFechaOperativa)
        {
            var piIDUsuarioParameter = piIDUsuario.HasValue ?
                new ObjectParameter("piIDUsuario", piIDUsuario) :
                new ObjectParameter("piIDUsuario", typeof(int));
    
            var pdFechaOperativaParameter = pdFechaOperativa.HasValue ?
                new ObjectParameter("pdFechaOperativa", pdFechaOperativa) :
                new ObjectParameter("pdFechaOperativa", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Requerimiento_Indicadores", piIDUsuarioParameter, pdFechaOperativaParameter);
        }
    
        public virtual ObjectResult<sp_Requerimiento_Lista_Result> sp_Requerimiento_Lista(Nullable<int> piIDSesion, Nullable<short> piIDApp, Nullable<int> piIDUsuario)
        {
            var piIDSesionParameter = piIDSesion.HasValue ?
                new ObjectParameter("piIDSesion", piIDSesion) :
                new ObjectParameter("piIDSesion", typeof(int));
    
            var piIDAppParameter = piIDApp.HasValue ?
                new ObjectParameter("piIDApp", piIDApp) :
                new ObjectParameter("piIDApp", typeof(short));
    
            var piIDUsuarioParameter = piIDUsuario.HasValue ?
                new ObjectParameter("piIDUsuario", piIDUsuario) :
                new ObjectParameter("piIDUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Requerimiento_Lista_Result>("sp_Requerimiento_Lista", piIDSesionParameter, piIDAppParameter, piIDUsuarioParameter);
        }
    
        public virtual ObjectResult<sp_Requerimiento_Maestro_Detalle_Result> sp_Requerimiento_Maestro_Detalle(Nullable<int> piIDSesion, Nullable<short> piIDApp, Nullable<int> piIDUsuario, Nullable<int> piIDRequerimiento)
        {
            var piIDSesionParameter = piIDSesion.HasValue ?
                new ObjectParameter("piIDSesion", piIDSesion) :
                new ObjectParameter("piIDSesion", typeof(int));
    
            var piIDAppParameter = piIDApp.HasValue ?
                new ObjectParameter("piIDApp", piIDApp) :
                new ObjectParameter("piIDApp", typeof(short));
    
            var piIDUsuarioParameter = piIDUsuario.HasValue ?
                new ObjectParameter("piIDUsuario", piIDUsuario) :
                new ObjectParameter("piIDUsuario", typeof(int));
    
            var piIDRequerimientoParameter = piIDRequerimiento.HasValue ?
                new ObjectParameter("piIDRequerimiento", piIDRequerimiento) :
                new ObjectParameter("piIDRequerimiento", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Requerimiento_Maestro_Detalle_Result>("sp_Requerimiento_Maestro_Detalle", piIDSesionParameter, piIDAppParameter, piIDUsuarioParameter, piIDRequerimientoParameter);
        }
    
        public virtual int sp_Requerimiento_NumeroConsecutivo(Nullable<int> piIDContador, ObjectParameter poiSiguienteNumero)
        {
            var piIDContadorParameter = piIDContador.HasValue ?
                new ObjectParameter("piIDContador", piIDContador) :
                new ObjectParameter("piIDContador", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Requerimiento_NumeroConsecutivo", piIDContadorParameter, poiSiguienteNumero);
        }
    
        public virtual ObjectResult<sp_Requerimiento_Tipo_Adjuntos_Result> sp_Requerimiento_Tipo_Adjuntos(Nullable<int> piIDSesion, Nullable<short> piIDApp, Nullable<int> piIDUsuario)
        {
            var piIDSesionParameter = piIDSesion.HasValue ?
                new ObjectParameter("piIDSesion", piIDSesion) :
                new ObjectParameter("piIDSesion", typeof(int));
    
            var piIDAppParameter = piIDApp.HasValue ?
                new ObjectParameter("piIDApp", piIDApp) :
                new ObjectParameter("piIDApp", typeof(short));
    
            var piIDUsuarioParameter = piIDUsuario.HasValue ?
                new ObjectParameter("piIDUsuario", piIDUsuario) :
                new ObjectParameter("piIDUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Requerimiento_Tipo_Adjuntos_Result>("sp_Requerimiento_Tipo_Adjuntos", piIDSesionParameter, piIDAppParameter, piIDUsuarioParameter);
        }
    
        public virtual ObjectResult<string> sp_Requerimientos_Adjuntos_Guardar(Nullable<int> piIDRequerimiento, string pcNombreArchivo, string pcTipoArchivo, string pcRutaArchivo, string pcURL, Nullable<int> piIDSesion, Nullable<int> piIDApp, Nullable<int> piIDUsuario)
        {
            var piIDRequerimientoParameter = piIDRequerimiento.HasValue ?
                new ObjectParameter("piIDRequerimiento", piIDRequerimiento) :
                new ObjectParameter("piIDRequerimiento", typeof(int));
    
            var pcNombreArchivoParameter = pcNombreArchivo != null ?
                new ObjectParameter("pcNombreArchivo", pcNombreArchivo) :
                new ObjectParameter("pcNombreArchivo", typeof(string));
    
            var pcTipoArchivoParameter = pcTipoArchivo != null ?
                new ObjectParameter("pcTipoArchivo", pcTipoArchivo) :
                new ObjectParameter("pcTipoArchivo", typeof(string));
    
            var pcRutaArchivoParameter = pcRutaArchivo != null ?
                new ObjectParameter("pcRutaArchivo", pcRutaArchivo) :
                new ObjectParameter("pcRutaArchivo", typeof(string));
    
            var pcURLParameter = pcURL != null ?
                new ObjectParameter("pcURL", pcURL) :
                new ObjectParameter("pcURL", typeof(string));
    
            var piIDSesionParameter = piIDSesion.HasValue ?
                new ObjectParameter("piIDSesion", piIDSesion) :
                new ObjectParameter("piIDSesion", typeof(int));
    
            var piIDAppParameter = piIDApp.HasValue ?
                new ObjectParameter("piIDApp", piIDApp) :
                new ObjectParameter("piIDApp", typeof(int));
    
            var piIDUsuarioParameter = piIDUsuario.HasValue ?
                new ObjectParameter("piIDUsuario", piIDUsuario) :
                new ObjectParameter("piIDUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_Requerimientos_Adjuntos_Guardar", piIDRequerimientoParameter, pcNombreArchivoParameter, pcTipoArchivoParameter, pcRutaArchivoParameter, pcURLParameter, piIDSesionParameter, piIDAppParameter, piIDUsuarioParameter);
        }
    
        public virtual int sp_Requerimientos_Bandeja(Nullable<int> piIDSesion, Nullable<int> piIDApp, Nullable<int> piIDUsuario)
        {
            var piIDSesionParameter = piIDSesion.HasValue ?
                new ObjectParameter("piIDSesion", piIDSesion) :
                new ObjectParameter("piIDSesion", typeof(int));
    
            var piIDAppParameter = piIDApp.HasValue ?
                new ObjectParameter("piIDApp", piIDApp) :
                new ObjectParameter("piIDApp", typeof(int));
    
            var piIDUsuarioParameter = piIDUsuario.HasValue ?
                new ObjectParameter("piIDUsuario", piIDUsuario) :
                new ObjectParameter("piIDUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Requerimientos_Bandeja", piIDSesionParameter, piIDAppParameter, piIDUsuarioParameter);
        }
    
        public virtual int sp_Requerimientos_Bitacoras_Estados(Nullable<int> piIDRequerimiento)
        {
            var piIDRequerimientoParameter = piIDRequerimiento.HasValue ?
                new ObjectParameter("piIDRequerimiento", piIDRequerimiento) :
                new ObjectParameter("piIDRequerimiento", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Requerimientos_Bitacoras_Estados", piIDRequerimientoParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> Suma(Nullable<int> numero1, Nullable<int> numero2)
        {
            var numero1Parameter = numero1.HasValue ?
                new ObjectParameter("numero1", numero1) :
                new ObjectParameter("numero1", typeof(int));
    
            var numero2Parameter = numero2.HasValue ?
                new ObjectParameter("numero2", numero2) :
                new ObjectParameter("numero2", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Suma", numero1Parameter, numero2Parameter);
        }
    
        public virtual int sp_Areas_Desactivar(Nullable<int> fiIDArea)
        {
            var fiIDAreaParameter = fiIDArea.HasValue ?
                new ObjectParameter("fiIDArea", fiIDArea) :
                new ObjectParameter("fiIDArea", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Areas_Desactivar", fiIDAreaParameter);
        }
    
        public virtual int sp_Areas_Editar(Nullable<int> piIDArea, string pcDescripcion, string pcCorreoElectronico, Nullable<int> piIDUsuarioResponsable)
        {
            var piIDAreaParameter = piIDArea.HasValue ?
                new ObjectParameter("piIDArea", piIDArea) :
                new ObjectParameter("piIDArea", typeof(int));
    
            var pcDescripcionParameter = pcDescripcion != null ?
                new ObjectParameter("pcDescripcion", pcDescripcion) :
                new ObjectParameter("pcDescripcion", typeof(string));
    
            var pcCorreoElectronicoParameter = pcCorreoElectronico != null ?
                new ObjectParameter("pcCorreoElectronico", pcCorreoElectronico) :
                new ObjectParameter("pcCorreoElectronico", typeof(string));
    
            var piIDUsuarioResponsableParameter = piIDUsuarioResponsable.HasValue ?
                new ObjectParameter("piIDUsuarioResponsable", piIDUsuarioResponsable) :
                new ObjectParameter("piIDUsuarioResponsable", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Areas_Editar", piIDAreaParameter, pcDescripcionParameter, pcCorreoElectronicoParameter, piIDUsuarioResponsableParameter);
        }
    
        public virtual ObjectResult<sp_Areas_Find_Result> sp_Areas_Find(Nullable<int> fiIDArea)
        {
            var fiIDAreaParameter = fiIDArea.HasValue ?
                new ObjectParameter("fiIDArea", fiIDArea) :
                new ObjectParameter("fiIDArea", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Areas_Find_Result>("sp_Areas_Find", fiIDAreaParameter);
        }
    
        public virtual int sp_Areas_Insertar(string pcDescripcion, string pcCorreoElectronico, Nullable<int> piIDUsuarioResponsable)
        {
            var pcDescripcionParameter = pcDescripcion != null ?
                new ObjectParameter("pcDescripcion", pcDescripcion) :
                new ObjectParameter("pcDescripcion", typeof(string));
    
            var pcCorreoElectronicoParameter = pcCorreoElectronico != null ?
                new ObjectParameter("pcCorreoElectronico", pcCorreoElectronico) :
                new ObjectParameter("pcCorreoElectronico", typeof(string));
    
            var piIDUsuarioResponsableParameter = piIDUsuarioResponsable.HasValue ?
                new ObjectParameter("piIDUsuarioResponsable", piIDUsuarioResponsable) :
                new ObjectParameter("piIDUsuarioResponsable", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Areas_Insertar", pcDescripcionParameter, pcCorreoElectronicoParameter, piIDUsuarioResponsableParameter);
        }
    
        public virtual int sp_Estados_Editar(Nullable<int> piIDEstado, string pcDescripcionEstado, string pcClaseColor)
        {
            var piIDEstadoParameter = piIDEstado.HasValue ?
                new ObjectParameter("piIDEstado", piIDEstado) :
                new ObjectParameter("piIDEstado", typeof(int));
    
            var pcDescripcionEstadoParameter = pcDescripcionEstado != null ?
                new ObjectParameter("pcDescripcionEstado", pcDescripcionEstado) :
                new ObjectParameter("pcDescripcionEstado", typeof(string));
    
            var pcClaseColorParameter = pcClaseColor != null ?
                new ObjectParameter("pcClaseColor", pcClaseColor) :
                new ObjectParameter("pcClaseColor", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Estados_Editar", piIDEstadoParameter, pcDescripcionEstadoParameter, pcClaseColorParameter);
        }
    
        public virtual int sp_Estados_Insertar(string pcDescripcionEstado, string pcClaseColor)
        {
            var pcDescripcionEstadoParameter = pcDescripcionEstado != null ?
                new ObjectParameter("pcDescripcionEstado", pcDescripcionEstado) :
                new ObjectParameter("pcDescripcionEstado", typeof(string));
    
            var pcClaseColorParameter = pcClaseColor != null ?
                new ObjectParameter("pcClaseColor", pcClaseColor) :
                new ObjectParameter("pcClaseColor", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Estados_Insertar", pcDescripcionEstadoParameter, pcClaseColorParameter);
        }
    
        public virtual ObjectResult<sp_Estados_Lista_Result> sp_Estados_Lista()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Estados_Lista_Result>("sp_Estados_Lista");
        }
    
        public virtual int sp_Indicadores_Desactivar(Nullable<int> fiIDTipoRequerimiento)
        {
            var fiIDTipoRequerimientoParameter = fiIDTipoRequerimiento.HasValue ?
                new ObjectParameter("fiIDTipoRequerimiento", fiIDTipoRequerimiento) :
                new ObjectParameter("fiIDTipoRequerimiento", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Indicadores_Desactivar", fiIDTipoRequerimientoParameter);
        }
    
        public virtual int sp_Indicadores_Editar(Nullable<int> fiIDTipoRequerimiento, string fcTipoRequerimiento)
        {
            var fiIDTipoRequerimientoParameter = fiIDTipoRequerimiento.HasValue ?
                new ObjectParameter("fiIDTipoRequerimiento", fiIDTipoRequerimiento) :
                new ObjectParameter("fiIDTipoRequerimiento", typeof(int));
    
            var fcTipoRequerimientoParameter = fcTipoRequerimiento != null ?
                new ObjectParameter("fcTipoRequerimiento", fcTipoRequerimiento) :
                new ObjectParameter("fcTipoRequerimiento", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Indicadores_Editar", fiIDTipoRequerimientoParameter, fcTipoRequerimientoParameter);
        }
    
        public virtual ObjectResult<sp_Indicadores_Find_Result> sp_Indicadores_Find(Nullable<int> fiIDTipoRequerimiento)
        {
            var fiIDTipoRequerimientoParameter = fiIDTipoRequerimiento.HasValue ?
                new ObjectParameter("fiIDTipoRequerimiento", fiIDTipoRequerimiento) :
                new ObjectParameter("fiIDTipoRequerimiento", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Indicadores_Find_Result>("sp_Indicadores_Find", fiIDTipoRequerimientoParameter);
        }
    
        public virtual int sp_Indicadores_Insertar(string fcTipoRequerimiento)
        {
            var fcTipoRequerimientoParameter = fcTipoRequerimiento != null ?
                new ObjectParameter("fcTipoRequerimiento", fcTipoRequerimiento) :
                new ObjectParameter("fcTipoRequerimiento", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Indicadores_Insertar", fcTipoRequerimientoParameter);
        }
    
        public virtual ObjectResult<sp_Indicadores_Lista_Result> sp_Indicadores_Lista()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Indicadores_Lista_Result>("sp_Indicadores_Lista");
        }
    
        public virtual int sp_ListadoPaisPrueba()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ListadoPaisPrueba");
        }
    
        public virtual ObjectResult<Nullable<int>> sp_UsuarioMaestro_ObtenerIdAreaAsignada(Nullable<int> piIDusuario)
        {
            var piIDusuarioParameter = piIDusuario.HasValue ?
                new ObjectParameter("piIDusuario", piIDusuario) :
                new ObjectParameter("piIDusuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_UsuarioMaestro_ObtenerIdAreaAsignada", piIDusuarioParameter);
        }
    
        public virtual ObjectResult<sp_Usuarios_Maestro_Lista_Result> sp_Usuarios_Maestro_Lista()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Usuarios_Maestro_Lista_Result>("sp_Usuarios_Maestro_Lista");
        }
    
        public virtual ObjectResult<sp_Usuarios_Maestro_PorArea_Result> sp_Usuarios_Maestro_PorArea(Nullable<int> piIDArea)
        {
            var piIDAreaParameter = piIDArea.HasValue ?
                new ObjectParameter("piIDArea", piIDArea) :
                new ObjectParameter("piIDArea", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Usuarios_Maestro_PorArea_Result>("sp_Usuarios_Maestro_PorArea", piIDAreaParameter);
        }
    
        public virtual ObjectResult<sp_Usuarios_Maestro_PorIdUsuario_Result> sp_Usuarios_Maestro_PorIdUsuario(Nullable<int> piIDUsuario)
        {
            var piIDUsuarioParameter = piIDUsuario.HasValue ?
                new ObjectParameter("piIDUsuario", piIDUsuario) :
                new ObjectParameter("piIDUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Usuarios_Maestro_PorIdUsuario_Result>("sp_Usuarios_Maestro_PorIdUsuario", piIDUsuarioParameter);
        }
    
        public virtual ObjectResult<sp_Usuarios_Maestro_PorIdUsuarioSupervisor_Result> sp_Usuarios_Maestro_PorIdUsuarioSupervisor(Nullable<int> piIDUsuarioSupervisor)
        {
            var piIDUsuarioSupervisorParameter = piIDUsuarioSupervisor.HasValue ?
                new ObjectParameter("piIDUsuarioSupervisor", piIDUsuarioSupervisor) :
                new ObjectParameter("piIDUsuarioSupervisor", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Usuarios_Maestro_PorIdUsuarioSupervisor_Result>("sp_Usuarios_Maestro_PorIdUsuarioSupervisor", piIDUsuarioSupervisorParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_Usuarios_Maestros_ObtenerIdUsuario(string pcNombreCorto)
        {
            var pcNombreCortoParameter = pcNombreCorto != null ?
                new ObjectParameter("pcNombreCorto", pcNombreCorto) :
                new ObjectParameter("pcNombreCorto", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_Usuarios_Maestros_ObtenerIdUsuario", pcNombreCortoParameter);
        }
    
        public virtual ObjectResult<sp_Areas_Lista_Result> sp_Areas_Lista()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Areas_Lista_Result>("sp_Areas_Lista");
        }
    
        public virtual int sp_Requerimiento_Maestro_Actualizar(Nullable<int> piIDUsuario, Nullable<int> piIDRequerimiento, string pcTituloRequerimiento, string pcDescripcionRequerimiento, Nullable<byte> piIDEstadoRequerimiento, Nullable<System.DateTime> fdFechaAsignacion, Nullable<int> pifiIDUsuarioAsignado, Nullable<int> piTiempodeDesarrollo, Nullable<byte> pifiTipoRequerimiento, Nullable<int> piIDApp)
        {
            var piIDUsuarioParameter = piIDUsuario.HasValue ?
                new ObjectParameter("piIDUsuario", piIDUsuario) :
                new ObjectParameter("piIDUsuario", typeof(int));
    
            var piIDRequerimientoParameter = piIDRequerimiento.HasValue ?
                new ObjectParameter("piIDRequerimiento", piIDRequerimiento) :
                new ObjectParameter("piIDRequerimiento", typeof(int));
    
            var pcTituloRequerimientoParameter = pcTituloRequerimiento != null ?
                new ObjectParameter("pcTituloRequerimiento", pcTituloRequerimiento) :
                new ObjectParameter("pcTituloRequerimiento", typeof(string));
    
            var pcDescripcionRequerimientoParameter = pcDescripcionRequerimiento != null ?
                new ObjectParameter("pcDescripcionRequerimiento", pcDescripcionRequerimiento) :
                new ObjectParameter("pcDescripcionRequerimiento", typeof(string));
    
            var piIDEstadoRequerimientoParameter = piIDEstadoRequerimiento.HasValue ?
                new ObjectParameter("piIDEstadoRequerimiento", piIDEstadoRequerimiento) :
                new ObjectParameter("piIDEstadoRequerimiento", typeof(byte));
    
            var fdFechaAsignacionParameter = fdFechaAsignacion.HasValue ?
                new ObjectParameter("fdFechaAsignacion", fdFechaAsignacion) :
                new ObjectParameter("fdFechaAsignacion", typeof(System.DateTime));
    
            var pifiIDUsuarioAsignadoParameter = pifiIDUsuarioAsignado.HasValue ?
                new ObjectParameter("pifiIDUsuarioAsignado", pifiIDUsuarioAsignado) :
                new ObjectParameter("pifiIDUsuarioAsignado", typeof(int));
    
            var piTiempodeDesarrolloParameter = piTiempodeDesarrollo.HasValue ?
                new ObjectParameter("piTiempodeDesarrollo", piTiempodeDesarrollo) :
                new ObjectParameter("piTiempodeDesarrollo", typeof(int));
    
            var pifiTipoRequerimientoParameter = pifiTipoRequerimiento.HasValue ?
                new ObjectParameter("pifiTipoRequerimiento", pifiTipoRequerimiento) :
                new ObjectParameter("pifiTipoRequerimiento", typeof(byte));
    
            var piIDAppParameter = piIDApp.HasValue ?
                new ObjectParameter("piIDApp", piIDApp) :
                new ObjectParameter("piIDApp", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Requerimiento_Maestro_Actualizar", piIDUsuarioParameter, piIDRequerimientoParameter, pcTituloRequerimientoParameter, pcDescripcionRequerimientoParameter, piIDEstadoRequerimientoParameter, fdFechaAsignacionParameter, pifiIDUsuarioAsignadoParameter, piTiempodeDesarrolloParameter, pifiTipoRequerimientoParameter, piIDAppParameter);
        }
    
        public virtual int sp_Requerimiento_Bitacoras_Agregar(Nullable<int> piIDUsuario, Nullable<int> piIDRequerimiento, Nullable<int> piIDUsuarioSolicitante, string fcComentario, Nullable<int> piIDApp, Nullable<int> piIDEstado)
        {
            var piIDUsuarioParameter = piIDUsuario.HasValue ?
                new ObjectParameter("piIDUsuario", piIDUsuario) :
                new ObjectParameter("piIDUsuario", typeof(int));
    
            var piIDRequerimientoParameter = piIDRequerimiento.HasValue ?
                new ObjectParameter("piIDRequerimiento", piIDRequerimiento) :
                new ObjectParameter("piIDRequerimiento", typeof(int));
    
            var piIDUsuarioSolicitanteParameter = piIDUsuarioSolicitante.HasValue ?
                new ObjectParameter("piIDUsuarioSolicitante", piIDUsuarioSolicitante) :
                new ObjectParameter("piIDUsuarioSolicitante", typeof(int));
    
            var fcComentarioParameter = fcComentario != null ?
                new ObjectParameter("fcComentario", fcComentario) :
                new ObjectParameter("fcComentario", typeof(string));
    
            var piIDAppParameter = piIDApp.HasValue ?
                new ObjectParameter("piIDApp", piIDApp) :
                new ObjectParameter("piIDApp", typeof(int));
    
            var piIDEstadoParameter = piIDEstado.HasValue ?
                new ObjectParameter("piIDEstado", piIDEstado) :
                new ObjectParameter("piIDEstado", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Requerimiento_Bitacoras_Agregar", piIDUsuarioParameter, piIDRequerimientoParameter, piIDUsuarioSolicitanteParameter, fcComentarioParameter, piIDAppParameter, piIDEstadoParameter);
        }
    }
}
