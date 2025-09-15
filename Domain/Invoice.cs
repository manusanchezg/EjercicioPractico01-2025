using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioPractico01_2025.Domain
{
    internal class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<InvoiceDetail> Details { get; set; }
        public decimal Total
        {
            get
            {
                return Details.Sum(d => d.Subtotal);
            }
        }
        public Invoice(int id, DateTime date, string customer, PaymentMethod paymentMethod, List<InvoiceDetail> details)
        {
            Id = id;
            Date = date;
            Customer = customer;
            PaymentMethod = paymentMethod;
            Details = details;
        }
        public Invoice()
        {
            Id = 0;
            Date = DateTime.Now;
            Customer = string.Empty;
            PaymentMethod = new PaymentMethod();
            Details = new List<InvoiceDetail>();
        }

        public void AddDetail(InvoiceDetail detail)
        {
            if (detail != null)
            {
                Details.Add(detail);
            }
        }

        public void RemoveDetail(int index)
        {
            Details.RemoveAt(index);
        }

        public void RemoveDetail(InvoiceDetail detail)
        {
            Details.Remove(detail);
        }

        public override string ToString()
        {
            return $"Invoice Id: {Id}, Date: {Date}, Customer: {Customer}, Payment Method: {PaymentMethod}, Total: {Total}";
        }
    }
}
