using SakilaAPI.Core.Entities;
using SakilaAPI.Core.Models.Film;

namespace SakilaAPI.Core.Models.Actor
{
    public class ActorModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<FilmModel> Films { get; set; }
        public List<int> IdFilms { get; set; }
    }
}
