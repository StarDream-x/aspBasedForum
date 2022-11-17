using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using TsAdm.Dashboard.RequestBodies;
using TsAdm.Dashboard.Services;
using TsAdm.Repository;

namespace TsAdm.Dashboard.Controllers
{
    public class ContentController : ApiController
    {
        private ContentService contentService = new ContentService();
        private PostService postService = new PostService();

        [HttpGet]
        [Route("content")]
        public IHttpActionResult getContentAbstracts(string prevRequest)
        {
            long prevRequestLong;
            if (prevRequest == null)
            {
                prevRequestLong = 0;
            }
            else
            {
                prevRequestLong = long.Parse(prevRequest);
            }
            List<ContentAbstract> contentAbstracts = contentService.getContents(prevRequestLong);

            string requestId = (prevRequestLong + contentAbstracts.Count).ToString();
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("requestId", requestId);
            result.Add("contents", contentAbstracts);
            return Ok(result);
        }

        [HttpGet]
        [Route("content")]
        public IHttpActionResult getContentAbstracts(long? lastContentId, string userId)
        {
            long seriousLastContentId = (lastContentId == null) ? 0 : lastContentId.Value;
            List<ContentAbstract> contentAbstracts = contentService.getContentsByUserId(userId, seriousLastContentId);

            return Ok(contentAbstracts);
        }

        [HttpPost]
        [Route("content")]
        public IHttpActionResult postContent([FromBody] ContentPost request)
        {
            string currentUserId = getCurrentUserId();
            return Ok(postService.postContent(request, currentUserId));
        }

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

        [HttpGet]
        [Route("content/{id}")]
        public IHttpActionResult getContentById([FromUri] long id)
        {
            string currentUserId = getCurrentUserId();
            Content content = contentService.getContent(id, currentUserId);
            return Ok(content);
        }

        [HttpGet]
        [Route("content/{id}/interaction")]
        public IHttpActionResult getUserContentInteraction([FromUri] long id)
        {
            string currentUserId = getCurrentUserId() ;
            ContentInteraction contentInteraction = contentService.getContentInteraction(id, currentUserId);
            return Ok(contentInteraction);
        }

        [HttpPatch]
        [Route("content/{id}/interaction")]
        public IHttpActionResult patchContentInteraction([FromUri] long id, [FromBody] Dictionary<string, bool> body)
        {
            string currentUserId = getCurrentUserId();
            if (body.TryGetValue("like", out bool like))
            {
                contentService.userInteraction(id,like, null,currentUserId);
            }
            if (body.TryGetValue("favorite", out bool favorite))
            {
                contentService.userInteraction(id, null, favorite, currentUserId);
            }
            return Ok(contentService.getContentInteraction(id, currentUserId));
        }

        [HttpPatch]
        [Route("content/{content_id}/comment/{comment_id}")]
        public IHttpActionResult userCommentInteraction([FromUri]long content_id,[FromUri]long comment_id,[FromBody] Dictionary<string, bool> body)//存疑
        {
            string currentUserId = getCurrentUserId();
            if (body.TryGetValue("like", out bool like))
            {
                bool result = contentService.userCommentInteract(currentUserId, content_id, comment_id, like);
                Dictionary<string, bool> result2 = new Dictionary<string, bool>();
                result2.Add("like", result);
                return Ok(result2);
            }
            return BadRequest("Malformed request body");
        }
    }
}