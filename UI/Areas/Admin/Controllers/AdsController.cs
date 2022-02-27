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
    public class AdsController : BaseController
    {
        AdsBll bll = new Bll.AdsBll();
        // GET: Admin/Ads
        public ActionResult AddAds() 
        {
            AdsDTO model = new AdsDTO();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddAds(AdsDTO model) 
        {
            if (model.AdsImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                HttpPostedFileBase postedfile = model.AdsImage;
                Bitmap SocialMedia = new Bitmap(postedfile.InputStream);
                string ext = Path.GetExtension(postedfile.FileName);
                string filename = "";
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    string uniquenumber = Guid.NewGuid().ToString();
                    filename = uniquenumber + postedfile.FileName;

                    SocialMedia.Save(Server.MapPath("~/Areas/Admin/Content/AdsImage/" + filename));
                    model.ImagePath = filename;
                    if (bll.AddAds(model))
                    {
                        ViewBag.ProcessState = General.Messages.Addsuccess;
                        
                        ModelState.Clear();
                        model = new AdsDTO();
                    }
                    else
                        ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(model);
        }
        public ActionResult AdsList() 
        {
            List<AdsDTO> ls = new List<AdsDTO>();
            ls = bll.GetAds();
            return View(ls);
        }
        public ActionResult UpdateAds(int ID) 
        {
            AdsDTO model = new AdsDTO();
            model = bll.GetAdsWithID(ID);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateAds(AdsDTO model) 
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.AdsImage!=null)
                {
                    HttpPostedFileBase postedfile = model.AdsImage;
                    Bitmap SocialMedia = new Bitmap(postedfile.InputStream);
                    string ext = Path.GetExtension(postedfile.FileName);
                    string filename = "";
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        string uniquenumber = Guid.NewGuid().ToString();
                        filename = uniquenumber + postedfile.FileName;

                        SocialMedia.Save(Server.MapPath("~/Areas/Admin/Content/AdsImage/" + filename));
                        model.ImagePath = filename;
                    }
                }
              
                string oldImagePath = bll.UpdateAds(model);
                if (model.ImagePath!=null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdsImage/" + oldImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdsImage/" + oldImagePath));
                    }
                }
                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }
           
            return View(model);
        }
        public JsonResult DeleteAds(int ID) 
        {
            string imgpath = bll.DeleteAds(ID);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdsImage/" + imgpath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdsImage/" + imgpath));
            }
            return Json("");
        }
    }
}