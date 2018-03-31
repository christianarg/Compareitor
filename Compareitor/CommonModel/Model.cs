using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compareitor.CommonModel
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }
    }

    public class InvoiceLine
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
        [MaxLength(150)]
        public string ProductName { get; set; }
    }
}
