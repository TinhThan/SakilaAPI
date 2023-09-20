using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core;
using SakilaAPI.Core.Entities;
using SakilaAPI.Core.Models;

namespace SakilaAPI.Controllers
{
    [ApiController]
    [Route("api/actor")]
    public class ActorController : ControllerBase
    {

        private readonly ILogger<ActorController> _logger;
        public ActorController(ILogger<ActorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Danh sách diễn viên theo tên
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("danhsach/{name}")]
        public async Task<IActionResult> DanhSach(string name)
        {
            using (var context = new DataContext())
            {
                var actors = await context.Actors.Where(t => t.FirstName.Contains(name) || t.LastName.Contains(name)).ToListAsync();
                if (!actors.Any())
                {
                    return NotFound();
                }
                return Ok(actors);
            }
        }

        /// <summary>
        /// Lấy chi tiết diễn viên by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("chitiet/{id}")]
        public IActionResult ChiTiet(int id)
        {
            using (var context = new DataContext())
            {
                var actor = context.Actors.FirstOrDefault(a => a.Id == id);
                if (actor == null)
                {
                    return NotFound();
                }
                return Ok(actor);
            }
        }

        /// <summary>
        /// Tạo mới diễn viên
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost("taomoi")]
        public async Task<IActionResult> TaoMoi(ActorTaoMoiModel model)
        {
            try
            {
                using (var context = new DataContext())
                {
                    var actorEntity = new ActorEntity()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        LastUpdate = DateTime.Now
                    };
                    var resultTaoMoi = await context.Actors.AddAsync(actorEntity);
                    await context.SaveChangesAsync();
                    return Ok("Thêm thành công");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Xóa diễn viên
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("xoa/{id}")]
        public async Task<IActionResult> Xoa(int id)
        {
            try
            {
                using (var context = new DataContext())
                {
                    var actor = await context.Actors.FirstOrDefaultAsync(a => a.Id == id);
                    if (actor == null)
                    {
                        return NotFound();
                    }
                    var result = context.Remove(actor);
                    await context.SaveChangesAsync();
                    return Ok("Xóa thành công");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
