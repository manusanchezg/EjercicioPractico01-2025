using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioPractico01_2025.Domain
{
    internal class Parameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Parameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
