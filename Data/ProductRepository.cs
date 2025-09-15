using EjercicioPractico01_2025.Domain;
using System.Data;

namespace EjercicioPractico01_2025.Data
{
    internal class ProductRepository : IRepository<Product, int>
    {
        public bool Add(Product entity)
        {
            try
            {
                var (name, price) = (entity.Name, entity.Price);
                List<Parameter> parameters = new()
                    {
                        new Parameter("@Name", name),
                        new Parameter("@Price", price)
                    };
                DataHelper.GetInstance().ExecuteSPQuery("MODIFICAR_ARTICULO", parameters);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el producto: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            var result = DataHelper.GetInstance().ExecuteSPQuery("ELIMINAR_ARTICULO", new List<Parameter> { new Parameter("@Id", id) });
            return result.Rows.Count > 0;
        }

        public List<Product> GetAll()
        {
            List<Product> products = new();
            try
            {
                DataTable dt = DataHelper.GetInstance().ExecuteSPQuery("OBTENER_ARTICULOS");

                if (dt == null)
                    return products;

                foreach (DataRow dr in dt.Rows)
                {
                    products.Add(new Product(
                        (int)dr["Id"],
                        (string)dr["Name"],
                        (decimal)dr["Price"]
                    ));
                }
            }
            catch (Exception ex)
            {
                // Por ahora, simplemente retornamos la lista vacía
                throw new Exception("Error al leer los productos", ex);
            }
            return products;
        }

        public Product? GetById(int id)
        {
            try
            {
                DataTable dt = DataHelper.GetInstance().ExecuteSPQuery("OBTENER_ARTICULO_X_ID", new List<Parameter> { new Parameter("@Id", id) });

                if (dt == null || dt.Rows.Count == 0)
                    return null;
                DataRow dr = dt.Rows[0];
                return new Product(
                    (int)dr["Id"],
                    (string)dr["Name"],
                    (decimal)dr["Price"]
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer el producto", ex);
            }

        }

        public bool Update(Product entity)
        {
            try
            {
                var (name, price) = (entity.Name, entity.Price);
                List<Parameter> parameters = new()
                    {
                        new Parameter("@Name", name),
                        new Parameter("@Price", price)
                    };
                DataHelper.GetInstance().ExecuteSPQuery("MODIFICAR_ARTICULO", parameters);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el producto: {ex.Message}");
                return false;
            }
        }
    }
}
