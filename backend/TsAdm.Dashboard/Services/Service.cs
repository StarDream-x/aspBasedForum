using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TsAdm.Dashboard.RequestBodies;
using TsAdm.Dashboard.Services;

namespace TsAdm.Dashboard.Services
{
    public class Service
    {
        private MysqlService mysql = new MysqlService();

        public string register(Body body)
        {
            return body.id;
        }

        public bool login(Body body)
        {
            try
            {
                return mysql.login(body);
            }catch(Exception err)
            {
                return false;
            }
        }

        public bool checkId(Query query)
        {
            return true;
        }

        public Content[] getContents(Query query)
        {
            return null;
        }

        public Content getContents(int id)
        {
            return null;
        }

        public ContentInteraction getInteraction(int id)
        {
            return null;
        }

    }
}