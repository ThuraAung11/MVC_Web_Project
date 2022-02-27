using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MetaDTO
    {
        public int MetaID { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage ="Please fill the content area")]
        public string MeataContent { get; set; }
        //public string AddDate { get; set; }
        //public bool isDeleted { get; set; }
        //public DateTime DeletedDate { get; set; }
        //public int LastUpdateUserID { get; set; }
        //public DateTime LastUpdateDate { get; set; }
    }
}
