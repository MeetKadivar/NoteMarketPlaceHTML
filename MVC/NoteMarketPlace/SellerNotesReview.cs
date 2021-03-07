//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NoteMarketPlace
{
    using System;
    using System.Collections.Generic;
    
    public partial class SellerNotesReview
    {
        public int ID { get; set; }
        public int NoteID { get; set; }
        public int ReviewedByID { get; set; }
        public int AgainstDownloadsID { get; set; }
        public double Ratings { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Download Download { get; set; }
        public virtual SellerNote SellerNote { get; set; }
        public virtual User User { get; set; }
    }
}
