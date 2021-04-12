using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteMarketPlace.Models
{
    public class MemberDetails
    {
        public UserProfile up { get; set; }
        public List<SellerNote> note { get; set; }
    }
}