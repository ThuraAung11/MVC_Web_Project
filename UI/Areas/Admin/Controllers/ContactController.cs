using Bll;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        ContactBLL bll = new ContactBLL();
        // GET: Admin/Contact
        public ActionResult UnreadMessages()
        {
            List<ContactDTO> dto = new List<ContactDTO>();
            dto=bll.GetAllUnreadMessages();
            return View(dto);
        }
        public ActionResult AllMessage() 
        {
            List<ContactDTO> dto = new List<ContactDTO>();
            dto = bll.GetAllMessages();
            return View(dto);
        }
        public ActionResult ReadMessage(int ID)
        {
            bll.ReadMessage(ID);
            return RedirectToAction("UnreadMessages");
        }
        public ActionResult ReadMessage2(int ID)
        {
            bll.ReadMessage(ID);
            return RedirectToAction("AllMessage");
        }
        public JsonResult DeleteMessage(int ID) 
        {
            bll.DeleteMessage(ID);
            return Json("");
        }
    }
}