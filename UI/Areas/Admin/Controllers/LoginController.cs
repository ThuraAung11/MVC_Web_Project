using Bll;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace UI.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        UserBll bll = new UserBll();
        // GET: Admin/Login
        public ActionResult Index()
        {
            UserDTO userDTO = new UserDTO();
            return View(userDTO);
        }
        [HttpPost]
        public ActionResult Index(UserDTO userDTO) 
        {
            if (userDTO.Username!=null && userDTO.Password!=null)
            {
                UserDTO dto = bll.GetUserWithUsernameAndPassword(userDTO);
                if (dto != null && dto.ID != 0)
                {
                    UserStatic.UserID = dto.ID;
                    UserStatic.Namesurname = dto.Name;
                    UserStatic.isAdmin = dto.isAdmin;
                    UserStatic.ImagePath = dto.ImagePath;
                    LogBll.AddLog(General.ProcessType.Login,General.TableName.Login,12);
                    return RedirectToAction("PostList", "Post");
                }
                else
                {
                    return View(userDTO);
                }

            }
            else
                return View(userDTO);
        }
    }
}