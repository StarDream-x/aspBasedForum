using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TsAdm.Dashboard.Services
{
    public class PicSwitcher
    {
        public void storePic(string path= @"D:\cppPractice\cf\Star_Dream.jpg")
        {

            FileStream fs = new System.IO.FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] photo = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();

            string nPath = @"D:\dotnetProject\test.jpg";
            BinaryWriter bw = new BinaryWriter(File.Open(nPath, FileMode.OpenOrCreate));
            bw.Write(photo);
            bw.Close();
            

        }


        public void picToUrl(byte[] photo,string url)
        {
            try
            {
                BinaryWriter bw = new BinaryWriter(File.Open(url, FileMode.OpenOrCreate));
                bw.Write(photo);
                bw.Close();
            }catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

    }
}