using HRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Controllers
{
    public class RecruitController : Controller
    {
        //
        // GET: /Recruit/
        HRMSContext _db = new HRMSContext(); 


        public ActionResult Index()
        {
            return View();
        }
         

        public ActionResult Create()
        {
            return View();
             
        }

        [HttpPost]
        public ActionResult Create(Opening opening)
        {
            try
            {
                using (var context = new HRMSContext())
                { 
                    opening.CreatedDate = DateTime.Now; 
                    context.Openings.Add(opening);
                    context.SaveChanges(); 
                }
            }
            catch (Exception ex)
            {
                TempData["color"] = "red";
                TempData["result"] = ex.Message;
            }

            return new RedirectResult(Url.Action("Hire", new { id = opening.OpeningID }) + "?#tabs-2");
             
        }
        [HttpGet]
        public ActionResult Hire(int id)
        {
            return View(_db.Openings.Where(i => i.OpeningID == id).FirstOrDefault());
        } 

        public ActionResult Stage()
        {
            var stages = (from c in _db.Stages   
                             orderby c.Seq ascending
                             select c);
            return View(stages);
        }

        public ActionResult StageForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StageForm(Stage stage)
        {
            try
            {
                using (var context = new HRMSContext())
                {
                    context.Stages.Add(stage);
                    context.SaveChanges();

                    //get last sequence
                    var idKey = (from c in context.Stages // the table1 here has red line under it.
                                 orderby c.Seq descending
                                 select c).First();

                    int last = idKey.Seq;

                    Stage s = _db.Stages.SingleOrDefault(i => i.StageID == stage.StageID);
                    s.Seq = last + 1;
                    _db.SaveChanges();

                }

                TempData["color"] = "green";
                TempData["result"] = "Successfully Saved New Stage Record Database.";
            }
            catch (Exception ex)
            {
                TempData["color"] = "red";
                TempData["result"] = ex.Message;
            }

            return RedirectToAction("Stage");
        }

        [HttpPost]
        public ActionResult UpdateStage(Stage stage, int id)
        {
            using (var _db = new HRMSContext())
            {
                Stage s = _db.Stages.SingleOrDefault(i => i.StageID == id);

                //get last sequence
                var idKey = (from c in _db.Stages // the table1 here has red line under it.
                             orderby c.Seq descending
                             select c).First();

                int last = idKey.Seq;

                s.Desc = stage.Desc;
                _db.SaveChanges();
                TempData["color"] = "green";
                TempData["result"] = "Successfully Updated Candidate Record.";
            }

            return RedirectToAction("Stage");
        }

        public ActionResult UpdateStage(int id)
        {
            return View(_db.Stages.Where(i => i.StageID == id).FirstOrDefault());
        }

        public ActionResult DeleteStage(int id)
        {
            try
            {
                Stage s = _db.Stages.First(i => i.StageID == id);
                _db.Stages.Remove(s);
                _db.SaveChanges();

                TempData["color"] = "green";
                TempData["result"] = "Successfully Deleted Stage From Database.";
                return RedirectToAction("Stage");
            }
            catch (Exception ex)
            {//TODO: Log error				
                TempData["color"] = "red";
                TempData["result"] = ex.Message;
                return RedirectToAction("Stage");
            }
        }
        
       [HttpGet]
        public ActionResult UpdateStageSeq(Stage stage, string seq)
        {

            using (var _db = new HRMSContext())
            {
                //update all sequence value to 0

                string[] sq = seq.Split(',');
                foreach (var item in sq)
                {
                    int q = Convert.ToInt32(item);
                    Stage s = _db.Stages.SingleOrDefault(i => i.StageID == q);
                    s.Seq = 0;
                    _db.SaveChanges();
                }

                foreach (var item in sq)
                {
                    int q = Convert.ToInt32(item);
                    Stage s = _db.Stages.SingleOrDefault(i => i.StageID == q);

                    //get last sequence
                    var idKey = (from c in _db.Stages // the table1 here has red line under it.
                                 orderby c.Seq descending
                                 select c).First();

                    int last = idKey.Seq;
                    s.Seq = last + 1;
                    _db.SaveChanges();
                }
            }
            //update seq field to 1,2,3... so on and so forth
            return RedirectToAction("Stage");
        }
    }
}
