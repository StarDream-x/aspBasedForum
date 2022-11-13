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
        private string token;
        public Tokenhub()
        {
            if (!File.Exists(file)) createFile();
            token = readFileToken();
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
        
        private string readFileToken()
        {
            string res = "";
            try
            {
                FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream, System.Text.Encoding.UTF8);
                for (string s = streamReader.ReadLine(); s != null; s = streamReader.ReadLine())
                {
                    foreach (char ch in s)
                    {
                        if (ch == '@') break;
                        res += ch;
                    }
                    break;
                }
                fileStream.Close();
                streamReader.Close();
                //File.WriteAllText(chosenText, res);
            }catch(Exception err){
                Console.WriteLine(err.Message);
            }
            return res;
        }

        private string readFileId()
        {
            string res = "";
            try
            {
                FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream, System.Text.Encoding.UTF8);
                for (string s = streamReader.ReadLine(); s != null; s = streamReader.ReadLine())
                {
                    bool flag = false;
                    foreach (char ch in s)
                    {
                        if(flag) res += ch;
                        if (ch == '@') flag = true;
                    }
                    break;
                }
                fileStream.Close();
                streamReader.Close();
                //File.WriteAllText(chosenText, res);
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
                s = s+'@'+id;
                File.WriteAllText(file, s);
            }catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        public string getToken()
        {
            token = readFileToken();
            return token;
        }

        private void setToken(string s,string id) { writeFile(s,id); }

        public void clearToken() { writeFile("",""); }

        public void generateToken(string id)
        {
            RNGCryptoServiceProvider csp = new RNGCryptoServiceProvider();
            byte[] byteCsp = new byte[10];
            csp.GetBytes(byteCsp);
            string rdToken = BitConverter.ToString(byteCsp);
            setToken(rdToken,id);
        }

        public string getId(string token)
        {
            try
            {
                if (token.Equals(getToken()))
                {
                    return readFileId();
                }
                else throw new Exception("WRONG TOKEN!");
            }catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return "ERROR";
        }

    }
}
