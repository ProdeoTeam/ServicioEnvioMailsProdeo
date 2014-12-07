using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioEnvioMailsProdeo
{
    namespace Auxiliar
    {
        public static class Alertas
        {
            public static DateTime hora_1 = DateTime.Now.AddHours(1);
            public static DateTime hora_2 = DateTime.Now.AddHours(2);
            public static DateTime hora_3 = DateTime.Now.AddHours(3);
            public static DateTime hora_4 = DateTime.Now.AddHours(4);
            public static DateTime hora_5 = DateTime.Now.AddHours(5);

            public static DateTime dia_1 = DateTime.Now.AddDays(1);
            public static DateTime dia_2 = DateTime.Now.AddDays(2);
            public static DateTime dia_3 = DateTime.Now.AddDays(3);
            public static DateTime dia_4 = DateTime.Now.AddDays(4);
            public static DateTime dia_5 = DateTime.Now.AddDays(5);
        }
    }
}
