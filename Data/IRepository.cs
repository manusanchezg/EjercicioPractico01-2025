namespace EjercicioPractico01_2025.Data
{
    internal interface IRepository<T, D>
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(D id);
        List<T>? GetAll();
        T? GetById(D id);
    }
}
