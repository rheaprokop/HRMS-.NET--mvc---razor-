using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HRMS.Models;
using System.Data.Entity;
using System.ComponentModel;
using System.IO;
using System.Data;

namespace HRMS.Controllers
{
    public class CandidateController : Controller
    {
        //
        // GET: /Candidate/
        HRMSContext _db = new HRMSContext();

        public ActionResult Index()
        {
            try
            {
                 var candidates = _db.Candidates;
                 return View(candidates);
            }
            catch (Exception ex)
            {
                TempData["color"] = "red";
                TempData["result"] = ex.Message;

            }
            return View();

        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Candidate candidate)
        {
            try
            {
                using (var context = new HRMSContext()) 
                {

                    candidate.CreatedDate = DateTime.Now;
                    context.Candidates.Add(candidate);
                    context.SaveChanges();  


                    //upload candidate's resume
                    //drops database and data with it
                    //Database.SetInitializer<CandidateContext>(new DropCreateDatabaseAlways<CandidateContext>()); 
                    if (Request.Files["cvfile"].ContentLength > 0)
                    {
                        //get last insert id for saving the resume
                        string filename = candidate.Firstname + "_" + candidate.Lastname + "_" + candidate.CandidateID;

                        string extension = System.IO.Path.GetExtension(Request.Files["cvfile"].FileName);

                        string path = string.Format("{0}/{1}{2}", Server.MapPath("~/Documents/cv"),
                            filename, extension);

                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);

                        }

                        Request.Files["cvfile"].SaveAs(path);

                        Candidate c = _db.Candidates.SingleOrDefault(i => i.CandidateID == candidate.CandidateID);
                        c.CVFilename = filename;
                        _db.SaveChanges();
                    }
                    else
                    {
                        candidate.CVFilename = "";
                    }

                }
                TempData["color"] = "green";
                TempData["result"] = "Successfully Saved New Candidate To Database.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Alert = ex.Message;
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Candidate c = _db.Candidates.First(i => i.CandidateID == id);
                _db.Candidates.Remove(c);
                _db.SaveChanges();

                TempData["color"] = "green";
                TempData["result"] = "Successfully Deleted Candidate Record From Database.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {//TODO: Log error				
                TempData["color"] = "red";
                TempData["result"] = ex.Message;
                return RedirectToAction("Index");
            } 
        }

        public ActionResult Update(int id)
        {
            return View(_db.Candidates.Where(i => i.CandidateID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Update(Candidate candidate, int id)
        {

            using (var _db = new HRMSContext())
            {
                Candidate c = _db.Candidates.SingleOrDefault(i => i.CandidateID == id);

                c.Firstname = candidate.Firstname;
                c.Lastname = candidate.Lastname;
                c.Title = candidate.Title;
                c.Email = candidate.Email;
                c.Telephone = candidate.Telephone;
                c.Status = candidate.Status;
                c.ShortDesc = candidate.ShortDesc;

                if (Request.Files["cvfile"].ContentLength > 0)
                {
                    //get last insert id for saving the resume
                    string filename = candidate.Firstname + "_" + candidate.Lastname + "_" + id;

                    string extension = System.IO.Path.GetExtension(Request.Files["cvfile"].FileName);

                    string path = string.Format("{0}/{1}{2}", Server.MapPath("~/Documents/cv"),
                        filename, extension);

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    Request.Files["cvfile"].SaveAs(path);

                    candidate.CVFilename = filename + "." + extension;
                }
                else
                {
                    candidate.CVFilename = "";
                }

                c.CVFilename = candidate.CVFilename;
                _db.SaveChanges();

                TempData["color"] = "green";
                TempData["result"] = "Successfully Updated Candidate Record.";
            }

            return RedirectToAction("Index");
        }
        public ViewResult View(int id)
        {
            TempData["isupdate"] = "no";
            return View(_db.Candidates.Where(i => i.CandidateID == id).FirstOrDefault());
        }

        
    }
}
