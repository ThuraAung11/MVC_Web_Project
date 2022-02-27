using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AddressDAO : PostContext
    {
        public int AddAddress(Address address)
        {
            try
            {
                db.Addresses.Add(address);
                db.SaveChanges();
                return address.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AddressDTO> GetAddress()
        {
            List<AddressDTO> dtolist = new List<AddressDTO>();
            List<Address> ads = db.Addresses.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();
            foreach (var item in ads)
            {
                AddressDTO dto = new AddressDTO()
                {
                    ID = item.ID,
                    AddressContent=item.Address1,
                    Email=item.Email,
                    Phone=item.Phone,
                    Phone2=item.Phone2,
                    Fax=item.Fax,
                    LargeMapPath=item.MapPathLarge,
                    SmallMapPath=item.MapPathSmall
                };
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public AddressDTO GetAddressWithID(int ID)
        {
            try
            {
                Address ads = db.Addresses.First(x=>x.ID==ID);
                AddressDTO dto = new AddressDTO();
                dto.AddressContent = ads.Address1;
                dto.ID = ads.ID;
                dto.Email = ads.Email;
                dto.Phone = ads.Phone;
                dto.Phone2 = ads.Phone2;
                dto.Fax = ads.Fax;
                dto.LargeMapPath = ads.MapPathLarge;
                dto.SmallMapPath = ads.MapPathSmall;
                return dto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteAddress(int ID)
        {
            try
            {
                Address ads = db.Addresses.First(x=>x.ID==ID);
                ads.isDeleted = true;
                ads.DeletedDate = DateTime.Now;
                ads.LastUpdateDate = DateTime.Now;
                ads.LastUpdateID = UserStatic.UserID;

                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateAddress(AddressDTO model)
        {
            try
            {
                Address ads = db.Addresses.First(x=>x.ID==model.ID);
                ads.ID = model.ID;
                ads.Address1 = model.AddressContent;
                ads.Email = model.Email;
                ads.Phone = model.Phone;
                ads.Phone2 = model.Phone2;
                ads.Fax = model.Fax;
                ads.LastUpdateDate = DateTime.Now;
                ads.LastUpdateID = UserStatic.UserID;
                ads.MapPathLarge = model.LargeMapPath;
                ads.MapPathSmall = model.SmallMapPath;
                
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
