using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class FavBll
    {
        FavDAO dao = new FavDAO();
        public FavDTO GetFav()
        {
            return dao.GetFav();
        }

        public FavDTO UpdateFav(FavDTO model)
        {
            FavDTO dto = new FavDTO();
            dto = dao.UpdateFav(model);
            LogDAL.AddLog(General.ProcessType.IconUpdate,General.TableName.IconFavLogoTitle,dto.ID);
            return dto;
        }
    }
}
