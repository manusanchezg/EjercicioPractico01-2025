using EjercicioPractico01_2025.Domain;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public Invoice GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Invoice entity)
        {
            throw new NotImplementedException();
        }
    }
}
