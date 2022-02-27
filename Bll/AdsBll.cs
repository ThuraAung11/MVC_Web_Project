using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class AdsBll
    {
        AdsDAO dao = new AdsDAO();
        public bool AddAds(AdsDTO model)
        {
            Ads ad = new Ads();
            ad.Name = model.Name;
            ad.Link = model.Link;
            ad.ImagePath = model.ImagePath;
            ad.Size = model.Imagesize;
            ad.AddDate = DateTime.Now;
            ad.LastUpdateDate = DateTime.Now;
            ad.LastUpdateUserID = UserStatic.UserID;

            int ID = dao.AddAds(ad);
            LogDAL.AddLog(General.ProcessType.AdsAdd,General.TableName.Ads,ID);
            return true;
        }

        public List<AdsDTO> GetAds()
        {
            return dao.GetAds();
        }

        public AdsDTO GetAdsWithID(int ID)
        {
            return dao.GetAdsWithID(ID);
        }

        public string UpdateAds(AdsDTO model)
        {
            string oldImagePath = dao.UpdateAds(model);
            LogDAL.AddLog(General.ProcessType.AdsUpdate,General.TableName.Ads,model.ID);
            return oldImagePath;
        }

        public string DeleteAds(int ID)
        {
            string imgPath = dao.DeleteAds(ID);
            LogDAL.AddLog(General.ProcessType.AdsDelete,General.TableName.Ads,ID);
            return imgPath;
        }
    }
}
