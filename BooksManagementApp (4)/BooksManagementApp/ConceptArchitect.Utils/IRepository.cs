namespace ConceptArchitect.Utils
{
    public interface IRepository<Entity,Id>
    {
        Task<Entity> Add(Entity entity);
        Task<List<Entity>> GetAll();

        Task<List<Entity>> GetAll(Func<Entity, bool> predicate);

        Task<Entity> GetById(Id id);

        Task<Entity> Update(Entity entity, Action<Entity,Entity> mergeOldNew);

        Task Delete(Id id);

    }
}