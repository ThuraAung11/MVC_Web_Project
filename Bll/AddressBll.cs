using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class AddressBll
    {
        AddressDAO dao = new AddressDAO();
        public bool AddAddress(AddressDTO model)
        {
            Address address = new Address();
            address.Address1 = model.AddressContent;
            address.Email = model.Email;
            address.Phone = model.Phone;
            address.Phone2 = model.Phone2;
            address.Fax = model.Fax;
            address.MapPathLarge = model.LargeMapPath;
            address.MapPathSmall = model.SmallMapPath;
            address.AddDate = DateTime.Now;
            address.LastUpdateDate = DateTime.Now;
            address.LastUpdateID = UserStatic.UserID;

            int ID = dao.AddAddress(address);
            LogDAL.AddLog(General.ProcessType.AddressAdd,General.TableName.Address,ID);
            return true;
        }

        public List<AddressDTO> GetAddress()
        {
            return dao.GetAddress();
        }

        public AddressDTO GetAddressWithID(int ID)
        {
            AddressDTO dto = new AddressDTO();
            dto = dao.GetAddressWithID(ID);
            return dto;
        }

        public bool UpdateAddress(AddressDTO model)
        {
            dao.UpdateAddress(model);
            LogDAL.AddLog(General.ProcessType.AddressUpdate,General.TableName.Address,model.ID);
            return true;
        }

        public void DeleteAddress(int ID)
        {
            dao.DeleteAddress(ID);
        }
    }
}
