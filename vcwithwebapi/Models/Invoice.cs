using System;
using System.Collections.Generic;

namespace vcwithwebapi.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceLineItems = new HashSet<InvoiceLineItem>();
        }

        public int InvoiceId { get; set; }
        public int VendorId { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceTotal { get; set; }
        public decimal PaymentTotal { get; set; }
        public decimal CreditTotal { get; set; }
        public int TermsId { get; set; }
        public DateTime InvoiceDueDate { get; set; }
        public DateTime? PaymentDate { get; set; }

        public virtual Term Terms { get; set; } = null!;
        public virtual Vendor Vendor { get; set; } = null!;
        public virtual ICollection<InvoiceLineItem> InvoiceLineItems { get; set; }
    }
}
