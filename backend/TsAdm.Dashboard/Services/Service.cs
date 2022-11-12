using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TsAdm.Dashboard.RequestBodies;
using TsAdm.Dashboard.Services;
using TsAdm.Domain;

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
            try
            {
                return mysql.checkId(query);
                //return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public bool getContents(Query query)
        {
            try
            {
                return getContents(query);
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public bool getContentAbstract(Query query)
        {
            try
            {
                return getContentAbstract(query);
            }
            catch (Exception err)
            {
                return false;
            }
        }
        public Content getContents(int id)
        {
            try
            {
                return getContents(id);
            }
            catch (Exception err)
            {
                return null;
            }  
        }

        public ContentInteraction getInteraction(int id)
        {
            try
            {
                return getInteraction(id);
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public Comment getComments(int id)
        {
            try
            {
                return getComments(id);
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public User getUsers(int id)
        {
            try
            {
                return getUsers(id);
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public User getFollowings(int id)
        {
            try
            {
                return getFollowings(id);
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public User getFollowers(int id)
        {
            try
            {
                return getFollowers(id);
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public bool postContents(Body body)
        {
            try
            {
                return mysql.Content(body);
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public bool deleteContents(int id)
        {
            try
            {
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public bool deleteComments(int id)
        {
            try
            {
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
    }
}