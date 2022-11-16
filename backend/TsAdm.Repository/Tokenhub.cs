using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TsAdm.Repository
{
    public class Tokenhub
    {
        const string file = @"D:\dotNetFhwToken.txt";//"token.txt";
        //private string token;
        public Tokenhub()
        {
            if (!File.Exists(file)) createFile();
        }

        private void createFile()
        {
            try
            {
                FileStream fileStream = new FileStream(file, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Close();
            }catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
        
        private string readFileToken(string nid)
        {
            string res = "";
            try
            {
                FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream, System.Text.Encoding.UTF8);
                for (string s = streamReader.ReadLine(); s != null; s = streamReader.ReadLine())
                {
                    bool flag = false;
                    string token = "", id = "";
                    foreach (char ch in s)
                    {
                        if (!flag) token += ch;
                        else id += ch;
                        if (ch == '@') flag = true;
                    }
                    if (nid.Equals(id)) res = token;
                }
                fileStream.Close();
                streamReader.Close();
            }
            catch(Exception err){
                Console.WriteLine(err.Message);
            }
            return res;
        }

        private string readFileId(string nToken)
        {
            string res = "";
            try
            {
                FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream, System.Text.Encoding.UTF8);
                for (string s = streamReader.ReadLine(); s != null; s = streamReader.ReadLine())
                {
                    bool flag = false;
                    string token = "", id = "";
                    foreach (char ch in s)
                    {
                        if (!flag) token += ch;
                        else id += ch;
                        if (ch == '@') flag = true;
                    }
                    if (nToken.Equals(token)) res = id;
                }
                fileStream.Close();
                streamReader.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return res;
        }

        private string clearByToken(string nToken)
        {
            string res = "", nFile = "";
            try
            {
                FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream, System.Text.Encoding.UTF8);
                for (string s = streamReader.ReadLine(); s != null; s = streamReader.ReadLine())
                {
                    bool flag = false;
                    string token = "", id = "";
                    foreach (char ch in s)
                    {
                        if (!flag) token += ch;
                        else id += ch;
                        if (ch == '@') flag = true;
                    }
                    if (!nToken.Equals(token)) nFile = nFile + System.Environment.NewLine + token + "@" + id;
                }
                fileStream.Close();
                streamReader.Close();
                File.WriteAllText(file, nFile);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return res;
        }

        private void writeFile(string s,string id)
        {
            try
            {
                string res = "";
                FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream, System.Text.Encoding.UTF8);
                for (string st = streamReader.ReadLine(); st != null; st = streamReader.ReadLine())
                {
                    foreach (char ch in st)
                    {
                        res += ch;
                    }
                    res += System.Environment.NewLine;
                }
                fileStream.Close();
                streamReader.Close();
                res = res + System.Environment.NewLine + s + '@' + id;
                File.WriteAllText(file, res);
            }catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        public string getToken(string id)
        {
            string token = readFileToken(id);
            return token;
        }

        private void setToken(string s,string id) { writeFile(s,id); }

        public void clearToken(string token) { clearByToken(token); }

        public string generateToken(string id)
        {
            RNGCryptoServiceProvider csp = new RNGCryptoServiceProvider();
            byte[] byteCsp = new byte[10];
            csp.GetBytes(byteCsp);
            string rdToken = BitConverter.ToString(byteCsp);
            setToken(rdToken,id);
            return rdToken;
        }

        public string getId(string token)
        {
            try
            {
                string id = readFileId(token);
                if (id.Equals("")) throw new Exception("UNFOUND");
                else return id;
            }catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return "ERROR";
        }

    }
}
