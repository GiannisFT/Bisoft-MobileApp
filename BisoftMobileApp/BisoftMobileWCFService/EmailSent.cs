//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BisoftMobileWCFService
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailSent
    {
        public int Id { get; set; }
        public System.DateTime Created { get; set; }
        public int ModuleId { get; set; }
        public string ToAdress { get; set; }
        public string FromAdress { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int OfficeId { get; set; }
        public bool Automatic { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public bool Sent { get; set; }
        public string SendMessage { get; set; }
        public string IcsFile { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Module Module { get; set; }
        public virtual Office Office { get; set; }
    }
}
