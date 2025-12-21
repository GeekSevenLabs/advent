namespace Advent.Kernel;
public interface IRepository<TEntity> where TEntity : IAgreggateRoot;
