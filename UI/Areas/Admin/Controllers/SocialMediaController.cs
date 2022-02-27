using Bll;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class SocialMediaController : BaseController
    {
        SocialMediaBll bll = new SocialMediaBll();
        // GET: Admin/SocialMedia
        public ActionResult AddSocialMedia() 
        {
            SocialMediaDTO model = new SocialMediaDTO();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddSocialMedia(SocialMediaDTO smodel) 
        {
            if (smodel.SocialImage==null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                HttpPostedFileBase postedfile = smodel.SocialImage;
                Bitmap SocialMedia = new Bitmap(postedfile.InputStream);
                string ext = Path.GetExtension(postedfile.FileName);
                string filename = "";
                if (ext==".jpg" || ext==".jpeg" || ext==".png" || ext==".gif")
                {
                    string uniquenumber = Guid.NewGuid().ToString();
                    filename = uniquenumber + postedfile.FileName;
               
                    SocialMedia.Save(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + filename));
                    smodel.ImagePath = filename;
                    if (bll.AddSocialMedia(smodel))
                    {
                        ViewBag.ProcessState = General.Messages.Addsuccess;
                        smodel = new SocialMediaDTO();
                        ModelState.Clear();
                    }
                    else
                        ViewBag.ProcessState = General.Messages.GeneralError;
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.ExtensionsError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(smodel);
        }
        public ActionResult SocialMeadiaList()
        {
            List<SocialMediaDTO> dtoSocial = new List<SocialMediaDTO>();
            dtoSocial = bll.GetSocialMedia();
            return View(dtoSocial);
        }
        public ActionResult UpdateSocialMedia(int ID)
        {
            SocialMediaDTO dto = bll.GetSocialMediaWithID(ID);
            return View(dto);
        }

        [HttpPost]
        public ActionResult UpdateSocialMedia(SocialMediaDTO mediaDTO) 
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (mediaDTO.SocialImage!=null)
                {
                    HttpPostedFileBase postedfile = mediaDTO.SocialImage;
                    Bitmap SocialMedia = new Bitmap(postedfile.InputStream);
                    string ext = Path.GetExtension(postedfile.FileName);
                    string filename = "";
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        string uniquenumber = Guid.NewGuid().ToString();
                        filename = uniquenumber + postedfile.FileName;

                        SocialMedia.Save(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + filename));
                        mediaDTO.ImagePath = filename;
                    }
                }
                string oldimage = bll.UpdateSocialMedia(mediaDTO);
                if (mediaDTO.ImagePath!=null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + oldimage)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + oldimage));
                    }
                }
                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }
            return View(mediaDTO);
        }
        public JsonResult DeleteSocialMedia(int ID) 
        {
            string oldimage = bll.DeleteSocialMedia(ID);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + oldimage)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + oldimage));
            }
            return Json("");
        }
    }
}