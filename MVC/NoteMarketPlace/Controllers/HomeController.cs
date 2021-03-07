using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using NoteMarketPlace.Models;

namespace NoteMarketPlace.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Contact(string Fullname, string Email, string subject, string comment)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("meetkadivar88@gmail.com", Fullname);
                    var receiverEmail = new MailAddress("meetkadivar88@gmail.com", "Receiver");
                    var password = "123@Meet";
                    var sub = subject;
                    var body = Fullname;
                    var cmnt = comment;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = Email + "\n " + body + " \n" + cmnt

                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }

        NoteMarketPlaceEntities dc = new NoteMarketPlaceEntities();
        
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [Authorize]
        public ActionResult BuyerRequest(string serachText, string SortOrder, string SortBy, int PageNumber = 1)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.SortBy = SortBy;

            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var sellerID = v.ID;
            var res = dc.Downloads.Where(a => a.Seller == sellerID).ToList();
            if(serachText != null)
            {
                res = dc.Downloads.Where(x => x.NoteTitle.Contains(serachText) || x.NoteCategory.Contains(serachText) || x.User1.Email.Contains(serachText)).ToList();
               res= ApplySorting(SortOrder, SortBy, res);

                res = ApplyPagination(res, PageNumber);

            }
            else
            {
                res = ApplySorting(SortOrder, SortBy, res);

                res = ApplyPagination(res, PageNumber);

            }

            return View(res);

        }
      
        [Authorize]
        public ActionResult DashBoard()
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var sellerID = v.ID;
            var res = dc.SellerNotes.Where(a => a.SellerID == sellerID).ToList();
            return View(res);
        }
        
        public ActionResult FAQ()
        {
            return View();
        }
        
        public List<Download> ApplySorting(string SortOrder, string SortBy,List<Download>  res)
        {
            switch (SortBy)
            {
                case "NoteTitle":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    res = res.OrderBy(x => x.NoteTitle).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    res = res.OrderByDescending(x => x.NoteTitle).ToList();
                                    break;
                                }
                            default:
                                {
                                    res = res.OrderBy(x => x.NoteTitle).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                case "Category":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    res = res.OrderBy(x => x.NoteCategory).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    res = res.OrderByDescending(x => x.NoteCategory).ToList();
                                    break;
                                }
                            default:
                                {
                                    res = res.OrderBy(x => x.NoteCategory).ToList();
                                    break;
                                }
                        }
                        break;
                    }

                case "Buyer":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    res = res.OrderBy(x => x.User1.Email).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    res = res.OrderByDescending(x => x.User1.Email).ToList();
                                    break;
                                }
                            default:
                                {
                                    res = res.OrderBy(x => x.User1.Email).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                case "Price":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    res = res.OrderBy(x => x.PurchasedPrice).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    res = res.OrderByDescending(x => x.PurchasedPrice).ToList();
                                    break;
                                }
                            default:
                                {
                                    res = res.OrderBy(x => x.PurchasedPrice).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                case "DownDate":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    res = res.OrderBy(x => x.AttachmentDownloadedDate).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    res = res.OrderByDescending(x => x.AttachmentDownloadedDate).ToList();
                                    break;
                                }
                            default:
                                {
                                    res = res.OrderBy(x => x.AttachmentDownloadedDate).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        res = res.OrderBy(x => x.AttachmentDownloadedDate).ToList();
                        break;
                    }
            }
            return res;
        }

        public List<Download> ApplyPagination(List<Download> res, int PageNumber = 1)
        {
            ViewBag.TotalPages = Math.Ceiling(res.Count() / 3.0);
            ViewBag.PageNumber = PageNumber;
            res = res.Skip((PageNumber - 1) * 3).Take(3).ToList();
            return res;

        }


        [Authorize]
        public ActionResult SellNote()
        {
            var list = dc.NoteCategories.ToList();
            ViewBag.List = new SelectList(list, "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult SellNote(AddNote model)
        {
            string filename = Path.GetFileNameWithoutExtension(model.SellerNote.DisplayFile.FileName);
            return View();
        }
    }
}