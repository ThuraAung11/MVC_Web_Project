using Bll;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class MetaController : BaseController
    {
        // GET: Admin/Meta
        public ActionResult Index()
        {
            return View();
        }

        MetaBll bll = new MetaBll();
        public ActionResult AddMeta()
        {
            MetaDTO dTO = new MetaDTO();
            return View(dTO);
        }
        [HttpPost]
        public ActionResult AddMeta(MetaDTO metaDTO)
        {
            if (ModelState.IsValid)
            {
                if (bll.AddMeta(metaDTO))
                {
                    ViewBag.ProcessState = General.Messages.Addsuccess;
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            MetaDTO newmodel = new MetaDTO();
            return View(newmodel);
        }

        public ActionResult MetaList()
        {
            List<MetaDTO> model = new List<MetaDTO>();
            model = bll.GetData();
            return View(model);
        }
        public ActionResult UpdateMeta(int ID) 
        {
            MetaDTO model = new MetaDTO();
            model = bll.GetMetaID(ID);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateMeta(MetaDTO metaDTO) 
        {
            if (ModelState.IsValid)
            {
                if (bll.UpdateMeta(metaDTO))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(metaDTO);
        }
        public JsonResult DeleteMeta(int ID) 
        {
            bll.DeleteMeta(ID);
            return Json("");
        }
    }
}