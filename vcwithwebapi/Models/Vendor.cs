using System;
using System.Collections.Generic;

namespace vcwithwebapi.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int VendorId { get; set; }
        public string VendorName { get; set; } = null!;
        public string? VendorAddress1 { get; set; }
        public string? VendorAddress2 { get; set; }
        public string VendorCity { get; set; } = null!;
        public string VendorState { get; set; } = null!;
        public string VendorZipCode { get; set; } = null!;
        public string? VendorPhone { get; set; }
        public string? VendorContactLname { get; set; }
        public string? VendorContactFname { get; set; }
        public int DefaultTermsId { get; set; }
        public int DefaultAccountNo { get; set; }

        public virtual Glaccount DefaultAccountNoNavigation { get; set; } = null!;
        public virtual Term DefaultTerms { get; set; } = null!;
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
