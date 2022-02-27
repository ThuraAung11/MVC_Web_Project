using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAO:PostContext
    {
        public UserDTO GetUserWithPasswordAndUsername(UserDTO user) 
        {
            UserDTO dto = new UserDTO();
            T_User t_User = db.T_User.FirstOrDefault(x=> x.Username==user.Username && x.Password==user.Password);
            if (t_User!=null && t_User.ID!=0)
            {
                dto.ID = t_User.ID;
                dto.Username = t_User.Username;
                dto.Name = t_User.NameSurname;
                dto.ImagePath = t_User.ImagePath;
                dto.isAdmin = t_User.isAdmin;
            }
            return dto;
        }

        public List<UserDTO> GetUser()
        {
            List<T_User> user = db.T_User.Where(x=>x.isDeleted==false).OrderBy(x=>x.AddDate).ToList();
            List<UserDTO> model = new List<UserDTO>();
            foreach (var item in user)
            {
                UserDTO usermodel = new UserDTO() 
                {
                    ID=item.ID,
                    Name=item.NameSurname,
                    Username=item.Username,
                    ImagePath=item.ImagePath,
                };
                model.Add(usermodel);
            }
            return model;
        }

        public UserDTO GetUserWithID(int ID)
        {
            UserDTO model = new UserDTO();
            T_User user = db.T_User.First(x=>x.ID==ID);
            model.ID = user.ID;
            model.Name = user.NameSurname;
            model.Username = user.Username;
            model.Password = user.Password;
            model.isAdmin = user.isAdmin;
            model.Email = user.Email;
            model.ImagePath = user.ImagePath;

            return model;
        }

        public string DeleteUser(int ID)
        {
            try
            {
                T_User user = db.T_User.First(x => x.ID == ID);
                user.isDeleted = true;
                user.DeletedDate = DateTime.Now;
                user.LastUpdateDate = DateTime.Now;
                user.LastUpdateUserID = UserStatic.UserID;

                db.SaveChanges();
                return user.ImagePath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public string UpdateUser(UserDTO model)
        {
            T_User user = db.T_User.First(x=>x.ID==model.ID);
            string oldImagePath = user.ImagePath;
            user.NameSurname = model.Name;
            user.Password = model.Password;
            user.Username = model.Username;
            if (model.ImagePath!=null)
            {
                user.ImagePath = model.ImagePath;
            }
            user.Email = model.Email;
            user.LastUpdateDate = DateTime.Now;
            user.LastUpdateUserID = UserStatic.UserID;
            user.isAdmin = model.isAdmin;
            db.SaveChanges();
            return oldImagePath;
        }

        public int AddUser(T_User user)
        {
            try
            {
                db.T_User.Add(user);
                db.SaveChanges();
                return user.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
