using Bll;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class CommentController : BaseController
    {
        PostBll bll = new PostBll();
        // GET: Admin/Comment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UnApprovedComment()
        {
            List<CommentDTO> commentList = new List<CommentDTO>();
            commentList = bll.GetAllComment();
            return View(commentList);
        }
        public ActionResult AllComment() 
        {
            List<CommentDTO> cmtList = new List<CommentDTO>();
            cmtList = bll.GetAllComments();
            return View(cmtList);
        }
        public ActionResult ApprovedComment(int ID) 
        {
            bll.ApprovedComment(ID);
            return RedirectToAction("UnApprovedComment","Comment");
        }
        public ActionResult ApprovedComment2(int ID)
        {
            bll.ApprovedComment(ID);
            return RedirectToAction("AllComment", "Comment");
        }
        public JsonResult DeleteComment(int ID) 
        {
            bll.DeleteComment(ID);
            return Json("");
        }
    }
}