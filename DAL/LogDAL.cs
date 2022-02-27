using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Net.WebRequestMethods;

namespace DAL
{
    public class LogDAL:PostContext
    {
        public static void AddLog(int ProcessType, string TableName, int ProcessID) 
        {
            Log_Table log = new Log_Table();
            log.UserID = UserStatic.UserID;
            log.ProcessType = ProcessType;
            log.ProcessID = ProcessID;
            log.ProcessCategoryType = TableName;
            log.IPAddress = HttpContext.Current.Request.UserHostAddress;
            log.ProcessDate = DateTime.Now;
            db.Log_Table.Add(log);
            db.SaveChanges();
        }

        public List<LogDTO> GetAllLog()
        {
            List<LogDTO> logList = new List<LogDTO>();
            var list = (from l in db.Log_Table
                        join u in db.T_User on l.UserID equals u.ID
                        join p in db.ProcessTypes on l.ProcessType equals p.ID
                        select new 
                        {
                            ID=l.ID,
                            UserName=u.Username,
                            TableName=l.ProcessCategoryType,
                            TableID=l.ProcessID,
                            ProcessName=p.ProcessName,
                            ProcessDate=l.ProcessDate,
                            ipAdderess=l.IPAddress
                        }).OrderByDescending(x=>x.ProcessDate).ToList();
            foreach (var item in list)
            {
                LogDTO dto = new LogDTO() 
                {
                    ID=item.ID,
                    UserName=item.UserName,
                    TableID=item.TableID,
                    TableName=item.TableName,
                    ProcessName=item.ProcessName,
                    PorcessDate=item.ProcessDate,
                    IpAddress=item.ipAdderess
                };
                logList.Add(dto);
            }
            return logList;
        }
    }
}
