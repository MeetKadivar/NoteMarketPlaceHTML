using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteMarketPlace.Models
{
    public class NoteListing
    {
        public List<SellerNote> sn { get; set; }
        public SellerNotesReportedIssue isuue { get; set; }
        public SellerNotesReview review { get; set; }
    }
}