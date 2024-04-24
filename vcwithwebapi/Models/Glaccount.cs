using System;
using System.Collections.Generic;

namespace vcwithwebapi.Models
{
    public partial class Glaccount
    {
        public Glaccount()
        {
            InvoiceLineItems = new HashSet<InvoiceLineItem>();
            Vendors = new HashSet<Vendor>();
        }

        public int AccountNo { get; set; }
        public string AccountDescription { get; set; } = null!;

        public virtual ICollection<InvoiceLineItem> InvoiceLineItems { get; set; }
        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
