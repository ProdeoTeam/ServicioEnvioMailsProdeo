using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioEnvioMailsProdeo
{
    class ProcesoEnvioMails
    {
        public List<Mails> GenerarMailsVencimientoModulo(string email)
        {
            List<Mails> query = new List<Mails>();
            prodeoEntities prodeoContext = new prodeoEntities();
            query = (from u in prodeoContext.Mails
                     where u.enviado == "P"
                     select u).ToList();

            return query;
        }
    }
}
