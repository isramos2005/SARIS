using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrionCoreCableColor.DbConnection.CMContext
{
    public class Areas
    {

        public  Areas()
        {
            UsuariosPorArea = new HashSet<Usuarios_Maestro>();
        }


        [Key]
        public int fiIDArea { get; set; }

        public string fcNombreArea { get; set; }

        public int fiActivo { get; set; }

        public virtual ICollection<Usuarios_Maestro> UsuariosPorArea { get; set; }


    }
}