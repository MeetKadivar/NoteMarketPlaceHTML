using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoteMarketPlace.Controllers
{
    public class NoteController : Controller
    {
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        // GET: Note
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult NoteListing(string search, string Type, string Country, string Category, string Uni, string Course, string Rating,int PageNumber = 1 )
        {
            if((search == null || search == "") && (Type == null && Country == null && Course==null && Category == null && Uni == null && Rating == null))
            {

                var res= db.SellerNotes.ToList();
                ViewBag.TotalPages = Math.Ceiling(res.Count() / 6.0);
                ViewBag.PageNumber = PageNumber;
                res = res.Skip((PageNumber - 1) * 6).Take(6).ToList();

                return View(res);

            }
            else
            {
                ViewBag.Message = search;
               
                    var res = db.SellerNotes.Where(x => x.Title.Contains(search) || x.NoteType1.Name == Type || x.Country1.Name == Country || x.NoteCategory.Name == Category || x.UniversityName == Uni || x.Course == Course).Distinct().ToList();
                    return View(res);

             


            }


          
        }

        

        public ActionResult Delete(int id)
        {
            var NoteId = db.SellerNotes.Where(x => x.ID == id).FirstOrDefault();
            var SellerNoteAttachement = db.SellerNotesAttachements.Where(x => x.NoteID == id).FirstOrDefault();
            var NoteReview = db.SellerNotesReviews.Where(x => x.NoteID == id);
            var SellerNoteIssue = db.SellerNotesReportedIssues.Where(x => x.NoteID == id);
            var download = db.Downloads.Where(x => x.NoteID == id);
            
            if(SellerNoteAttachement != null)
            {
                db.SellerNotesAttachements.Remove(SellerNoteAttachement);
            }
            if(NoteReview != null)
            {
                db.SellerNotesReviews.RemoveRange(NoteReview);
            }
            if(SellerNoteIssue != null)
            {
                db.SellerNotesReportedIssues.RemoveRange(SellerNoteIssue);
            }
            if(download != null)
            {
                db.Downloads.RemoveRange(download);
            }


            db.SellerNotes.Remove(NoteId);
            db.SaveChanges();
            return RedirectToAction("DashBoard", "Home");
        }
    }

   
}