using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ContactDAO : PostContext
    {
        public void AddContact(Contact contact)
        {
            try
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ContactDTO> GetAllUnreadMessages()
        {
            List<ContactDTO> cList = new List<ContactDTO>();
            List<Contact> contact = db.Contacts.Where(x=>x.isRead==false && x.isDeleted==false).OrderByDescending(x=>x.AddDate).ToList();
            foreach (var item in contact)
            {
                ContactDTO dto = new ContactDTO()
                {
                    ID = item.ID,
                    Name = item.NameSurname,
                    Email=item.Email,
                    Subject=item.Subject,
                    Message=item.Message,
                    AddDate=item.AddDate,
                    isRead=item.isRead
                };
                
                cList.Add(dto);
            }
            return cList;
        }

        public List<ContactDTO> GetAllMessages()
        {
            List<ContactDTO> cList = new List<ContactDTO>();
            List<Contact> contact = db.Contacts.Where(x =>x.isDeleted == false).OrderByDescending(x => x.AddDate).ToList();
            foreach (var item in contact)
            {
                ContactDTO dto = new ContactDTO()
                {
                    ID = item.ID,
                    Name = item.NameSurname,
                    Email = item.Email,
                    Subject = item.Subject,
                    Message = item.Message,
                    AddDate = item.AddDate,
                    isRead = item.isRead
                };

                cList.Add(dto);
            }
            return cList;
        }

        public void DeleteMessage(int ID)
        {
            Contact contact = db.Contacts.First(x=>x.ID==ID);
            contact.isDeleted = true;
            contact.DeletedDate = DateTime.Now;
            contact.LastUpdateDate = DateTime.Now;
            contact.LastUpdateUserID = UserStatic.UserID;

            db.SaveChanges();
        }

        public void ReadMessage(int ID)
        {
            Contact contact = db.Contacts.First(x=>x.ID==ID);
            contact.isRead = true;
            contact.ReadUserID = UserStatic.UserID;
            contact.LastUpdateDate = DateTime.Now;
            contact.LastUpdateUserID = UserStatic.UserID;
           
            db.SaveChanges();
        }
    }
}
