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
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class SellerNote
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SellerNote()
        {
            this.Downloads = new HashSet<Download>();
            this.SellerNotesAttachements = new HashSet<SellerNotesAttachement>();
            this.SellerNotesReportedIssues = new HashSet<SellerNotesReportedIssue>();
            this.SellerNotesReviews = new HashSet<SellerNotesReview>();
        }
    
        public int ID { get; set; }
        public int SellerID { get; set; }
        public int Status { get; set; }
        public Nullable<int> ActionedBy { get; set; }
        public string AdminRemarks { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> PublishedDate { get; set; }
        public string Title { get; set; }
        public int Category { get; set; }
        public string DisplayPicture { get; set; }
        public Nullable<int> NoteType { get; set; }
        public Nullable<int> NumberofPages { get; set; }
        public string Description { get; set; }
        public string UniversityName { get; set; }
        public Nullable<int> Country { get; set; }
        public string Course { get; set; }
        public string CourseCode { get; set; }
        public string Professor { get; set; }
        public bool IsPaid { get; set; }
        public Nullable<double> SellingPrice { get; set; }
        public string NotesPreview { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Country Country1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Download> Downloads { get; set; }
        public virtual NoteCategory NoteCategory { get; set; }
        public virtual NoteType NoteType1 { get; set; }
        public virtual ReferenceData ReferenceData { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SellerNotesAttachement> SellerNotesAttachements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SellerNotesReportedIssue> SellerNotesReportedIssues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SellerNotesReview> SellerNotesReviews { get; set; }
        public HttpPostedFileBase DisplayFile { get; set; }
    }
}
