using EjercicioPractico01_2025.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioPractico01_2025.Data
{
    internal class InvoiceRepository : IRepository<Invoice, int>
    {
        public bool Add(Invoice entity)
        {
            try { 
                List<Parameter> parameters = new()
                {
                    new Parameter("@Date", entity.Date),
                    new Parameter("@Customer", entity.Customer),
                    new Parameter("@PaymentMethodId", entity.PaymentMethod.Id),
                    // Agregar más parámetros según sea necesario
                };
                DataHelper.GetInstance().ExecuteSPQuery("MODIFICAR_FACTURAS", parameters);
                return true; // Retorna true si la operación fue exitosa
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar la factura: {ex.Message}");
                return false; // Retorna false si hubo un error
            }
        }

        public bool Delete(int id)
        {
            // Aquí iría la lógica para eliminar una factura por su ID
            List<Parameter> parameters = new()
            {
                new Parameter("@Id", id)
            };

            return false;
        }

        public List<Invoice> GetAll()
        {
            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery("OBTENER_FACTURAS");
            List<Invoice> invoices = new List<Invoice>();

            foreach (DataRow row in dt.Rows)
            {
                Invoice invoice = new Invoice();
                invoice.Id = Convert.ToInt32(row["Id"]);
                invoice.Date = Convert.ToDateTime(row["Date"]);
                invoice.Customer = row["Customer"].ToString();
                invoice.PaymentMethod = new PaymentMethod
                {
                    Id = Convert.ToInt32(row["PaymentMethodId"]),
                    Name = row["PaymentMethodName"].ToString()
                };
                invoices.Add(invoice);
            }
            return invoices;
        }

        public Invoice? GetById(int id)
        {
            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery("OBTENER_FACTURA_X_ID", new List<Parameter>
            {
                new Parameter("@Id", id)
            });
            if (dt.Rows.Count == 0)
                return null;
            DataRow row = dt.Rows[0];
            Invoice invoice = new Invoice();
            invoice.Id = Convert.ToInt32(row["Id"]);
            invoice.Date = Convert.ToDateTime(row["Date"]);
            invoice.Customer = row["Customer"].ToString();
            // Probablemente referencia nula, chequear SP o hacer otro llamado
            invoice.PaymentMethod = new PaymentMethod {
                Id = Convert.ToInt32(row["PaymentMethodId"]),
                Name = row["PaymentMethodName"].ToString()
            };
            return invoice;
        }

        public bool Update(Invoice entity)
        {
            try
            {
                List<Parameter> parameters = new()
                {
                    new Parameter("@Date", entity.Date),
                    new Parameter("@Customer", entity.Customer),
                    new Parameter("@PaymentMethodId", entity.PaymentMethod.Id),
                    // Agregar más parámetros según sea necesario
                };
                DataHelper.GetInstance().ExecuteSPQuery("MODIFICAR_FACTURAS", parameters);
                return true; // Retorna true si la operación fue exitosa
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar la factura: {ex.Message}");
                return false; // Retorna false si hubo un error
            }
        }
    }
}
