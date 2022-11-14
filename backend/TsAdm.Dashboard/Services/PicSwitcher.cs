using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TsAdm.Dashboard.Services
{
    public class PicSwitcher
    {
        public void storePic(string path= @"D:\cppPractice\cf\Star_Tear.jpeg")
        {

            FileStream fs = new System.IO.FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] photo = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();

            string res = picToUrl(photo, "jpeg", "starTear");
            Console.WriteLine(res);

        }

        /// <summary>
        /// picture stream to url
        /// </summary>
        /// <param name="photo">picture stream</param>
        /// <param name="type">picture type</param>
        /// <param name="name">picture name</param>
        /// <returns>url if succeed, -1 otherwise</returns>
        public string picToUrl(byte[] photo, string type, string name)
        {
            try
            {
                string tPath = @"D:\dotnetProject\img\" + name + "." + type;
                if (File.Exists(tPath)) throw new Exception("CAN NOT OVERWRITTEN!");
                BinaryWriter bw = new BinaryWriter(File.Open(tPath, FileMode.OpenOrCreate));
                bw.Write(photo);
                bw.Close();
                string url = "http://localhost:12548/getImg/" + type + "/" + name;
                return url;
            }catch(Exception err)
            {
                Console.WriteLine(err.Message);
                return "-1";
            }
        }

    }
}