using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

    namespace ServicioEnvioMailsProdeo
    {
        public class Service1 : ServiceBase
        {

            private Proceso oProceso = new Proceso();
            private System.Threading.Thread thProceso;
            private System.Threading.ThreadStart thInicio;

            // El punto de entrada principal para el proceso
            [MTAThread()]
            internal static void Main()
            {
#if ( DEBUG)
                Service1 DebugService = new Service1();
                DebugService.OnStart(null);
#else
        System.ServiceProcess.ServiceBase[] ServicesToRun;
        ServicesToRun = new System.ServiceProcess.ServiceBase() {new ProcFIX}();
        System.ServiceProcess.ServiceBase.Run(ServicesToRun);
#endif
            }

            protected override void OnStart(string[] args)
            {
#if ( DEBUG)
                oProceso.Iniciar();
#else
        thInicio = oProceso.Iniciar;
        thProceso = new System.Threading.Thread(thInicio);
        thProceso.Start();
#endif
            }

            protected override void OnStop()
            {
                oProceso.Finalizar();
            }

        }
        public class Proceso
        {
            private Timer oTimer;
            private bool bProcesando;

            public void Iniciar()
            {
                string sMsgErrorConexion = String.Empty;

                try
                {
                    //Se verifica la conexion con la BD
                    //if( mloUtil.SetearConeccion(sMsgErrorConexion) ){
                    bProcesando = false;

#if ( DEBUG)
                    ProcesoGral();
#else
                //---------------------------------------------------------------------------------------------------
                //Inicialmente se crea el timer sin intervalo.
                //Luego que se ejecute por primera vez el evento TimeIntervalElapsed, se setea el intervalo del timer
                //---------------------------------------------------------------------------------------------------
                oTimer = new Timer();
                oTimer.Enabled = true;
                oTimer.AutoReset = true;
                oTimer.Start();
                //---------------------------------------------------------------------------------------------------
#endif

                    //}else{
                    //    //Log.GrabarLog("Proceso.Iniciar => No se pudo conectar: '" + sMsgErrorConexion + "'. Revise la conexion con la base de datos y reinicie el servicio.");

                    //}

                }
                catch (Exception ex)
                {
                    //Log.GrabarLog("Proceso.Iniciar => " + ex.Message);

                }

            }

            public void Finalizar()
            {
                //Se detiene el timer
                if (oTimer == null)
                {
                }
                else
                {
                    oTimer.Enabled = false;
                    oTimer.Stop();
                }
            }

            private void TimeIntervalElapsed(object sender, ElapsedEventArgs e)
            {
                try
                {

                    if (!bProcesando)
                    {

                        //Se marca el inicio del proceso de alta de facturaciones
                        bProcesando = true;

                        //Se setea el intervalo del timer 2 minutos
                        this.oTimer.Interval = System.TimeSpan.FromMinutes(2).TotalMilliseconds;

                        // Se procesan las facturaciones
                        ProcesoGral();

                        //Se marca la finalizacion del procesamiento de las facturaciones
                        bProcesando = false;

                    }

                }
                catch (Exception ex)
                {
                    //Log.GrabarLog("TimeIntervalElapsed => " + ex.Message);

                }

            }

            private void ProcesoGral()
            {
                try
                {
                    //Codigo que del proceso
                    ProcesoEnvioMails proceso = new ProcesoEnvioMails();
                    proceso.GenerarMails();
                    proceso.EnviarMailsPendientes();
                }
                catch (Exception ex)
                {

                }

            }


        }

    }

