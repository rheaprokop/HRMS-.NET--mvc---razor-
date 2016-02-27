using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HRMS.Models
{
    public class Candidate
    {
        public int CandidateID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; } 
        public string ShortDesc { get; set; }
        public string Status { get; set; }
        public string CVFilename { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; } 
        
    } 
}