using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteMarketPlace.Models
{
    public class NoteDetails
    {
        public SellerNote sellernote { get; set; }
        public List<SellerNotesReview> review { get; set; }
        public SellerNotesReportedIssue issue { get; set; }
        public UserProfile up { get; set; }
    }
}