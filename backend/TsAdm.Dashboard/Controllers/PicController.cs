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
using System.Threading.Tasks;
using System.Diagnostics;

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

        public const string imgRoot = @"D:\img";

        [HttpGet]
        [Route("img/{imgName}")]
        public HttpResponseMessage getImg([FromUri] string imgName)
        {
            try
            {
                //string pname = "pic.png";
                var imgPath = imgRoot + "\\" + imgName;
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
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
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

        [HttpPost]
        [Route("media")]
        public async Task<HttpResponseMessage> PostPicture()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new MultipartFormDataStreamProvider(imgRoot);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                string localFilePath = provider.FileData[0].LocalFileName;
                string localFileName = Path.GetFileName(localFilePath);
                string resultPath = "/img/" + localFileName;
                Dictionary<string, string> result = new Dictionary<string, string>();
                result.Add("fileUrl", resultPath);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

    }
}