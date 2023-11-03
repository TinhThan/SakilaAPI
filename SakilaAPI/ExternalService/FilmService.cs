using SakilaAPI.Core.Models.Film;

namespace SakilaAPI.ExternalService
{
    public interface IFilmService
    {
        Task<List<FilmModel>> DanhSachFilmByIds(int[] ids);
    }

    public class FilmService : IFilmService
    {
        private readonly ICallAPI _callAPI;

        public FilmService(ICallAPI callAPI)
        {
            _callAPI = callAPI;
        }

        public async Task<List<FilmModel>> DanhSachFilmByIds(int[] ids)
        {
            var listFilmModel = new List<FilmModel>();
            string url = _callAPI.GetFullLink(ActionUrl.API_Film, ActionUrl.Film_DanhSachByIds, new string[] { _callAPI.BuildParram(ids) });
            _callAPI.SetTokenHeaders(url, "application/json");
            var resultRequest = await _callAPI.CallAPIGet(url);
            return await _callAPI.Result(resultRequest, listFilmModel);
        }
    }
}
