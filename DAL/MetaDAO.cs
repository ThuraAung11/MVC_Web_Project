using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MetaDAO:PostContext
    {
        public int AddMeta(Meta meta)
        {
            try
            {
                db.Metas.Add(meta);
                db.SaveChanges();

                return meta.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<MetaDTO> GetMetaData()
        {
            List<MetaDTO> metaDTOs = new List<MetaDTO>();

            List<Meta> list = db.Metas.Where(x=>x.isDeleted==false).OrderBy(x=>x.AddDate).ToList();
            foreach (var item in list)
            {
                MetaDTO meta = new MetaDTO() 
                {
                    MetaID=item.ID,
                    Name=item.Name,
                    MeataContent=item.MetaContent,
                };

                metaDTOs.Add(meta);
            }
            return metaDTOs;
        }

        public MetaDTO GetMetaID(int ID)
        {
            Meta meta = db.Metas.First(x=> x.ID==ID);
            MetaDTO dtoresult = new MetaDTO()
            {
                MetaID = meta.ID,
                Name=meta.Name,
                MeataContent=meta.MetaContent,
            };
            return dtoresult;
        }

        public void DeleteMeta(int ID)
        {
            try
            {
                Meta meta = db.Metas.First(x=>x.ID==ID);
                meta.DeletedDate = DateTime.Now;
                meta.isDeleted = true;
                meta.LastUpdateDate = DateTime.Now;
                meta.LastUpdateUserID = UserStatic.UserID;

                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateMeta(MetaDTO metaDTO)
        {
            try
            {
                Meta meta = db.Metas.First(x => x.ID == metaDTO.MetaID);
                meta.ID = metaDTO.MetaID;
                meta.Name = metaDTO.Name;
                meta.MetaContent = metaDTO.MeataContent;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
