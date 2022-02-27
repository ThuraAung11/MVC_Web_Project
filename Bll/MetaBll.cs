using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class MetaBll
    {
        MetaDAO dao = new MetaDAO();
        public bool AddMeta(MetaDTO metaDTO)
        {
            Meta meta = new Meta();
            meta.Name = metaDTO.Name;
            meta.MetaContent = metaDTO.MeataContent;
            meta.LastUpdateDate = DateTime.Now;
            meta.LastUpdateUserID = UserStatic.UserID;
            meta.AddDate = DateTime.Now;
            int MetaID=dao.AddMeta(meta);

            LogDAL.AddLog(General.ProcessType.MetaAdd,General.TableName.Meta,MetaID);
            return true;
        }

        public List<MetaDTO> GetData()
        {
            List<MetaDTO> dTOs = new List<MetaDTO>();
            dTOs = dao.GetMetaData();
            return dTOs;
        }

        public MetaDTO GetMetaID(int ID)
        {
            MetaDTO meta = new MetaDTO();
            meta = dao.GetMetaID(ID);
            return meta;
        }

        public bool UpdateMeta(MetaDTO metaDTO)
        {
            dao.UpdateMeta(metaDTO);
            LogDAL.AddLog(General.ProcessType.MetaUpdate,General.TableName.Meta,metaDTO.MetaID);
            return true;
        }

        public void DeleteMeta(int ID)
        {
            dao.DeleteMeta(ID);
            LogDAL.AddLog(General.ProcessType.MetaDelete,General.TableName.Meta,ID);
        }
    }
}
