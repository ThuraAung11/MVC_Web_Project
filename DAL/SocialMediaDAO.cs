using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SocialMediaDAO : PostContext
    {
        
        public int AddSocialMedia(SocialMedia model)
        {
            try
            {
                db.SocialMedias.Add(model);
                db.SaveChanges();
                return model.ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SocialMediaDTO> GetSocialMedias()
        {
            List<SocialMedia> ls = db.SocialMedias.Where(x=>x.isDeleted==false).ToList();
            List<SocialMediaDTO> socialMediaDTOs = new List<SocialMediaDTO>();

            foreach (var item in ls)
            {
                SocialMediaDTO mediaDTO = new SocialMediaDTO() 
                {
                    Name=item.Name,
                    ImagePath=item.ImagePath,
                    Link=item.Link,
                    ID=item.ID
                };
                socialMediaDTOs.Add(mediaDTO);
            }
            return socialMediaDTOs;
        }

        public SocialMediaDTO GetSocialMediaWithID(int ID)
        {
            SocialMedia social = db.SocialMedias.First(x=>x.ID==ID);
            SocialMediaDTO dto = new SocialMediaDTO();
            dto.ID = social.ID;
            dto.Name = social.Name;
            dto.Link = social.Link;
            dto.ImagePath=social.ImagePath;
            return dto;
        }

        public string UpdateSocialMedia(SocialMediaDTO mediaDTO)
        {
            try
            {
                SocialMedia social = db.SocialMedias.First(x => x.ID == mediaDTO.ID);
                string oldImagePath = social.ImagePath;
                social.Name = mediaDTO.Name;
                social.Link = mediaDTO.Link;
                if (mediaDTO.ImagePath != null)
                    social.ImagePath = mediaDTO.ImagePath;
                social.LastUpdateDate = DateTime.Now;
                social.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return oldImagePath;
            }
            catch (Exception ex)
            {
                 throw ex;
            }
          
        }

        public string DeleteSocialMedia(int ID)
        {
            SocialMedia social = db.SocialMedias.First(x=>x.ID==ID);
            social.isDeleted = true;
            social.DeletedDate = DateTime.Now;
            social.LastUpdateDate = DateTime.Now;
            social.LastUpdateUserID = UserStatic.UserID;

            db.SaveChanges();
            return social.ImagePath;
        }
    }
}
