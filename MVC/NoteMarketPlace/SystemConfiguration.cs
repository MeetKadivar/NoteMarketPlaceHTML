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
    using System.Web;

    public partial class SystemConfiguration
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public string email { get; set; }
        public string phonenumber { get; set; }
        public string evntemail { get; set; }
        public HttpPostedFileBase DisplayFile { get; set; }

        public HttpPostedFileBase NoteFile { get; set; }

        public string fburl { get; set; }
        public string twiterurl { get; set; }
        public string ldurl { get; set; }

    }
}
