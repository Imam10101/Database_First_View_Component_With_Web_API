using System;
using System.Collections.Generic;

namespace vcwithwebapi.Models
{
    public partial class InvoiceLineItem
    {
        public int InvoiceId { get; set; }
        public short InvoiceSequence { get; set; }
        public int AccountNo { get; set; }
        public decimal InvoiceLineItemAmount { get; set; }
        public string InvoiceLineItemDescription { get; set; } = null!;

        public virtual Glaccount AccountNoNavigation { get; set; } = null!;
        public virtual Invoice Invoice { get; set; } = null!;
    }
}
