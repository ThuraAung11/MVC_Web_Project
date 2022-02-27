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
    public class FavController : BaseController
    {
        FavBll bll = new FavBll();
        // GET: Admin/Fav
        public ActionResult UpdateFav() 
        {
            FavDTO model = new FavDTO();
            model = bll.GetFav();
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateFav(FavDTO model) 
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.FavImage!=null)
                {
                    string Favname = "";
                    HttpPostedFileBase postedfile = model.FavImage;
                    Bitmap FavImage = new Bitmap(postedfile.InputStream);
                    Bitmap ImageResize = new Bitmap(FavImage,80,80);
                    string ext = Path.GetExtension(postedfile.FileName);
                    if (ext==".ico" || ext==".jpg" || ext==".jpeg" || ext==".png")
                    {
                        string Favunique = Guid.NewGuid().ToString();
                        Favname = Favunique + postedfile.FileName;
                        ImageResize.Save(Server.MapPath("~/Areas/Admin/Content/FavImage/"+Favname));
                        model.Fav = Favname;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionsError;
                    }
                }
                if (model.LogoImage != null)
                {
                    string Logoname = "";
                    HttpPostedFileBase postedfile = model.LogoImage;
                    Bitmap LogoImage = new Bitmap(postedfile.InputStream);
                    Bitmap ImageResize = new Bitmap(LogoImage, 100, 100);
                    string ext = Path.GetExtension(postedfile.FileName);
                    if (ext == ".ico" || ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                    {
                        string logounique = Guid.NewGuid().ToString();
                        Logoname = logounique + postedfile.FileName;
                        ImageResize.Save(Server.MapPath("~/Areas/Admin/Content/FavImage/" + Logoname));
                        model.Logo = Logoname;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionsError;
                    }
                }
                FavDTO dto = new FavDTO();
                dto = bll.UpdateFav(model);
                if (model.LogoImage!=null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/FavImage/" + dto.Logo)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/FavImage/" + dto.Logo));
                    }
                }
                if (model.FavImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/FavImage/" + dto.Fav)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/FavImage/" + dto.Fav));
                    }
                }
                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }
            return View(model);
        }
    }
}