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
    
    public partial class ManualDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ManualDocument()
        {
            this.ManualPartDocuments = new HashSet<ManualPartDocument>();
            this.ManualPartSuggestionDocuments = new HashSet<ManualPartSuggestionDocument>();
            this.ManualPartTemplateDocuments = new HashSet<ManualPartTemplateDocument>();
            this.OfficeManualDocuments = new HashSet<OfficeManualDocument>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descrition { get; set; }
        public int ManualDocumentCategoryId { get; set; }
        public bool Deleted { get; set; }
        public string DocPath { get; set; }
        public Nullable<int> SwedAreaId { get; set; }
    
        public virtual ManualDocumentCategory ManualDocumentCategory { get; set; }
        public virtual SwedArea SwedArea { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManualPartDocument> ManualPartDocuments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManualPartSuggestionDocument> ManualPartSuggestionDocuments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManualPartTemplateDocument> ManualPartTemplateDocuments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfficeManualDocument> OfficeManualDocuments { get; set; }
    }
}