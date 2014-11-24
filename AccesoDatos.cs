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
        public List<Mails> obtenerMailsPendientesEnvio(string email)
        {
            List<Mails> query = new List<Mails>();
            prodeoEntities prodeoContext = new prodeoEntities();
            query = (from u in prodeoContext.Mails
                         where u.enviado == "P"
                         select u).ToList();

            return query;
        }
        public List<Mails> obtenerMailsPendientesEnvio(string email)
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
