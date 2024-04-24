using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vcwithwebapi.Models;

namespace vcwithwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly APContext _context;

        public InvoiceController(APContext context)
        {
            _context = context;
        }

        [HttpGet("Due")]
        //Invoice due
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            var invoices = await _context.Invoices.Where(i => i.InvoiceTotal > (i.PaymentTotal + i.CreditTotal)).ToListAsync();
            return invoices;
        }
        //1. Write a query to retrieve last 5 those invoices record whose invoice total is equal to the sum of payment total &amp; credit total.
        [HttpGet("Top_5")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoiceTotal()
        {
            var invoices = await _context.Invoices
                                 .Where(p => p.InvoiceTotal == (p.PaymentTotal + p.CreditTotal))
                                 .OrderByDescending(p => p.InvoiceId).Take(5).ToListAsync();
            return invoices;
        }
        //2. Write a query to retrieve those invoices record whose date is later then 01/01/2016 or invoice
        //total is more than 500 and invoice total must be greater than sum of payment total and credit total.
        [HttpGet("AND_OR")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoice()
        {
            var invoices = await _context.Invoices
         .Where(i => i.InvoiceDate > new DateTime(2012, 4, 1) ||
                     (i.InvoiceTotal > 500 &&
                      i.InvoiceTotal > (i.PaymentTotal + i.CreditTotal)))
         .ToListAsync();
            return invoices;

        }
        // 3. Write a query to retrieve those invoices whose vendor states are all except ‘CA’, ‘NV’, ‘OR’ and invoice dates are later than 01/01/2016. 
        [HttpGet("Vendorstate_AND")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetVendorState()
        {
            var invoices = await _context.Invoices
                .Join(_context.Vendors,
                      invoice => invoice.VendorId,
                      vendor => vendor.VendorId,
                      (invoice, vendor) => new { Invoice = invoice, Vendor = vendor })
                .Where(joinResult => new[] { "CA", "NV", "OR" }.Contains(joinResult.Vendor.VendorState) &&
                                     joinResult.Invoice.InvoiceDate > new DateTime(2016, 1, 1))
                .Select(joinResult => joinResult.Invoice)
                .ToListAsync();


            return invoices;
        }
        //4. Write a query to retrieve invoices from 01/05/2016 to 31/05/2016. 
        [HttpGet("Date_Between")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoicesForDate()
        {
            var invoices = await _context.Invoices
                .Where(invoice => invoice.InvoiceDate >= new DateTime(2016, 1, 1) && invoice.InvoiceDate <= new DateTime(2016, 1, 31))
                .ToListAsync();


            return invoices;
        }
        //5. Write a query to retrieve vendors whose vendor city starts with ‘SAN’.
        [HttpGet("VendorState_SA")]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendorsInSanCity()
        {
            var vendors = await _context.Vendors
                .Where(vendor => vendor.VendorCity.StartsWith("SAN"))
                .ToListAsync();

            return vendors;
        }
        //6. Write a query to retrieve vendors whose contact name has one of the following characters: a, e, i, o, u.
        [HttpGet("aeiou")]
        public async Task<ActionResult<IEnumerable<Vendor>>> Getaeiou()
        {
            var vendors = await _context.Vendors
                .Where(vendor => (vendor.VendorContactFname + " " + vendor.VendorContactLname).Any(char.IsLetter) &&
                                 (vendor.VendorContactFname + " " + vendor.VendorContactLname).ToLower().Any(c => "aeiou".Contains(c)))
                .ToListAsync();


            return vendors;
        }
        //7. Write a query to find all vendors whose first letter of state starts with N and the next letter is one of A through J.
        [HttpGet("Start_N")]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendorsWithStatePattern()
        {
            var vendors = await _context.Vendors
                .Where(vendor => vendor.VendorState.StartsWith("N") && vendor.VendorState.Length > 1 && vendor.VendorState[1] >= 'A' && vendor.VendorState[1] <= 'J')
                .ToListAsync();
            return vendors;
        }
        //8

        //9
        [HttpGet("Fetch_2nd_10")]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendorsPaged()
        {
            var vendors = await _context.Vendors.OrderBy(vendor => vendor.VendorId).Skip(10).Take(10).ToListAsync();
            return vendors;

        }

    }
}
