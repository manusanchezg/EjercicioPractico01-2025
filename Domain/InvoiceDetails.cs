using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioPractico01_2025.Domain
{
    internal class InvoiceDetails
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal
        {
            get
            {
                return Product.Price * Quantity;
            }
        }
        public InvoiceDetails(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
        public InvoiceDetails()
        {
            Product = new Product();
            Quantity = 0;
        }

        public override string ToString()
        {
            return $"Product: {Product}, Quantity: {Quantity}, Subtotal: {Product.Price * Quantity}";
        }
    }
}
