namespace Sakila_B.Core.Entities
{
    public class ActorEntity
    {
        public ActorEntity()
        {
            FilmActors = new HashSet<ActorFilmEntity>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual ICollection<ActorFilmEntity> FilmActors { get; set; }
    }
}
