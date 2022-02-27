using Bll;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        LayoutBLL bll = new LayoutBLL();
        GeneralBLL generalbll = new GeneralBLL();
        PostBll pbll = new PostBll();
        ContactBLL cbll = new ContactBLL();
        // GET: Home
        public ActionResult Index()
        { 
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = bll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO general = new GeneralDTO();
            general = generalbll.GetAllPost();
            return View(general);
        }
        public ActionResult CategoryPostList(string CategoryName)
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = bll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = generalbll.GetCategoryPostList(CategoryName);
            return View(dto);
        }
        public ActionResult PostDetail(int ID)
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = bll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = generalbll.GetPostDetailItemWithID(ID);
            return View(dto);
        }
        [HttpPost]
        public ActionResult PostDetail(GeneralDTO model) 
        {
            if (model.Email!=null && model.Message!= null && model.Name!=null)
            {
                if (pbll.AddComment(model))
                {
                    ViewData["CommentState"] = "SUccess";
                    ModelState.Clear();
                   
                }
                else
                    ViewData["CommentState"] = "Error";
            }
            else
            {
                ViewData["CommentState"] = "Error";
            }
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = bll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            
            model = generalbll.GetPostDetailItemWithID(model.PostID);
            return View(model);
        }
        [Route("contactus")]
        public ActionResult ContactUs() 
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = bll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = generalbll.GetContactItem();
            return View(dto);
        }
        [Route("contactus")]
        [HttpPost]
        public ActionResult ContactUs(GeneralDTO model)
        {
            if (model.Name!=null && model.Subject!=null && model.Email!=null && model.Message!=null)
            {
                if (cbll.AddContact(model))
                {
                    ViewData["CommentState"] = "SUccess";
                    ModelState.Clear();
                }
                else
                    ViewData["CommentState"] = "Error";
            }
            else
            {
                ViewData["CommentState"] = "Error";
            }
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = bll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = generalbll.GetContactItem();
            return View(dto);
        }
        [Route("search")]
        [HttpPost]
        public ActionResult Search(GeneralDTO model)
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = bll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = generalbll.GetSearchPosts(model.SearchText);
            return View(dto);
        }
    }
}