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
    
    public partial class CarPreSaleLog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CarPreSaleLog()
        {
            this.CarPreSaleLogDocuments = new HashSet<CarPreSaleLogDocument>();
        }
    
        public int Id { get; set; }
        public System.DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public int CarPreSaleId { get; set; }
        public string Description { get; set; }
        public string Header { get; set; }
        public Nullable<bool> IsMaintenance { get; set; }
        public Nullable<bool> IsInspection { get; set; }
        public Nullable<int> CarPreSaleMaintenanceBegId { get; set; }
        public Nullable<int> CarPreSaleMaintenanceLagerId { get; set; }
    
        public virtual CarPreSale CarPreSale { get; set; }
        public virtual CarPreSalesMaintenanceBeg CarPreSalesMaintenanceBeg { get; set; }
        public virtual CarPreSalesMaintenanceLager CarPreSalesMaintenanceLager { get; set; }
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarPreSaleLogDocument> CarPreSaleLogDocuments { get; set; }
    }
}