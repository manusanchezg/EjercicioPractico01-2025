using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioPractico01_2025.Domain
{
    internal class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PaymentMethod(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public PaymentMethod()
        {
            Id = 0;
            Name = string.Empty;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
