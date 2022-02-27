using Bll;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class VideoController : BaseController
    {
        VideoBll bll = new VideoBll();
        // GET: Admin/Video
        public ActionResult AddVideo()
        {
            VideoDTO dto = new VideoDTO();
            return View(dto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddVideo(VideoDTO model) 
        {
            //https://www.youtube.com/watch?v=tIA_vrBDC1g
            //iframe width = "560" height = "315" src = "https://www.youtube.com/embed/tIA_vrBDC1g" title = "YouTube video player" frameborder = "0"  allowfullscreen ></ iframe >
            if (ModelState.IsValid)
            {
                string path = model.OriginalVideoPath.Substring(32);
                string mergeLink = "https://www.youtube.com/embed/";
                mergeLink += path;
                model.VideoPath = String.Format(@"<iframe width = ""350"" height = ""200"" src = ""{0}"" title = ""YouTube video player"" frameborder = ""0"" allow=""accelerometer; autoplay; clipboard - write; encrypted - media; gyroscope;""  allowfullscreen ></ iframe >", mergeLink);
                if (bll.AddVideo(model))
                {
                    ViewBag.ProcessState = General.Messages.Addsuccess;
                    ModelState.Clear();
                    model = new VideoDTO();
                }
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(model);
        }
        public ActionResult VideoList() 
        {
            List<VideoDTO> dto = new List<VideoDTO>();
            dto = bll.VideoList();
            return View(dto);
        }
        public ActionResult UpdateVideo(int ID) 
        {
            VideoDTO dto = new VideoDTO();
            dto = bll.GetVideoWithID(ID);
            return View(dto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateVideo(VideoDTO model) 
        {
            if (ModelState.IsValid)
            {
                string path = model.OriginalVideoPath.Substring(32);
                string mergeLink = "https://www.youtube.com/embed/";
                mergeLink += path;
                model.VideoPath = String.Format(@"<iframe width = ""350"" height = ""200"" src = ""{0}"" title = ""YouTube video player"" frameborder = ""0"" allow=""accelerometer; autoplay; clipboard - write; encrypted - media; gyroscope;""  allowfullscreen ></ iframe >", mergeLink);
                if (bll.UpdateVideo(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
            {
                ViewBag.ProccessState = General.Messages.EmptyArea;
            }
            return View(model);
        }
        public JsonResult DeleteVideo(int ID) 
        {
            bll.DeleteVideo(ID);
            return Json("");
        }
    }
}