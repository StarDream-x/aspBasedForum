using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TsAdm.Dashboard.RequestBodies;

namespace TsAdm.Dashboard.Services
{
    public class Service
    {

        public string register(Body body)
        {
            return body.id;
        }

        public string login(Body body)
        {
            return "-"+body.id;
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