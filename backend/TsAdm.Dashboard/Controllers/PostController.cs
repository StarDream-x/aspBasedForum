using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using TsAdm.Dashboard.RequestBodies;
using TsAdm.Dashboard.Services;
using TsAdm.Repository;

namespace TsAdm.Dashboard.Controllers
{
    public class PostController : ApiController
    {
        PostService postService = new PostService();

        private string getCurrentUserId()
        {
            string value = HttpContext.Current.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(value) && value.StartsWith("token="))
            {
                string token = value.Substring(6);
                Tokenhub tokenhub = new Tokenhub();
                string id = tokenhub.getId(token);
                if (id == "ERROR")
                {
                    return string.Empty;
                }
                else
                {
                    return id;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        [HttpPost]
        [Route("content/{id}/comment")]
        public IHttpActionResult postComment([FromUri] long id,[FromBody] Dictionary<string, string> body)
        {
            string currentUserId = getCurrentUserId();
            if (body.TryGetValue("content", out string text))
            {
                postService.postComment(id, text, currentUserId);
            }
            return Ok();
        }
    }
}