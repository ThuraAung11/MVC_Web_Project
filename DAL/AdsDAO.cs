using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdsDAO : PostContext
    {
        public int AddAds(Ads ad)
        {
            try
            {
                db.Ads1.Add(ad);
                db.SaveChanges();
                return ad.ID;
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

        public string UpdateAds(AdsDTO model)
        {
            try
            {
                Ads ads = db.Ads1.First(x=>x.ID==model.ID);
                string oldImagePath = ads.ImagePath;
                ads.ID = model.ID;
                ads.Name = model.Name;
                ads.Link = model.Link;
                if (model.ImagePath!=null)
                {
                    ads.ImagePath = model.ImagePath;
                }
                ads.LastUpdateDate = DateTime.Now;
                ads.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return oldImagePath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string DeleteAds(int ID)
        {
            Ads ad = db.Ads1.First(x=>x.ID==ID);
            ad.isDeleted = true;
            ad.DeletedDate = DateTime.Now;
            ad.LastUpdateDate = DateTime.Now;
            ad.LastUpdateUserID = UserStatic.UserID;

            db.SaveChanges();
            return ad.ImagePath;
        }

        public AdsDTO GetAdsWithID(int ID)
        {
            Ads ads = db.Ads1.First(x=>x.ID==ID);
            AdsDTO model = new AdsDTO() 
            {
                ID=ads.ID,
                Name=ads.Name,
                Link=ads.Link,
                ImagePath=ads.ImagePath,
                Imagesize=ads.Size
            };
            return model;
        }

        public List<AdsDTO> GetAds()
        {
            List<AdsDTO> ls = new List<AdsDTO>();
            List<Ads> ads = db.Ads1.Where(x=>x.isDeleted==false).OrderBy(x=>x.AddDate).ToList();
            foreach (var item in ads)
            {
                AdsDTO model = new AdsDTO() 
                {
                    ID=item.ID,
                    Name=item.Name,
                    Link=item.Link,
                    ImagePath=item.ImagePath,
                    Imagesize=item.Size,
                };
                ls.Add(model);
            }
            return ls;
        }
    }
}
