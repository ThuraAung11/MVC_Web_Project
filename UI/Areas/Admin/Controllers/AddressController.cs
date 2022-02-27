using Bll;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class AddressController : BaseController
    {
        AddressBll bll = new AddressBll();
        // GET: Admin/Address
        public ActionResult AddAddress() 
        {
            AddressDTO model = new AddressDTO();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddAddress(AddressDTO model) 
        {
            if (ModelState.IsValid)
            {
                if (bll.AddAddress(model))
                {
                    ViewBag.ProcessState = General.Messages.Addsuccess;
                    ModelState.Clear();
                    model = new AddressDTO();
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
        public ActionResult AddressList()
        {
            List<AddressDTO> ls = new List<AddressDTO>();
            ls = bll.GetAddress();
            return View(ls);
        }
        public ActionResult UpdateAddress(int ID) 
        {
            AddressDTO dto = new AddressDTO();
            dto = bll.GetAddressWithID(ID);
            return View(dto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateAddress(AddressDTO model) 
        {
            if (ModelState.IsValid)
            {
                if (bll.UpdateAddress(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
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
        public JsonResult DeleteAddress(int ID) 
        {
            bll.DeleteAddress(ID);
            return Json("");
        }
    }
}