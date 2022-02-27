using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FavDAO : PostContext
    {
        public FavDTO GetFav()
        {
            FavDTO model = new FavDTO();
            FavLogoTitle fav = db.FavLogoTitles.First();
            model.ID = fav.ID;
            model.Title = fav.Title;
            model.Logo = fav.Logo;
            model.Fav = fav.Fav;
            return model;
        }

        public FavDTO UpdateFav(FavDTO model)
        {
            try
            {
                FavDTO dto = new FavDTO();
                FavLogoTitle fav = db.FavLogoTitles.First();
                dto.ID = fav.ID;
                dto.Title = fav.Title;
                dto.Logo = fav.Logo;
                fav.Title = model.Title;
                if (model.Logo!=null)
                {
                    fav.Logo = model.Logo;
                }
                if (model.Fav!=null)
                {
                    fav.Fav = model.Fav;
                }
                fav.LastUpdateDate = DateTime.Now;
                fav.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return dto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
