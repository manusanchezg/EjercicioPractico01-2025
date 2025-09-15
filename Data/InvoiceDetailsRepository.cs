using EjercicioPractico01_2025.Domain;
using System.Data;


namespace EjercicioPractico01_2025.Data
{
    internal class InvoiceDetailRepository : IInvioceDetailsRepository
    {
        public bool Add(InvoiceDetail entity)
        {
            try
            {
                var (productId, quantity) = (entity.Product.Id, entity.Quantity);
                List<Parameter> parameters = new()
                {
                    new Parameter("@ProductId", productId),
                    new Parameter("@Quantity", quantity)
                };
                DataHelper.GetInstance().ExecuteSPQuery("AGREGAR_DETALLE", parameters);
                return true; // Retorna true si la operación fue exitosa
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar los detalles de la factura: {ex.Message}");
                return false; // Retorna false si hubo un error
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<InvoiceDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<InvoiceDetail>? GetById(int id)
        {
            List<InvoiceDetail> InvoiceDetail = new();
            try
            {
                DataTable dt = DataHelper.GetInstance().ExecuteSPQuery("OBTENER_DETALLES_FACTURA", new List<Parameter> { new Parameter("@Id", id) });
                if (dt == null || dt.Rows.Count == 0)
                    return null;
                DataRowCollection dr = dt.Rows;

                foreach (DataRow row in dr)
                {
                    var product = new ProductRepository().GetById((int)row["ProductId"]);
                    if (product == null) continue;
                    InvoiceDetail detail = new InvoiceDetail(
                        (int)row["Id"],
                        product,
                        (int)row["Quantity"]
                    );
                    InvoiceDetail.Add(detail);
                }
                return InvoiceDetail;
            }
            catch (Exception ex)
            {
                // Por ahora, simplemente retornamos null
                throw new Exception("Error al leer los detalles de la factura", ex);
            }
        }

        public bool Update(InvoiceDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
