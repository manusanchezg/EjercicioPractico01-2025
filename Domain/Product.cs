using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioPractico01_2025.Domain
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public bool Active { get; set; } = true;
        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
        public Product()
        {
            Id = 0;
            Name = string.Empty;
            Price = 0;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
