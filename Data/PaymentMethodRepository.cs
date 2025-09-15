using EjercicioPractico01_2025.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioPractico01_2025.Data
{
    internal class PaymentMethodRepository : IRepository<PaymentMethod, int>
    {
        public bool Add(PaymentMethod entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<PaymentMethod> GetAll()
        {
            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery("OBTENER_METODOS_PAGOS");
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
            foreach (DataRow row in dt.Rows)
            {
                PaymentMethod paymentMethod = new PaymentMethod
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString()
                };
                paymentMethods.Add(paymentMethod);
            }
            return paymentMethods;
        }

        public PaymentMethod? GetById(int id)
        {
            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery("OBTENER_METODO_PAGO_POR_ID", new List<Parameter>
            {
                new("@Id", id)
            });

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            return new PaymentMethod
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString()
            };
        }

        public bool Update(PaymentMethod entity)
        {
            throw new NotImplementedException();
        }
    }
}
