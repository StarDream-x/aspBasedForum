﻿using System.Collections.Generic;
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

        [HttpGet]
        [Route("content")]
        public IHttpActionResult getContentAbstracts(string prevRequest)
        {
            long prevRequestLong = long.Parse(prevRequest);
            List<ContentAbstract> contentAbstracts = contentService.getContents(prevRequestLong);

            string requestId = (prevRequestLong + contentAbstracts.Count).ToString();
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("requestId", requestId);
            result.Add("contents", contentAbstracts);
            return Ok(result);
        }

        [HttpGet]
        [Route("content")]
        public IHttpActionResult getContentAbstracts(long lastContentId, string userId)
        {
            List<ContentAbstract> contentAbstracts = contentService.getContentsByUserId(userId, lastContentId);

            return Ok(contentAbstracts);
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
        public IHttpActionResult userLike([FromUri] long id,[FromBody] bool like)
        {
            string currentUserId = getCurrentUserId();
            ContentInteraction contentInteraction = contentService.userLike(id,like,currentUserId);
            return Ok(contentInteraction);
        }

        [HttpPatch]
        [Route("content/{id}/interaction")]
        public IHttpActionResult userFavorite([FromUri] long id, [FromBody] bool favorite)
        {
            string currentUserId = getCurrentUserId();
            ContentInteraction contentInteraction = contentService.userfavorite(id, favorite, currentUserId);
            return Ok(contentInteraction);
        }

        [HttpPatch]
        [Route("content/{content_id}/comment/{comment_id}")]
        public IHttpActionResult userCommentInteraction([FromUri]long conten_id,[FromUri]long comment_id,[FromBody] bool like)//存疑
        {
            string currentUserId = getCurrentUserId();
            bool result = contentService.userCommentInteract(currentUserId,conten_id,comment_id,like);
            return Ok(result);
        }
    }
}