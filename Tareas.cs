//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServicioEnvioMailsProdeo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tareas
    {
        public Tareas()
        {
            this.ParticipantesTareas = new HashSet<ParticipantesTareas>();
        }
    
        public int idTarea { get; set; }
        public int idModulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public System.DateTime FechaVencimiento { get; set; }
        public Nullable<System.DateTime> FechaFinalizacion { get; set; }
        public string AlertaPrevia { get; set; }
        public string DireccionGPS { get; set; }
        public string Prioridad { get; set; }
        public string Comentario { get; set; }
        public string Estado { get; set; }
        public Nullable<double> Tiempo { get; set; }
    
        public virtual Modulos Modulos { get; set; }
        public virtual ICollection<ParticipantesTareas> ParticipantesTareas { get; set; }
    }
}
