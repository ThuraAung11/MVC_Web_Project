using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class LogBll
    {
        LogDAL dal = new LogDAL();
        public static void AddLog(int ProcessType,string TableName,int ProcessID) 
        {
            LogDAL.AddLog(ProcessType,TableName,ProcessID);
        }

        public List<LogDTO> GetAllLog()
        {
            return dal.GetAllLog();
        }
    }
}
