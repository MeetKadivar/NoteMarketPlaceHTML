using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
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
            var profile = dc.UserProfiles.Where(a=>a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
           
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

            if(res.Count() == 0)
            {
                ViewBag.Result = "Not";
            }
            
            return View(res);

        }
      
        [Authorize]
        public ActionResult DashBoard()
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var sellerID = v.ID;
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            var res = dc.SellerNotes.Where(a => a.SellerID == sellerID).ToList();

           ViewBag.TotalDownload = dc.Downloads.Where(a => (a.Seller == sellerID) && (a.IsSellerHasAllowedDownload == true)).Count();
           ViewBag.TotalMoney = dc.Downloads.Where(a => (a.Seller == sellerID) && (a.IsSellerHasAllowedDownload == true)).Sum(a => a.PurchasedPrice);
            ViewBag.TotalUserDownload = dc.Downloads.Where(a => a.Downloader == sellerID).Count();
            ViewBag.TotalRejected = dc.SellerNotes.Where(a => (a.SellerID == sellerID) && (a.Status == 10)).Count();
            ViewBag.TotalBuyerRequest = dc.Downloads.Where(a => (a.Seller == sellerID) && (a.IsSellerHasAllowedDownload == false)).Count();
            

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
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
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

            SendEmail("meetkadivar88@gmail.com", "publishnote");

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

            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
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

            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }


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
            string userId1 = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId1).FirstOrDefault();

            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
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
                                                    PhoneNumber = model.PhoneNumber,
                                                    ProfilePicture = filename,
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

            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            var buyerid = v.ID;
            var res = dc.Downloads.Where(a => a.Downloader == buyerid).ToList();
            return View(res);
        }

        [Authorize]
        public ActionResult SoldNote()
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            var buyerid = v.ID;
            var res = dc.Downloads.Where(a => a.Downloader == buyerid).ToList();

            return View(res);
        }

        [Authorize]
        public ActionResult RejectedNote()
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
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
            var email = seller.User.Email;
            Download down = new Download()
            {
                NoteID = id,
                Seller = seller_id,
                Downloader = Down_Id,
                IsSellerHasAllowedDownload = false,
                AttachmentPath = null,
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

            SendEmail(email,"buyerrequest");
            return RedirectToAction("NoteDetails", new { id = id });
        }

        [Authorize]
        public ActionResult EditNote(int id)
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
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

            string userId1 = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId1).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }

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
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                ViewBag.ProfileUrl = "person_1.jpg";
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            var sellerid = v.ID;
            var downid = dc.Downloads.Where(a => (a.Seller == sellerid) && (a.NoteID == id) && (a.Downloader == downloader)).FirstOrDefault();
            downid.IsSellerHasAllowedDownload = true;
            dc.SaveChanges();
            var u = dc.Users.Where(a => a.ID == downloader).FirstOrDefault();
            var email = u.Email;
            SendEmail(email, "allowdownload");
            return RedirectToAction("BuyerRequest");
            
        }
      
        public ActionResult AddReview(int id,string cmt,int rating)
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var downloader = v.ID;
            var vs = dc.Downloads.Where(a => (a.NoteID == id) && (a.Downloader == downloader)).FirstOrDefault();
            var downloadid = vs.ID;
            SellerNotesReview re = new SellerNotesReview()
            {
                NoteID = id,
                Comments = cmt,
                ReviewedByID = downloader,
                AgainstDownloadsID = downloadid,
                Ratings = rating,
                CreatedDate = DateTime.Now,
                CreatedBy =downloader,
                IsActive = true
            };
            dc.SellerNotesReviews.Add(re);
            dc.SaveChanges();
            return RedirectToAction("Download");
        }

        public ActionResult AddIssue(int id,int noteid)
        {

            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var downloader = v.ID;
            SellerNotesReportedIssue issue = new SellerNotesReportedIssue()
            {
                NoteID=noteid,
                ReportedByID=downloader,
                AgainstDownloadID=id,
                Remarks="issue",
                CreatedDate=DateTime.Now,
                CreatedBy = downloader
            };
            dc.SellerNotesReportedIssues.Add(issue);
            dc.SaveChanges();

            SendEmail("meetkadivar88@gmail.com", "reportspam");
            return RedirectToAction("Download");
        }

        [Authorize]
        [HttpGet]
        public ActionResult AdminDashBoard(int? month)
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v.RoleID;
            if(v.RoleID == 2 || v.RoleID ==3)
            {
                List<TotalDownload> resquery = dc.Downloads.Where(a => a.IsAttachmentDownloaded == true).GroupBy(a => a.NoteID).Select(a => new TotalDownload { Note = a.Key, Count = a.Count() }).ToList();
                var date7 = DateTime.Now.Date.AddDays(-7);
                ViewBag.List = resquery;
                ViewBag.TotalReview = dc.SellerNotes.Where(a => a.Status == 7).Count();
                ViewBag.TotalDownload = dc.Downloads.Where(a => a.AttachmentDownloadedDate >= date7).Count();
                ViewBag.TotalMember = dc.Users.Where(a => a.CreatedDate >= date7).Count();
                if(month == null)
                {
                    var res = dc.SellerNotes.Where(a => a.Status == 9).ToList();
                    return View(res);
                }
                else
                {
                    if (month == 1)
                    {
                        var date = DateTime.Now.AddMonths(-1);
                        var res = dc.SellerNotes.Where(a => (a.Status == 9) && (a.CreatedDate > date)).ToList();
                        return View(res);
                    }
                    else if (month == 2)
                    {
                        var date = DateTime.Now.AddMonths(-2);
                        var res = dc.SellerNotes.Where(a => (a.Status == 9) && (a.CreatedDate > date)).ToList();
                        return View(res);
                    }
                    else if (month == 3)
                    {
                        var date = DateTime.Now.AddMonths(-3);
                        var res = dc.SellerNotes.Where(a => (a.Status == 9) && (a.CreatedDate > date)).ToList();
                        return View(res);
                    }
                    else if (month == 4)
                    {
                        var date = DateTime.Now.AddMonths(-4);
                        var res = dc.SellerNotes.Where(a => (a.Status == 9) && (a.CreatedDate > date)).ToList();
                        return View(res);
                    }
                    else if (month == 5)
                    {
                        var date = DateTime.Now.AddMonths(-5);
                        var res = dc.SellerNotes.Where(a => (a.Status == 9) && (a.CreatedDate > date)).ToList();
                        return View(res);
                    }
                    else
                    {
                        var date = DateTime.Now.AddMonths(-6);
                        var res = dc.SellerNotes.Where(a => (a.Status == 9) && (a.CreatedDate > date)).ToList();
                        return View(res);
                    }





                }

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        [Authorize]
        public ActionResult NoteUnderReview( string seller="")
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v.RoleID;
            if (seller == null || seller == "")
            {
                var res = dc.SellerNotes.Where(a => (a.Status == 7) || (a.Status == 8)).ToList();
                ViewBag.Seller = "none";
                return View(res);
            }
            else
            {
                var res = dc.SellerNotes.Where(a => (a.Status == 7) || (a.Status == 8));
                var res1 = res.Where(a => a.User.FirstName == seller).ToList();
                ViewBag.Seller = seller;
                return View(res1);
            }
         
        }
        [Authorize]
        public ActionResult AdminDownload(string title,string seller,string buyer)
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v.RoleID;
            if ((title == null || title == "undefined") && (seller == null || seller == "undefined") && (buyer == null || buyer == "undefined"))
            {
                var res = dc.Downloads.OrderBy(a => a.AttachmentDownloadedDate).Where(a => a.AttachmentDownloadedDate != null).ToList();

                return View(res);
            }
            else
            {
                ViewBag.Title = title;
                ViewBag.Seller = seller;
                ViewBag.Buyer = buyer;
                int sid=0;
                int bid = 0;
                if(seller !=null && seller != "undefined")
                {
                    var s = dc.Users.Where(a => a.FirstName == seller).FirstOrDefault();
                    sid = s.ID;
                }
                if (buyer != null && buyer != "undefined")
                {
                    var b = dc.Users.Where(a => a.FirstName == buyer).FirstOrDefault();
                    bid = b.ID;
                }

                var res = dc.Downloads.OrderBy(a => a.AttachmentDownloadedDate).Where(a => a.AttachmentDownloadedDate != null).ToList();
                var res1 = res.Where(a => (a.NoteTitle == title) || (a.Seller == sid) || (a.Downloader == bid)).ToList();
                return View(res1);
            }
           
        }
        [Authorize]
        public ActionResult AdminPublishedNote(string seller)
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v.RoleID;
            if (seller == null || seller== "undefined" )
            {
                var res = dc.SellerNotes.OrderBy(a => a.PublishedDate).Where(a => a.Status == 9).ToList();
                return View(res);
            }
            else
            {
                ViewBag.Seller = seller;
                var res = dc.SellerNotes.OrderBy(a => a.PublishedDate).Where(a => (a.Status == 9) && (a.User.FirstName == seller)).ToList();
                return View(res);
            }
           
        }
        [Authorize]
        public ActionResult AdminRejectedNote(string seller)
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v.RoleID;
            if (seller == null || seller == "undefined")
            {
                var res = dc.SellerNotes.OrderBy(a => a.PublishedDate).Where(a => a.Status == 10).ToList();
                return View(res);
            }
            else
            {
                ViewBag.Seller = seller;
                var res = dc.SellerNotes.OrderBy(a => a.PublishedDate).Where(a => a.Status == 10).ToList();

                return View(res);
            }
           
        }
        [Authorize]
        public ActionResult AdminMember()
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v.RoleID;
            List<TotalUnderReview> underReview = dc.SellerNotes.Where(a => ((a.Status == 7) || (a.Status == 8))&&(a.IsActive == true)).GroupBy(a => a.SellerID).Select(a => new TotalUnderReview { ID = a.Key, Count = a.Count() }).ToList();
            ViewBag.NoteUnderReview = underReview;
            List<TotalPublish> resquery = dc.SellerNotes.Where(a => (a.Status == 9 )&&(a.IsActive==true)).GroupBy(a => a.SellerID).Select(a => new TotalPublish { ID = a.Key, Count = a.Count() }).ToList();
            ViewBag.List = resquery;
            List<TotalDownload> resquery_1 = dc.Downloads.Where(a => a.IsAttachmentDownloaded == true).GroupBy(a => a.Seller).Select(a => new TotalDownload { Note = a.Key, Count = a.Count() }).ToList();
            ViewBag.ListOfDownload = resquery_1;
            List<TotalMoney> earning = dc.Downloads.Where(a => a.IsAttachmentDownloaded == true).GroupBy(a => a.Seller).Select(a => new TotalMoney { id = a.Key, Money = a.Sum(b => b.PurchasedPrice) }).ToList();
            ViewBag.TotalEarnings = earning;
            List<TotalMoney> expanse = dc.Downloads.Where(a => a.IsAttachmentDownloaded == true).GroupBy(a => a.Downloader).Select(a => new TotalMoney { id = a.Key, Money = a.Sum(b => b.PurchasedPrice) }).ToList();
            ViewBag.TotalExpanse = expanse;
            var res = dc.Users.OrderByDescending(a => a.CreatedDate).Where(a => (a.RoleID == 1) && (a.IsActive == true)).ToList();
            return View(res);
        }
        [Authorize]
        public ActionResult AdminMemberDetails(int id)
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile1 = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile1 == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile1.ProfilePicture;
            }
            ViewBag.Role = v.RoleID;
            var profile = dc.UserProfiles.Where(a => a.UserID == id).FirstOrDefault();
            var notes = dc.SellerNotes.Where(a => a.SellerID == id).ToList();
            List<TotalDownload> down = dc.Downloads.Where(a => (a.Seller == id) && (a.IsAttachmentDownloaded == true)).GroupBy(a => a.NoteID).Select(a => new TotalDownload { Note = a.Key, Count = a.Count() }).ToList();
            ViewBag.TotalDownload = down;
            List<TotalMoney> money = dc.Downloads.Where(a => (a.Seller == id) && (a.IsAttachmentDownloaded == true)).GroupBy(a => a.NoteID).Select(a => new TotalMoney { id = a.Key, Money = a.Sum(b => b.PurchasedPrice) }).ToList();
            ViewBag.TotalMoney = money;
            MemberDetails md = new MemberDetails() { note = notes,
            up = profile
            };

            return View(md);
        }
       
        [Authorize]
        public ActionResult UnPublishNote(int id, string cmt)
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v.RoleID;
            var res = dc.SellerNotes.Where(a => a.ID == id).FirstOrDefault();

            
            res.Status = 11;
            res.AdminRemarks = cmt;
          
            dc.SaveChanges();
            SendEmail(res.User.Email, "unpublishnote");
            return RedirectToAction("AdminDashBoard");
        }
        [Authorize]
        public ActionResult ManageAdmin()
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v.RoleID;
            if (v.RoleID == 3)
            {
                var res = dc.UserProfiles.Where(a => a.User.RoleID == 2).ToList();
                return View(res);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

              
        }
        [HttpGet]
        [Authorize]
        public ActionResult AdminUpdateProfile()
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v.RoleID;
            if (v.RoleID == 2 || v.RoleID == 3)
            {
                var phoneCode = dc.Countries.ToList();
                ViewBag.PhoneCode = new SelectList(phoneCode, "ID", "CountryCode");

                ViewBag.Email = userId;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
                
        }

        [Authorize]
        [HttpPost]
        public ActionResult AdminUpdateProfile(UserProfile model)
        {
          
                string userId = User.Identity.Name;
                var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v.RoleID;
                v.FirstName = model.FristName;
                v.LastName = model.LastName;
                dc.SaveChanges();
                string filename = Path.GetFileNameWithoutExtension(model.DisplayFile.FileName);
                string extension = Path.GetExtension(model.DisplayFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;

                string full_filename = Path.Combine(Server.MapPath("~/Images/Admin/"), filename);
                model.DisplayFile.SaveAs(full_filename);

                var code = Convert.ToInt32(model.CountryCode);
                var codeId = dc.Countries.Where(a => a.ID == code).FirstOrDefault();
                var countrycode = codeId.CountryCode;

            var check = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (check != null)
            {
                check.CountryCode = countrycode;
                check.PhoneNumber = model.PhoneNumber;
                check.ProfilePicture = filename;
                check.SecondaryEmailAddres = model.SecondaryEmailAddres;
                check.ModifiedBy = v.ID;
                check.ModifiedDate = DateTime.Now;
                dc.SaveChanges();


                var phoneCode = dc.Countries.ToList();
                ViewBag.PhoneCode = new SelectList(phoneCode, "ID", "CountryCode");

                ViewBag.Email = userId;

                ModelState.Clear();
                return View();
            }
            else 
            {
                UserProfile UP = new UserProfile()
                {
                    UserID = v.ID,
                    DOB = DateTime.Now,
                    Gender = 1,
                    CountryCode = countrycode,
                    PhoneNumber = model.PhoneNumber,
                    ProfilePicture = filename,
                    AddressLine1 = "xyz",
                    AddressLine2 = "abc",
                    City = "Rajkot",
                    State = "Gujarat",
                    ZipCode = "360001",
                    Country = "India",
                    University = "CVM",
                    College = "ADIT",
                    CreatedDate = DateTime.Now,
                    CreatedBy = v.ID,
                    SecondaryEmailAddres = model.SecondaryEmailAddres,
                    IsActive = true

                };


                dc.UserProfiles.Add(UP);



                var phoneCode = dc.Countries.ToList();
                ViewBag.PhoneCode = new SelectList(phoneCode, "ID", "CountryCode");

                ViewBag.Email = userId;

                dc.SaveChanges();
                ModelState.Clear();
                return View();

            }



        }
        [Authorize]
        public ActionResult AddAdmin()
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v.RoleID;
            if (v.RoleID == 3)
            {
                var phoneCode = dc.Countries.ToList();
                ViewBag.PhoneCode = new SelectList(phoneCode, "ID", "CountryCode");

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddAdmin (User modal)
        {
            string userId = User.Identity.Name;
            var v = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v.RoleID;
            var email = dc.Users.Where(a => a.Email == modal.Email).FirstOrDefault();
            if(email != null)
            {
                ViewBag.EmailError = "Email already Exist";
                return View();

            }
            else
            {
                User u = new User() { 
                    FirstName = modal.FirstName,
                    LastName = modal.LastName,
                    Email = modal.Email,
                    IsEmailVerify = true,
                    RoleID = 2,
                    ActivationCode = null,
                    Password = "1234",
                    ResetPasswordCode=null,
                    CreatedBy = v.ID,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };
                dc.Users.Add(u);
                dc.SaveChanges();

                var code = Convert.ToInt32(modal.CountryCode);
                var codeId = dc.Countries.Where(a => a.ID == code).FirstOrDefault();
                var countrycode = codeId.CountryCode;
                var id = dc.Users.Where(a => a.Email == modal.Email).FirstOrDefault();

                UserProfile up = new UserProfile() { 
                    UserID = id.ID,
                    DOB = DateTime.Now,
                    Gender = 1,
                    CountryCode = countrycode,
                    PhoneNumber = modal.phonenumber,
                    ProfilePicture = null,
                    AddressLine1 = "xyz",
                    AddressLine2 = "abc",
                    City = "Rajkot",
                    State = "Gujarat",
                    ZipCode = "360001",
                    Country = "India",
                    University = "CVM",
                    College = "ADIT",
                    CreatedDate = DateTime.Now,
                    CreatedBy = v.ID,
                    SecondaryEmailAddres = null,
                    IsActive = true

                };
                dc.UserProfiles.Add(up);
                dc.SaveChanges();


                var phoneCode = dc.Countries.ToList();
                ViewBag.PhoneCode = new SelectList(phoneCode, "ID", "CountryCode");
                return View();
            }


        }
        [Authorize]
        public ActionResult EditAdmin(int id)
        {
            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;

            var phoneCode = dc.Countries.ToList();
            ViewBag.PhoneCode = new SelectList(phoneCode, "ID", "CountryCode");
            var v = dc.Users.Where(a => a.ID == id).FirstOrDefault();
            return View(v);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditAdmin(User modal)
        {
            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;
            var phoneCode = dc.Countries.ToList();
            ViewBag.PhoneCode = new SelectList(phoneCode, "ID", "CountryCode");

            var check = dc.Users.Where(a => a.Email == modal.Email).FirstOrDefault();
            if(check != null)
            {
                ViewBag.EmailError = "Email alrady exists";
                return View();
            }
            else
            {
                var v = dc.Users.Where(a => a.ID == modal.ID).FirstOrDefault();
                v.FirstName = modal.FirstName;
                v.LastName = modal.LastName;
                v.Email = modal.Email;
                var up = dc.UserProfiles.Where(a => a.UserID == modal.ID).FirstOrDefault();

                var code = Convert.ToInt32(modal.CountryCode);
                var codeId = dc.Countries.Where(a => a.ID == code).FirstOrDefault();
                var countrycode = codeId.CountryCode;

                up.CountryCode = countrycode;
                up.PhoneNumber = modal.phonenumber;

                dc.SaveChanges();
                ModelState.Clear();
                return View();
            }
            
        }

        
        public ActionResult ApproveNote(int id)
        {
            var v = dc.SellerNotes.Where(a => a.ID == id).FirstOrDefault();
            v.Status = 9;
            dc.SaveChanges();
            
            return RedirectToAction("NoteUnderReview", "Home");
        }

        public ActionResult InReviewNote(int id)
        {
            var v = dc.SellerNotes.Where(a => a.ID == id).FirstOrDefault();
            v.Status = 8;
            dc.SaveChanges();
            return RedirectToAction("NoteUnderReview", "Home");
        }

        public ActionResult RejectNote(int id,string cmt)
        {
            var v = dc.SellerNotes.Where(a => a.ID == id).FirstOrDefault();
            v.Status = 10;
            v.AdminRemarks = cmt;
            dc.SaveChanges();
            return RedirectToAction("NoteUnderReview", "Home");
        }

        public ActionResult AdminMemberDeactive(int id)
        {
            var v = dc.Users.Where(a => a.ID == id).FirstOrDefault();
            v.IsActive = false;
            var res = dc.SellerNotes.Where(a => a.SellerID == id).ToList();
            res.ForEach(a => a.Status = 10);
            dc.SaveChanges();
           
            return RedirectToAction("AdminMember","Home");
        }

        public ActionResult DeleteAdmin(int id)
        {
            var v = dc.Users.Where(a => a.ID == id).FirstOrDefault();
            v.IsActive = false;
            dc.SaveChanges();
            return RedirectToAction("ManageAdmin","Home");
        }

        public ActionResult DownloadFileEntry(int id,string buyer)
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
                IsSellerHasAllowedDownload = true,
                AttachmentPath = path,
                IsAttachmentDownloaded = true,
                IsPaid = false,
                PurchasedPrice = 0,
                NoteTitle = seller.Title,
                NoteCategory = seller.NoteCategory.Name,
                CreatedDate = DateTime.Now,
                CreatedBy = Down_Id,
                AttachmentDownloadedDate = DateTime.Now


            };
            dc.Downloads.Add(down);
            dc.SaveChanges();
          
            
            return RedirectToAction("DownloadFile", new { id = id });
        }

        [NonAction]
        public void SendEmail(string email, string emailfor = "buyerrequest")
        {
            var fromEmail = new MailAddress("meetkadivar88@gmail.com", "Meet");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "123@Meet"; //Password
            string subject = "";
            string body = "";


            if (emailfor == "buyerrequest")
            {
                subject = "Someone want to purchase your notes!";

                body = "Hello, <br/>" +
                    "We would like to inform you that,Someone want to purchase your notes.Please see Buyer Requests tab and allow download access to Buyer if you have received the payment from him.<br/><br/>"+
                    "Regards,<br/>Notes Marketplace";


            }

            else if(emailfor == "allowdownload")
            {
                subject = "Your Request for note is Approve!";
                body = "Hello,<br/>" +
                    "We would like to inform you that,Seller Allows you to download a note.<br/>Please login and see My Download tabs to download particular note.<br/><br/>" +
                    "Regards,<br/>Notes Marketplace";
            }

            else if(emailfor == "reportspam")
            {
                subject = "Someone reported an issue for Note";
                body = "Hello Admins,<br/>" +
                    "We want to inform you that,Someone Reported an issue for Note with title.Please look at the notes and take required actions.<br/><br/>" +
                    "Regards,<br/>Notes Marketplace";
            }

            else if(emailfor == "publishnote")
            {
                subject = "Someone sent his note for review";
                body = "Hello Admins,<br/>" +
                    "We want to inform you that, Someone sent his note for review.Please look at the notes and take required actions<br/><br/>." +
                    "Regards,<br/>Notes Marketplace";
            }

            else if(emailfor == "unpublishnote")
            {
                subject = "Sorry! We need to remove your notes from our portal.";
                body = "Hello,Seller" +
                    "We want to inform you that,your note has been removed from the portal.<br/>" +
                    "Please find our remarks as below-<br/><br/><br/" +
                    "Regards,<br/>Notes Marketplace";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)


            };
            using (var Message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

                smtp.Send(Message);


        }
        [Authorize]
        public ActionResult ManageCategory()
        {
            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;


            var res = dc.NoteCategories.ToList();
            return View(res);
        }

        [Authorize]
        public ActionResult EditCategory(int id)
        {
            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;

            var res = dc.NoteCategories.Where(a => a.ID == id).FirstOrDefault();
            return View(res);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditCategory(NoteCategory modal)
        {

            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;

            var res = dc.NoteCategories.Where(a => a.ID == modal.ID).FirstOrDefault();
            res.Name = modal.Name;
            res.Description = modal.Description;
            dc.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("ManageCategory", "Home");
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult AddCategory(NoteCategory modal)
        {
            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;

            NoteCategory cat = new NoteCategory() {
                Name = modal.Name,
                Description = modal.Description,
                CreatedBy = v1.ID,
                IsActive = true,
                CreatedDate = DateTime.Now

            };
            dc.NoteCategories.Add(cat);
            dc.SaveChanges();


            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddCategory()
        {
            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;
            return View();
        }

        public ActionResult DeleteCategory(int id)
        {
            var v = dc.NoteCategories.Where(a => a.ID == id).FirstOrDefault();
            v.IsActive = false;
            dc.SaveChanges();
            return RedirectToAction("ManageCategory", "Home");
        }


        [Authorize]
        public ActionResult ManageType()
        {
            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;
            var res = dc.NoteTypes.ToList();
            return View(res);
        }


        [Authorize]
        public ActionResult EditType(int id)
        {
            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;

            var res = dc.NoteTypes.Where(a => a.ID == id).FirstOrDefault();
            return View(res);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditType(NoteType modal)
        {

            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;

            var res = dc.NoteTypes.Where(a => a.ID == modal.ID).FirstOrDefault();
            res.Name = modal.Name;
            res.Description = modal.Description;
            dc.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("ManageType", "Home");
        }

        public ActionResult DeleteType(int id)
        {
            var v = dc.NoteTypes.Where(a => a.ID == id).FirstOrDefault();
            v.IsActive = false;
            dc.SaveChanges();
            return RedirectToAction("ManageType", "Home");
        }


        [Authorize]
        [HttpPost]
        public ActionResult AddType(NoteType modal)
        {
            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;

            NoteType cat = new NoteType()
            {
                Name = modal.Name,
                Description = modal.Description,
                CreatedBy = v1.ID,
                IsActive = true,
                CreatedDate = DateTime.Now

            };
            dc.NoteTypes.Add(cat);
            dc.SaveChanges();


            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddType()
        {
            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;
            return View();
        }


        [Authorize]
        public ActionResult ManageCountry()
        {
            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;
            var res = dc.Countries.ToList();
            return View(res);
        }

        [Authorize]
        public ActionResult EditCountry(int id)
        {
            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;

            var res = dc.Countries.Where(a => a.ID == id).FirstOrDefault();
            return View(res);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditCountry(Country modal)
        {

            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;

            var res = dc.Countries.Where(a => a.ID == modal.ID).FirstOrDefault();
            res.Name = modal.Name;
            res.CountryCode = modal.CountryCode;
            dc.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("ManageCountry", "Home");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddCountry(Country modal)
        {
            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;

            Country cat = new Country()
            {
                Name = modal.Name,
                CountryCode= modal.CountryCode,
                CreatedBy = v1.ID,
                IsActive = true,
                CreatedDate = DateTime.Now

            };
            dc.Countries.Add(cat);
            dc.SaveChanges();


            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddCountry()
        {
            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;
            return View();
        }

        public ActionResult DeleteCountry(int id)
        {
            var v = dc.Countries.Where(a => a.ID == id).FirstOrDefault();
            v.IsActive = false;
            dc.SaveChanges();
            return RedirectToAction("ManageCountry", "Home");
        }


        [Authorize]
        public ActionResult ManageSystemConfi ()
        {
            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ManageSystemConfi(SystemConfiguration modal)
        {
            string userId = User.Identity.Name;
            var v1 = dc.Users.Where(a => a.Email == userId).FirstOrDefault();
            var profile = dc.UserProfiles.Where(a => a.UserID == v1.ID).FirstOrDefault();
            if (profile == null)
            {
                var r = dc.SystemConfigurations.Where(a=>a.Name == "Default Member Picture").FirstOrDefault();
                ViewBag.ProfileUrl = r.Value;
            }
            else
            {
                ViewBag.ProfileUrl = profile.ProfilePicture;
            }
            ViewBag.Role = v1.RoleID;


            string filename = Path.GetFileNameWithoutExtension(modal.DisplayFile.FileName);
            string extension = Path.GetExtension(modal.DisplayFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;

            string full_filename = Path.Combine(Server.MapPath("~/Images/Default/Member"), filename);
            modal.DisplayFile.SaveAs(full_filename);


            string filename1 = Path.GetFileNameWithoutExtension(modal.NoteFile.FileName);
            string extension1 = Path.GetExtension(modal.NoteFile.FileName);
            filename1 = filename1 + DateTime.Now.ToString("yymmssfff") + extension1;

            string full_filename1 = Path.Combine(Server.MapPath("~/Images/Default/Note/"), filename1);
            modal.DisplayFile.SaveAs(full_filename1);

            var c1 = dc.SystemConfigurations.Where(a => a.Name == "Email").FirstOrDefault();
            c1.Value = modal.Value;


            var c2 = dc.SystemConfigurations.Where(a => a.Name == "PhoneNumber").FirstOrDefault();
            c2.Value = modal.phonenumber;

            var c3 = dc.SystemConfigurations.Where(a => a.Name == "Event Email").FirstOrDefault();
            c3.Value = modal.evntemail;

            var c4 = dc.SystemConfigurations.Where(a => a.Name == "FaceBook Url").FirstOrDefault();
            c4.Value = modal.fburl;


            var c5 = dc.SystemConfigurations.Where(a => a.Name == "Twitter Url").FirstOrDefault();
            c5.Value = modal.twiterurl;

            var c6 = dc.SystemConfigurations.Where(a => a.Name == "Linkedln Url").FirstOrDefault();
            c6.Value = modal.ldurl;

            var c7 = dc.SystemConfigurations.Where(a => a.Name == "Default Member Picture").FirstOrDefault();
            c7.Value = filename;

            var c8 = dc.SystemConfigurations.Where(a => a.Name == "Default Note Picture").FirstOrDefault();
            c8.Value = filename1;

            dc.SaveChanges();
            ModelState.Clear();



            return View();
        }



    }
}