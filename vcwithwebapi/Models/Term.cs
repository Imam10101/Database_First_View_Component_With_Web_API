using System;
using System.Collections.Generic;

namespace vcwithwebapi.Models
{
    public partial class Term
    {
        public Term()
        {
            Invoices = new HashSet<Invoice>();
            Vendors = new HashSet<Vendor>();
        }

        public int TermsId { get; set; }
        public string TermsDescription { get; set; } = null!;
        public short TermsDueDays { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
