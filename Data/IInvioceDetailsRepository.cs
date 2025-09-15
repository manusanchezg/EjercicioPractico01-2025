using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EjercicioPractico01_2025.Domain;

namespace EjercicioPractico01_2025.Data
{
    internal interface IInvioceDetailsRepository
    {
        List<InvoiceDetail>? GetById(int id);
        bool Add(InvoiceDetail entity);
        bool Update(InvoiceDetail entity);
        bool Delete(int id);
        List<InvoiceDetail>? GetAll();
    }
}
