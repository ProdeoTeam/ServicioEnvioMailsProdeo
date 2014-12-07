using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioEnvioMailsProdeo
{
    class ProcesoEnvioMails
    {
        private AccesoDatos datos = new AccesoDatos();

        /// <summary>
        /// Genera los mails de alertas y los inserta en la tabla de mails
        /// </summary>
        /// <returns></returns>
        public Boolean GenerarMails()
        {
            Boolean procesoOk = true;
            List<Mails> listaDeMails = new List<Mails>();
            List<Proyectos> listaDeProyectos = new List<Proyectos>();
            List<Tareas> listaDeTareas = new List<Tareas>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                //---------------------------------------------------------
                // Creamos los mail para proyectos
                listaDeProyectos = datos.obtenerProyectosRequierenAlerta();

                foreach (Proyectos unProyecto in listaDeProyectos)
                {
                    this.CrearMailParaProyecto(unProyecto);
                }
                //---------------------------------------------------------
                // Creamos los mail para Tareas
                listaDeTareas = datos.obtenerTareasRequierenAlerta();

                foreach (Tareas unaTarea in listaDeTareas)
                {
                    this.CrearMailParaTarea(unaTarea);
                }
                //---------------------------------------------------------

            }
            catch (Exception)
            {
                procesoOk = false;
            }

            return procesoOk;
        }

        #region "Codigo de Generacion de Emails"

        private Mails CrearMailParaProyecto(Proyectos proy)
        {
            Mails mail = new Mails();
            try
            {
                //-------------------------------
                // Destinatarios
                foreach (ParticipantesProyectos unParticipante in proy.ParticipantesProyectos)
                {
                    //Recorremos los participantes y agregamos los mails en los destinatarios separados por ;
                    mail.destinatarios += datos.obtenerMailDeUsuario(unParticipante.idUsuario) + ";";
                }

                if (mail.destinatarios.Length > 0)
                {
                    //Quitamos el ultimo ; que esta de mas
                    mail.destinatarios = mail.destinatarios.Substring(0, mail.destinatarios.Length - 1);
                }
                //-------------------------------

                //-------------------------------
                // Cuerpo de mail


                mail.detalle = "<html>" 
                            + "    <header>"
                            + "    </header>"
                            + "    <body>"
                            + "         <h3>Tu proyecto esta por vencerse</h3> </br>"
                            + "         Querido usuario, tu proyecto " + proy.Nombre + " se vencera el dia " + proy.FechaVencimiento.ToShortDateString()
                            + "         a las " + proy.FechaVencimiento.ToShortTimeString()
                            + "    </body>"
                            + "</html>";
                //-------------------------------

                //-------------------------------
                // Asunto de mail
                mail.asunto = "Alerta PRODEO de proyecto " + proy.Nombre;
                //-------------------------------

                //-------------------------------
                // Datos de mail
                mail.enviado = "P";
                mail.idProyecto = proy.idProyecto;
                mail.idModulo = 0;
                mail.idTarea = 0;
                                
                //-------------------------------

            }
            catch (Exception)
            {
                mail = null;
            }
            return mail;
        }

        private Mails CrearMailParaTarea(Tareas tarea)
        {
            Mails mail = new Mails();
            Proyectos proy = new Proyectos();
            List<string> mailsAdministradores = new List<string>();
            try
            {
                //-------------------------------
                // Destinatarios
                
                //Participantes
                foreach (ParticipantesTareas unParticipante in tarea.ParticipantesTareas)
                {
                    //Recorremos los participantes y agregamos los mails en los destinatarios separados por ;
                    mail.destinatarios += datos.obtenerMailDeUsuario(unParticipante.idUsuario) + ";";
                }

                //Administradores del proyecto
                Modulos modulo = new Modulos();
                modulo = datos.cargarDatosModulo(tarea.idModulo);
                mailsAdministradores = datos.ObtenerMailsAdministradoresProyecto(modulo.idProyecto);
                foreach (string unMail in mailsAdministradores)
                {
                    //Recorremos los participantes y agregamos los mails en los destinatarios separados por ;
                    mail.destinatarios += unMail + ";";
                }

                if (mail.destinatarios.Length > 0)
                {
                    //Quitamos el ultimo ; que esta de mas
                    mail.destinatarios = mail.destinatarios.Substring(0, mail.destinatarios.Length - 1);
                }
                //-------------------------------

                //-------------------------------
                // Cuerpo de mail


                mail.detalle = "<html>"
                            + "    <header>"
                            + "    </header>"
                            + "    <body>"
                            + "         <h3>Tu proyecto esta por vencerse</h3> </br>"
                            + "         Querido usuario, tu proyecto " + proy.Nombre + " se vencera el dia " + proy.FechaVencimiento.ToShortDateString()
                            + "         a las " + proy.FechaVencimiento.ToShortTimeString()
                            + "    </body>"
                            + "</html>";
                //-------------------------------

                //-------------------------------
                // Asunto de mail
                mail.asunto = "Alerta PRODEO de proyecto " + proy.Nombre;
                //-------------------------------

                //-------------------------------
                // Datos de mail
                mail.enviado = "P";
                mail.idProyecto = proy.idProyecto;
                mail.idModulo = 0;
                mail.idTarea = 0;

                //-------------------------------

            }
            catch (Exception)
            {
                mail = null;
            }
            return mail;
        }


        #endregion

        public Boolean EnviarMailsPendientes()
        {
            Boolean procesoOk = true;
            EnvioMailService envioMails = new EnvioMailService();
            try
            {
                
                List<Mails> listaDeMails = new List<Mails>();
                AccesoDatos datos = new AccesoDatos();
                listaDeMails = datos.obtenerMailsPendientesEnvio();

                foreach (Mails unMail in listaDeMails)
                {
                    envioMails.enviarMail(unMail);
                }
            }
            catch (Exception)
            {
                procesoOk = false;
            }

            return procesoOk;
        }
    }
}
