using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class Opening
    {
        public int OpeningID { get; set; }
        public string JobTitle { get; set; }
        public string JobDesc { get; set; }
        public string Location { get; set; }
        public string EmplType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int StageID { get; set; }
    }

    public class Stage
    {
        public int StageID { get; set; }
        public string Desc { get; set; }
        public int Seq { get; set; }
    }

}