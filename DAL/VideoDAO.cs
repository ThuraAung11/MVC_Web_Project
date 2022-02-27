using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VideoDAO:PostContext
    {
        public int AddVideo(Video video)
        {
            try
            {
                db.Videos.Add(video);
                db.SaveChanges();
                return video.ID;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public VideoDTO GetVideoWithID(int ID)
        {
            Video v = db.Videos.First(x=>x.ID==ID);
            VideoDTO dto = new VideoDTO();
            dto.ID = v.ID;
            dto.Title = v.Title;
            dto.VideoPath = v.VideoPath;
            dto.OriginalVideoPath = v.OriginalVideoPath;
            return dto;
        }

        public void DeleteVideo(int ID)
        {
            try
            {
                Video v = db.Videos.First(x => x.ID == ID);
                v.isDeleted = true;
                v.DeletedDate = DateTime.Now;
                v.LastUpdateDate = DateTime.Now;
                v.LastUpdateUserID = UserStatic.UserID;

                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public void UpdateVideo(VideoDTO model)
        {
            try
            {
                Video v = db.Videos.First(x=>x.ID==model.ID);
                v.ID = model.ID;
                v.Title = model.Title;
                v.VideoPath = model.VideoPath;
                v.OriginalVideoPath = model.OriginalVideoPath;
                v.LastUpdateDate = DateTime.Now;
                v.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<VideoDTO> VideoList()
        {
            List<VideoDTO> ls = new List<VideoDTO>();
            List<Video> v = db.Videos.Where(x=>x.isDeleted==false).OrderBy(x=>x.AddDate).ToList();
            foreach (var item in v)
            {
                VideoDTO dto = new VideoDTO() 
                {
                    ID=item.ID,
                    Title=item.Title,
                    VideoPath=item.VideoPath,
                    OriginalVideoPath=item.OriginalVideoPath,
                    AddDate=item.AddDate
                };
                ls.Add(dto);
            }
            return ls;
        }
    }
}
