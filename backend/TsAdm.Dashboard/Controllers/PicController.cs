using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TsAdm.Repository;
using TsAdm.Dashboard.Services;
using TsAdm.Dashboard.RequestBodies;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace TsAdm.Dashboard.Controllers
{
    public class PicController : ApiController
    {

        //[HttpGet]
        //[Route("getImg/{pname}")]
        //public HttpResponseMessage getImg([FromBody] string pname)
        //{
        //    try
        //    {
        //        var imgPath = @"D:\dotnetProject\img\" + pname;
        //        //从图片中读取byte
        //        var imgByte = File.ReadAllBytes(imgPath);
        //        //从图片中读取流
        //        var imgStream = new MemoryStream(File.ReadAllBytes(imgPath));
        //        var resp = new HttpResponseMessage(HttpStatusCode.OK)
        //        {
        //            Content = new ByteArrayContent(imgByte)
        //            //或者
        //            //Content = new StreamContent(stream)
        //        };
        //        resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
        //        return resp;
        //    }
        //    catch (Exception err)
        //    {
        //        Console.WriteLine(err.Message);
        //        return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //    }

        //}

        [HttpGet]
        [Route("getImg/{type}/{pname}")]
        public HttpResponseMessage getImg([FromUri] string type, [FromUri] string pname)
        {
            try
            {
                //string pname = "pic.png";
                var imgPath = @"D:\dotnetProject\img\" + pname + "." + type;
                //从图片中读取byte
                var imgByte = File.ReadAllBytes(imgPath);
                //从图片中读取流
                var imgStream = new MemoryStream(File.ReadAllBytes(imgPath));
                var resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(imgByte)
                    //或者
                    //Content = new StreamContent(stream)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/"+type);
                return resp;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

        }

        [HttpGet]
        [Route("testPic")]
        public HttpResponseMessage testPic()
        {
            try
            {
                PicSwitcher switcher = new PicSwitcher();
                switcher.storePic();
                var resp = new HttpResponseMessage(HttpStatusCode.OK) { };
                return resp;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

        }

    }
}