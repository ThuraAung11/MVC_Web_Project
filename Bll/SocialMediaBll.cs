using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class SocialMediaBll
    {
        SocialMediaDAO dao = new SocialMediaDAO();
        public bool AddSocialMedia(SocialMediaDTO smodel)
        {
            SocialMedia model = new SocialMedia() 
            {
                Name=smodel.Name,
                ImagePath=smodel.ImagePath,
                Link=smodel.Link,
                AddDate=DateTime.Now,
                LastUpdateDate=DateTime.Now,
                LastUpdateUserID=UserStatic.UserID,
            };

            int ID = dao.AddSocialMedia(model);

            LogDAL.AddLog(General.ProcessType.SocialAdd,General.TableName.Social,ID);
            return true;
        }

        public List<SocialMediaDTO> GetSocialMedia()
        {
            List<SocialMediaDTO> dtolist = new List<SocialMediaDTO>();
            dtolist = dao.GetSocialMedias();

            return dtolist;
        }

        public SocialMediaDTO GetSocialMediaWithID(int ID)
        {
            SocialMediaDTO mediaDTO = dao.GetSocialMediaWithID(ID);
            return mediaDTO;
        }

        public string UpdateSocialMedia(SocialMediaDTO mediaDTO)
        {
            string oldImagePath = dao.UpdateSocialMedia(mediaDTO);
            LogDAL.AddLog(General.ProcessType.SocialUpdate, General.TableName.Social, mediaDTO.ID);
            return oldImagePath;
        }

        public string DeleteSocialMedia(int ID)
        {
            string img = dao.DeleteSocialMedia(ID);
            LogDAL.AddLog(General.ProcessType.SocialDelete,General.TableName.Social,ID);
            return img;
        }
    }
}
