using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class ContactBLL
    {
        ContactDAO dao = new ContactDAO();
        public bool AddContact(GeneralDTO model)
        {
            Contact contact = new Contact();
            contact.NameSurname = model.Name;
            contact.Subject = model.Subject;
            contact.Email = model.Email;
            contact.Message = model.Message;
            contact.AddDate = DateTime.Now;
            contact.LastUpdateDate = DateTime.Now;
            dao.AddContact(contact);
            return true;
        }

        public List<ContactDTO> GetAllMessages()
        {
            return dao.GetAllMessages();
        }

        public void ReadMessage(int ID)
        {
            dao.ReadMessage(ID);
            LogDAL.AddLog(General.ProcessType.ContactRead,General.TableName.Contact,ID);
        }

        public void DeleteMessage(int ID)
        {           
            
            dao.DeleteMessage(ID);
            LogDAL.AddLog(General.ProcessType.ContactDelete,General.TableName.Contact,ID);
        }

        public List<ContactDTO> GetAllUnreadMessages()
        {
            return dao.GetAllUnreadMessages();
        }
    }
}
