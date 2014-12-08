using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
namespace ServicioEnvioMailsProdeo
{
    class AccesoDatos
    {
        public List<Mails> obtenerMailsPendientesEnvio()
        {
            List<Mails> query = new List<Mails>();
            prodeoEntities prodeoContext = new prodeoEntities();
            query = (from u in prodeoContext.Mails
                         where u.enviado == "P"
                         select u).ToList();

            return query;
        }
        /// <summary>
        /// Obtiene los proyectos que segun su configuracion, requieren alertar en el dia de la fecha.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public List<Proyectos> obtenerProyectosRequierenAlerta()
        {
            List<Proyectos> listaDeProyectos = new List<Proyectos>();
            prodeoEntities prodeoContext = new prodeoEntities();
            DateTime d1 = DateTime.Now.AddHours(1);
            
            listaDeProyectos = (from p in prodeoContext.Proyectos
                     where 
                     p.AlertaPrevia == "H-1" && p.FechaVencimiento <= Auxiliar.Alertas.hora_1
                    || p.AlertaPrevia == "H-2" && p.FechaVencimiento <= Auxiliar.Alertas.hora_2
                    || p.AlertaPrevia == "H-3" && p.FechaVencimiento <= Auxiliar.Alertas.hora_3
                    || p.AlertaPrevia == "H-4" && p.FechaVencimiento <= Auxiliar.Alertas.hora_4
                    || p.AlertaPrevia == "H-5" && p.FechaVencimiento <= Auxiliar.Alertas.hora_5
                    || p.AlertaPrevia == "D-1" && p.FechaVencimiento <= Auxiliar.Alertas.dia_1
                    || p.AlertaPrevia == "D-2" && p.FechaVencimiento <= Auxiliar.Alertas.dia_2
                    || p.AlertaPrevia == "D-3" && p.FechaVencimiento <= Auxiliar.Alertas.dia_3
                    || p.AlertaPrevia == "D-4" && p.FechaVencimiento <= Auxiliar.Alertas.dia_4
                    || p.AlertaPrevia == "D-5" && p.FechaVencimiento <= Auxiliar.Alertas.dia_5
                                select p).ToList();

            return listaDeProyectos;
        }

        /// <summary>
        /// Obtiene los proyectos que segun su configuracion, requieren alertar en el dia de la fecha.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public List<Tareas> obtenerTareasRequierenAlerta()
        {
            List<Tareas> listaDeTareas = new List<Tareas>();
            prodeoEntities prodeoContext = new prodeoEntities();
            listaDeTareas = (from t in prodeoContext.Tareas
                                where
                                t.AlertaPrevia == "H-1" && t.FechaVencimiento <= Auxiliar.Alertas.hora_1
                            || t.AlertaPrevia == "H-2" && t.FechaVencimiento <= Auxiliar.Alertas.hora_2
                            || t.AlertaPrevia == "H-3" && t.FechaVencimiento <= Auxiliar.Alertas.hora_3
                            || t.AlertaPrevia == "H-4" && t.FechaVencimiento <= Auxiliar.Alertas.hora_4
                            || t.AlertaPrevia == "H-5" && t.FechaVencimiento <= Auxiliar.Alertas.hora_5
                            || t.AlertaPrevia == "D-1" && t.FechaVencimiento <= Auxiliar.Alertas.dia_1
                            || t.AlertaPrevia == "D-2" && t.FechaVencimiento <= Auxiliar.Alertas.dia_2
                            || t.AlertaPrevia == "D-3" && t.FechaVencimiento <= Auxiliar.Alertas.dia_3
                            || t.AlertaPrevia == "D-4" && t.FechaVencimiento <= Auxiliar.Alertas.dia_4
                            || t.AlertaPrevia == "D-5" && t.FechaVencimiento <= Auxiliar.Alertas.dia_5
                                select t).ToList();

            return listaDeTareas;
        }
        
        public string obtenerMailDeUsuario(int idUsuario) 
        {
            string mail;
            prodeoEntities prodeoContext = new prodeoEntities();
            
            try
            {
                mail = (from u in prodeoContext.Usuarios
                                where u.idUsuario == idUsuario
                                select u.mail).First(); 
            }
            catch (Exception)
            {
                mail = "";
            }
            return mail;
        }

        public List<string> ObtenerMailsAdministradoresProyecto(int idProyecto) {
            List<string> mailsAdministradores = new List<string>();
            prodeoEntities prodeoContext = new prodeoEntities();

            try
            {
                mailsAdministradores = (from p in prodeoContext.ParticipantesProyectos 
                                        join u in prodeoContext.Usuarios on p.idUsuario equals u.idUsuario
                                        where p.permisosAdministrador == "A"
                                        && p.idProyecto == idProyecto
                                        select u.mail).ToList<string>(); 
            }
            catch (Exception)
            {
                mailsAdministradores = new List<string>();
            }
            return mailsAdministradores;
        }

        public Modulos cargarDatosModulo(int idModulo)
        {
            Modulos modulo = new Modulos();
            prodeoEntities prodeoContext = new prodeoEntities();

            try
            {
                modulo = prodeoContext.Modulos.Find(idModulo);
            }
            catch (Exception)
            {
                modulo = new Modulos();
            }
            return modulo;
        }

        public int insertarMail(Mails mail)
        {
            try
            {
                prodeoEntities prodeoContext = new prodeoEntities();
                
                prodeoContext.Mails.Add(mail);
                prodeoContext.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public int blanquearAlertaProyecto(int idProyecto)
        {
            prodeoEntities prodeoContext = new prodeoEntities();
            var proyecto = (from p in prodeoContext.Proyectos
                          where p.idProyecto == idProyecto
                          select p).First();

            proyecto.AlertaPrevia = "h-0";
            
            prodeoContext.SaveChanges();
            return 1;
        }

        public int blanquearAlertaTarea(int idTarea)
        {
            prodeoEntities prodeoContext = new prodeoEntities();
            var tarea = (from t in prodeoContext.Tareas
                            where t.idTarea == idTarea
                            select t).First();

            tarea.AlertaPrevia = "h-0";

            prodeoContext.SaveChanges();
            return 1;
        }

        public int marcarMailEnviado(int idMail)
        {
            prodeoEntities prodeoContext = new prodeoEntities();
            var mail = (from p in prodeoContext.Mails
                            where p.idMail == idMail
                            select p).First();

            mail.enviado = "E";

            prodeoContext.SaveChanges();
            return 1;
        }

    }
}
