namespace PruebaLibros.Aplicacion.Core.Interfaces;

//Repositorio generico para acceso a base de datos
public interface IRepositorioGenerico<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();

    Task<T> GetById_ConEspecificacionAsync(IEspecificacion<T> espec);
    Task<IReadOnlyList<T>> GetAll_ConEspecificacionAsync(IEspecificacion<T> espec);

    Task<int> ConteoAsync(IEspecificacion<T> espec);
    Task<int> AgregarAsync(T entity);
    Task<int> ActualizarAsync(T entity);
    Task<int> EliminarAsync(T entity);

    //Estrategia UNITOFWORK
    void AddEntity(T Entity);
    void UpdateEntity(T Entity);
    void DeleteEntity(T Entity);
}