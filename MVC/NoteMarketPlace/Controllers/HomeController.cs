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
        //private AddNoteBase objAddNoteBase;
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
            var res = dc.Downloads.Where(a => (a.Seller == sellerID) && (a.IsSellerHasAllowedDownload == false)).ToList();
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
        [HttpPost]
        public ActionResult SellNote(AddNote model)
        {
            string filename = Path.GetFileNameWithoutExtension(model.SellerNote.DisplayFile.FileName);
            string extension = Path.GetExtension(model.SellerNote.DisplayFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            
            string full_filename = Path.Combine(Server.MapPath("~/Images/Notes/"), filename);
            model.SellerNote.DisplayFile.SaveAs(full_filename);


            string filename_preview;
            if (model.SellerNote.NotePreviewFile != null)
            {
                
                 filename_preview = Path.GetFileNameWithoutExtension(model.SellerNote.NotePreviewFile.FileName);
                string extension_preview = Path.GetExtension(model.SellerNote.NotePreviewFile.FileName);
                filename_preview = filename_preview + DateTime.Now.ToString("yymmssfff") + extension_preview;

                string full_filename_preview = Path.Combine(Server.MapPath("~/NotePdf/Preview"), filename_preview);
                model.SellerNote.NotePreviewFile.SaveAs(full_filename_preview);

            }
            else
            {
                filename_preview = null;
            }
            

            






            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var sellerID = v.ID;
            double price = 0;
            bool paid;
            if(model.SellerNote.PaidInfo == "Paid")
            {
                paid = true;
                price = (double)model.SellerNote.SellingPrice;
            }
            else
            {
                paid = false;
            }
            



            SellerNote sellernote = new SellerNote() { SellerID = sellerID,
                                                      Status = 7,
                                                      PublishedDate = DateTime.Now,
                                                      Title = model.SellerNote.Title,
                                                      Category = model.SellerNote.Category,
                                                      NoteType = model.SellerNote.NoteType,
                                                      NumberofPages = model.SellerNote.NumberofPages,
                                                      Description = model.SellerNote.Description,
                                                      UniversityName = model.SellerNote.UniversityName,
                                                      Country = model.SellerNote.Country,
                                                      Course = model.SellerNote.Course,
                                                      CourseCode =model.SellerNote.CourseCode,
                                                      Professor = model.SellerNote.Professor,
                                                      IsPaid = paid,
                                                      SellingPrice = price,
                                                      IsActive =true,
                                                      DisplayPicture = filename,
                                                      NotesPreview = filename_preview,
                                                      CreatedDate = DateTime.Now,
                                                      CreatedBy = sellerID
                                                      
                                                    };

           

            dc.SellerNotes.Add(sellernote);
            dc.SaveChanges();

            var noteid = dc.SellerNotes.Where(a => a.DisplayPicture == filename).FirstOrDefault();


            string filename_pdf = Path.GetFileNameWithoutExtension(model.SellerNotesAttachement.NoteFile.FileName);
            string extension_pdf = Path.GetExtension(model.SellerNotesAttachement.NoteFile.FileName);
            filename_pdf = filename_pdf + DateTime.Now.ToString("yymmssfff") + extension_pdf;

            string full_filename_pdf = Path.Combine(Server.MapPath("~/NotePdf/"), filename_pdf);
            model.SellerNotesAttachement.NoteFile.SaveAs(full_filename_pdf);




            SellerNotesAttachement sellernoteattachement = new SellerNotesAttachement()
                                                                { NoteID = noteid.ID,
                                                                  FileName = filename_pdf,
                                                                  FilePath = full_filename_pdf,
                                                                  IsActive = true,
                                                                  CreatedDate = DateTime.Now,
                                                                  CreatedBy = sellerID
                                                                };
            dc.SellerNotesAttachements.Add(sellernoteattachement);
            dc.SaveChanges();


            ModelState.Clear();

            var list = dc.NoteCategories.ToList();
            ViewBag.List = new SelectList(list, "ID", "Name");

            var typelist = dc.NoteTypes.ToList();
            ViewBag.TypeList = new SelectList(typelist, "ID", "Name");

            var countrylist = dc.Countries.ToList();
            ViewBag.CountryList = new SelectList(countrylist, "ID", "Name");

            return View();

        }

        [Authorize]
        [HttpGet]
        public ActionResult SellNote()
        {
            var list = dc.NoteCategories.ToList();
            ViewBag.List = new SelectList(list, "ID", "Name");

            var typelist = dc.NoteTypes.ToList();
            ViewBag.TypeList = new SelectList(typelist, "ID", "Name");

            var countrylist = dc.Countries.ToList();
            ViewBag.CountryList = new SelectList(countrylist, "ID", "Name");


            return View();
        }
      
        [Authorize]
        [HttpGet]
        public ActionResult UserProfile()
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            

            ViewBag.Email = v.Email;
            var countrylist = dc.Countries.ToList();
            ViewBag.CountryList = new SelectList(countrylist, "ID", "Name");

            var phoneCode = dc.Countries.ToList();
            ViewBag.PhoneCode = new SelectList(phoneCode, "ID", "CountryCode");

            var gender = new List<string> { "Male","Female","Other"};
            ViewBag.GenderList = gender;

            return View();
        }

        [HttpPost]
        public ActionResult UserProfile(UserProfile model)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileNameWithoutExtension(model.DisplayFile.FileName);
                string extension = Path.GetExtension(model.DisplayFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;

                string full_filename = Path.Combine(Server.MapPath("~/Images/Member/"), filename);
                model.DisplayFile.SaveAs(full_filename);

                string userId = User.Identity.Name;
                var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
                v.FirstName = model.FristName;
                v.LastName = model.LastName;
                dc.SaveChanges();
              
                var userid = v.ID;

                ViewBag.Email = v.Email;

                var country = Convert.ToInt32(model.Country);
                var countryID = dc.Countries.Where(a => a.ID == country).FirstOrDefault();
                var countryName = countryID.Name;

                var code = Convert.ToInt32(model.CountryCode);
                var codeId = dc.Countries.Where(a => a.ID == code).FirstOrDefault();
                var countrycode = codeId.CountryCode;

                var ph =  countrycode + " " + model.PhoneNumber; 


                var genderid = 0;
                if (model.SelectGender == "Male")
                {
                     genderid = 1;
                }
                else if(model.SelectGender == "Female")
                {
                     genderid = 2;
                }
                else
                {
                     genderid = 3;
                }

                UserProfile UP = new UserProfile()
                                                {
                                                    UserID =userid,
                                                    DOB = model.DOB,
                                                    Gender = genderid,
                                                    CountryCode = countrycode,
                                                    PhoneNumber = ph,
                                                    ProfilePicture = full_filename,
                                                    AddressLine1 = model.AddressLine1,
                                                    AddressLine2 = model.AddressLine2,
                                                    City = model.City,
                                                    State = model.State,
                                                    ZipCode = model.ZipCode,
                                                    Country = countryName,
                                                    University = model.University,
                                                    College = model.College,
                                                    CreatedDate = DateTime.Now,
                                                    CreatedBy = userid,
                                                    IsActive = true

                                                };


                dc.UserProfiles.Add(UP);


               

                dc.SaveChanges();
                ModelState.Clear();

                var countrylist_1 = dc.Countries.ToList();
                ViewBag.CountryList = new SelectList(countrylist_1, "ID", "Name");

                var phoneCode_1 = dc.Countries.ToList();
                ViewBag.PhoneCode = new SelectList(phoneCode_1, "ID", "CountryCode");


                var gender_1 = new List<string> { "Male", "Female", "Other" };
                ViewBag.GenderList = gender_1;

                return View();

            }
            else
            {
                var countrylist = dc.Countries.ToList();
                ViewBag.CountryList = new SelectList(countrylist, "ID", "Name");

                var phoneCode = dc.Countries.ToList();
                ViewBag.PhoneCode = new SelectList(phoneCode, "ID", "CountryCode");


                var gender = new List<string> { "Male", "Female", "Other" };
                ViewBag.GenderList = gender;

                return View();
            }


           
        }

        [Authorize]
        public ActionResult Download()
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var buyerid = v.ID;
            var res = dc.Downloads.Where(a => a.Downloader == buyerid).ToList();
            return View(res);
        }

        [Authorize]
        public ActionResult SoldNote()
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var buyerid = v.ID;
            var res = dc.Downloads.Where(a => a.Downloader == buyerid).ToList();

            return View(res);
        }

        [Authorize]
        public ActionResult RejectedNote()
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var buyerid = v.ID;
            var res = dc.SellerNotes.Where(a => a.SellerID == buyerid).ToList();

            return View(res);
        }

        public FileResult DownloadFile(int id)
        {
            
            var f = dc.SellerNotesAttachements.Where(x => x.NoteID == id).FirstOrDefault();
            string filepath = f.FilePath;
            return File(filepath,"application/pdf","Test.pdf");
        }

        public ActionResult NoteDetails(int id)
        {
            if (Request.IsAuthenticated)
            {
                string email = User.Identity.Name;
                ViewBag.Email = email;
            }
            else
            {
                ViewBag.Email = null;
            }
            var res = dc.SellerNotes.Where(x => x.ID == id).FirstOrDefault();
            ViewBag.url = res.DisplayPicture;
            ViewBag.preview = res.NotesPreview;
            ViewBag.price = '$'+ Convert.ToString(res.SellingPrice);
            ViewBag.id = res.ID;
            var reviewcount = dc.SellerNotesReviews.Where(x => x.NoteID == id).Count();
            if(reviewcount == 0)
            {
                ViewBag.Rating = 0;
                ViewBag.Total = 0;
            }
            else
            {
                var review = dc.SellerNotesReviews.Where(x => x.NoteID == id).Average(x => x.Ratings);
                var totalreview = dc.SellerNotesReviews.Where(x => x.NoteID == id).Count();
                ViewBag.Rating = Math.Ceiling(review);
                ViewBag.Total = totalreview;
            }
            
            
            ViewBag.Issue = dc.SellerNotesReportedIssues.Where(x => x.NoteID == id).Count();          
                
                
            
            if(ViewBag.Issue != 0)
            {
                var Review_Text = dc.SellerNotesReviews.Where(x => x.NoteID == id).ToList();
                NoteDetails nd = new NoteDetails()
                {
                    sellernote = res,
                    review = Review_Text

                };
                return View(nd);
            }
            else
            {
                NoteDetails nd = new NoteDetails()
                {
                    sellernote = res
                };
                return View(nd);
            }
           
            
            
        }

        public ActionResult AddBuyerRequest(int id,string buyer)
        {
            var user = dc.Users.Where(x => x.Email == buyer).FirstOrDefault();
            var Down_Id = user.ID;
            var seller = dc.SellerNotes.Where(x => x.ID == id).FirstOrDefault();
            var seller_id = seller.SellerID;
            var file = dc.SellerNotesAttachements.Where(x => x.NoteID == id).FirstOrDefault();
            var path = file.FilePath;

            Download down = new Download()
            {
                NoteID = id,
                Seller = seller_id,
                Downloader = Down_Id,
                IsSellerHasAllowedDownload = false,
                AttachmentPath = path,
                IsAttachmentDownloaded =false,
                IsPaid = true,
                PurchasedPrice = seller.SellingPrice,
                NoteTitle = seller.Title,
                NoteCategory = seller.NoteCategory.Name,
                CreatedDate = DateTime.Now,
                CreatedBy = Down_Id


            };
            dc.Downloads.Add(down);
            dc.SaveChanges();
            return RedirectToAction("NoteDetails", new { id = id });
        }

        [Authorize]
        public ActionResult EditNote(int id)
        {
            var note = dc.SellerNotes.Where(x => x.ID == id).FirstOrDefault();
            var list = dc.NoteCategories.ToList();
            ViewBag.List = new SelectList(list, "ID", "Name");

            var typelist = dc.NoteTypes.ToList();
            ViewBag.TypeList = new SelectList(typelist, "ID", "Name");

            var countrylist = dc.Countries.ToList();
            ViewBag.CountryList = new SelectList(countrylist, "ID", "Name");

            AddNote add = new AddNote()
            {
                SellerNote = note
            };
            return View(add);
        }
        [HttpPost]
        public ActionResult EditNote(AddNote model)
        {
            
            string filename = Path.GetFileNameWithoutExtension(model.SellerNote.DisplayFile.FileName);
            string extension = Path.GetExtension(model.SellerNote.DisplayFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;

            string full_filename = Path.Combine(Server.MapPath("~/Images/Notes/"), filename);
            model.SellerNote.DisplayFile.SaveAs(full_filename);


            string filename_preview;
            if (model.SellerNote.NotePreviewFile != null)
            {

                filename_preview = Path.GetFileNameWithoutExtension(model.SellerNote.NotePreviewFile.FileName);
                string extension_preview = Path.GetExtension(model.SellerNote.NotePreviewFile.FileName);
                filename_preview = filename_preview + DateTime.Now.ToString("yymmssfff") + extension_preview;

                string full_filename_preview = Path.Combine(Server.MapPath("~/NotePdf/Preview"), filename_preview);
                model.SellerNote.NotePreviewFile.SaveAs(full_filename_preview);

            }
            else
            {
                filename_preview = null;
            }





            var sellerid = dc.SellerNotes.Where(a => a.ID == model.SellerNote.ID).FirstOrDefault();
            dc.SellerNotes.Remove(sellerid);
            var note = dc.SellerNotesAttachements.Where(a => a.NoteID == model.SellerNote.ID).FirstOrDefault();
            dc.SellerNotesAttachements.Remove(note);




            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var sellerID = v.ID;
            double price = 0;
            bool paid;
            if (model.SellerNote.PaidInfo == "Paid")
            {
                paid = true;
                price = (double)model.SellerNote.SellingPrice;
            }
            else
            {
                paid = false;
            }




            SellerNote sellernote = new SellerNote()
            {
                SellerID = sellerID,
                Status = 7,
                PublishedDate = DateTime.Now,
                Title = model.SellerNote.Title,
                Category = model.SellerNote.Category,
                NoteType = model.SellerNote.NoteType,
                NumberofPages = model.SellerNote.NumberofPages,
                Description = model.SellerNote.Description,
                UniversityName = model.SellerNote.UniversityName,
                Country = model.SellerNote.Country,
                Course = model.SellerNote.Course,
                CourseCode = model.SellerNote.CourseCode,
                Professor = model.SellerNote.Professor,
                IsPaid = paid,
                SellingPrice = price,
                IsActive = true,
                DisplayPicture = filename,
                NotesPreview = filename_preview,
                CreatedDate =DateTime.Now,
                CreatedBy = sellerID

            };



            dc.SellerNotes.Add(sellernote);
            dc.SaveChanges();

            var noteid = dc.SellerNotes.Where(a => a.DisplayPicture == filename).FirstOrDefault();


            string filename_pdf = Path.GetFileNameWithoutExtension(model.SellerNotesAttachement.NoteFile.FileName);
            string extension_pdf = Path.GetExtension(model.SellerNotesAttachement.NoteFile.FileName);
            filename_pdf = filename_pdf + DateTime.Now.ToString("yymmssfff") + extension_pdf;

            string full_filename_pdf = Path.Combine(Server.MapPath("~/NotePdf/"), filename_pdf);
            model.SellerNotesAttachement.NoteFile.SaveAs(full_filename_pdf);




            SellerNotesAttachement sellernoteattachement = new SellerNotesAttachement()
            {
                NoteID = noteid.ID,
                FileName = filename_pdf,
                FilePath = full_filename_pdf,
                IsActive = true,
                CreatedDate = DateTime.Now,
                CreatedBy = sellerID
            };
            dc.SellerNotesAttachements.Add(sellernoteattachement);
            dc.SaveChanges();


            ModelState.Clear();

            var list = dc.NoteCategories.ToList();
            ViewBag.List = new SelectList(list, "ID", "Name");

            var typelist = dc.NoteTypes.ToList();
            ViewBag.TypeList = new SelectList(typelist, "ID", "Name");

            var countrylist = dc.Countries.ToList();
            ViewBag.CountryList = new SelectList(countrylist, "ID", "Name");

            return View();
        }
        [Authorize]
        public ActionResult AllowDownload (int id,int downloader)
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var sellerid = v.ID;
            var downid = dc.Downloads.Where(a => (a.Seller == sellerid) && (a.NoteID == id) && (a.Downloader == downloader)).FirstOrDefault();
            downid.IsSellerHasAllowedDownload = true;
            dc.SaveChanges();
            return RedirectToAction("BuyerRequest");
            
        }
    }
}