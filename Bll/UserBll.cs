using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class UserBll
    {
        UserDAO userDAO = new UserDAO();
        public UserDTO GetUserWithUsernameAndPassword(UserDTO dTO) 
        {
            UserDTO user = new UserDTO();
            user = userDAO.GetUserWithPasswordAndUsername(dTO);
            return user;
        }

        public List<UserDTO> GetUser()
        {
            return userDAO.GetUser();
        }

        public bool AddUser(UserDTO model)
        {
            T_User user = new T_User();
            user.Username = model.Username;
            user.Password = model.Password;
            user.Email = model.Email;
            user.ImagePath = model.ImagePath;
            user.NameSurname = model.Name;
            user.AddDate = DateTime.Now;
            user.isAdmin = model.isAdmin;
            user.LastUpdateDate = DateTime.Now;
            user.LastUpdateUserID = UserStatic.UserID;
            int ID = userDAO.AddUser(user);
            LogDAL.AddLog(General.ProcessType.UserAdd, General.TableName.User, ID);

            return true;
        }

        public UserDTO GetUserWithID(int ID)
        {
            return userDAO.GetUserWithID(ID);
        }

        public string UpdateUser(UserDTO model)
        {
            string oldImagePath = userDAO.UpdateUser(model);
            return oldImagePath;
        }

        public string DeleteUser(int ID)
        {
            string imgpath = userDAO.DeleteUser(ID);
            LogDAL.AddLog(General.ProcessType.UserDelete,General.TableName.User,ID);
            return imgpath;
        }
    }
}
