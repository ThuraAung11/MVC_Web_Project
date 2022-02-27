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
    public class PostController : BaseController
    {
        PostBll bll = new PostBll();
        // GET: Admin/Post
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddPost() 
        {
            PostDTO dto = new PostDTO();
            dto.Categories = CategoryBll.GetCategoryForDropDown();
            return View(dto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPost(PostDTO model) 
        {
            if (model.PostImage[0]==null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if(ModelState.IsValid)
            {
                foreach (var item in model.PostImage)
                {
                    Bitmap image = new Bitmap(item.InputStream);
                    string ext = Path.GetExtension(item.FileName);
                    if (ext!=".png" && ext!=".jpeg" && ext!=".jpg")
                    {
                        model.Categories = CategoryBll.GetCategoryForDropDown();
                        return View(model);
                    }
                }

                List<PostImageDTO> imageList = new List<PostImageDTO>();
                foreach (var PostedFile in model.PostImage)
                {
                    Bitmap image = new Bitmap(PostedFile.InputStream);
                    Bitmap resizeimage = new Bitmap(image,750,422);
                    string ext = Path.GetExtension(PostedFile.FileName);
                    string filename = "";
                    string uniquenum = Guid.NewGuid().ToString();
                    filename = uniquenum + PostedFile.FileName;
                    resizeimage.Save(Server.MapPath("~/Areas/Admin/Content/PostImage/"+filename));
                    PostImageDTO dto = new PostImageDTO();
                    dto.ImagePath = filename;
                    imageList.Add(dto);
                }

                model.PostImages = imageList;
                if (bll.AddPost(model))
                {
                    ViewBag.ProcessState = General.Messages.Addsuccess;
                    ModelState.Clear();
                    model = new PostDTO();

                }
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            model.Categories = CategoryBll.GetCategoryForDropDown();
            return View(model);
        }
        public ActionResult PostList() 
        {
            CountDTO dto = new CountDTO();
            dto = bll.GetAllCount();
            ViewData["Counts"] = dto;
            List<PostDTO> ls = new List<PostDTO>();
            ls = bll.GetPost();
            return View(ls) ;
        }
        public ActionResult PostUpdate(int ID) 
        {
            PostDTO model = new PostDTO();
            model = bll.GetPostsWithID(ID);
            model.Categories = CategoryBll.GetCategoryForDropDown();
            model.isUpdate = true;

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PostUpdate(PostDTO model) 
        {
            IEnumerable<SelectListItem> selectLists = CategoryBll.GetCategoryForDropDown();
            if (ModelState.IsValid)
            {
                if (model.PostImage==null)
                {
                    ViewBag.ProcessState = General.Messages.ImageMissing;
                }
                else
                {
                    foreach (var item in model.PostImage)
                    {
                        Bitmap image = new Bitmap(item.InputStream);
                        string ext = Path.GetExtension(item.FileName);
                        if (ext != ".png" && ext != ".jpeg" && ext != ".jpg")
                        {
                            model.Categories = CategoryBll.GetCategoryForDropDown();
                            return View(model);
                        }
                    }

                    List<PostImageDTO> imageList = new List<PostImageDTO>();
                    foreach (var PostedFile in model.PostImage)
                    {
                        
                        Bitmap image = new Bitmap(PostedFile.InputStream);
                        Bitmap resizeimage = new Bitmap(image, 750, 422);
                        string ext = Path.GetExtension(PostedFile.FileName);
                        string filename = "";
                        string uniquenum = Guid.NewGuid().ToString();
                        filename = uniquenum + PostedFile.FileName;
                        resizeimage.Save(Server.MapPath("~/Areas/Admin/Content/PostImage/" + filename));
                        PostImageDTO dto = new PostImageDTO();
                        dto.ImagePath = filename;
                        imageList.Add(dto);
                    }

                    model.PostImages = imageList;
                    if (bll.UpdatePost(model))
                    {
                        ViewBag.ProcessState = General.Messages.UpdateSuccess;
                    }
                    else
                        ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
                ViewBag.ProcessState = General.Messages.EmptyArea;
            model = bll.GetPostsWithID(model.ID);
            model.Categories =selectLists;
            model.isUpdate = true;

            return View(model);
        }
        public JsonResult DeletePostImage(int ID)
        {
            string imgpath = bll.DeletePostImage(ID);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/PostImage/" + imgpath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/PostImage/" + imgpath));
            }
            return Json("");
        }
        public JsonResult DeletePost(int ID) 
        {
            List<PostImageDTO> imgList = bll.DeletePost(ID);
            foreach (var item in imgList)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/PostImage/" + item.ImagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/PostImage/" + item.ImagePath));
                }
            }
            return Json("");
        }
        public JsonResult Count() 
        {
            CountDTO dto = new CountDTO();
            dto = bll.GetCount();
            return Json(dto,JsonRequestBehavior.AllowGet);
        }
    }
    
}