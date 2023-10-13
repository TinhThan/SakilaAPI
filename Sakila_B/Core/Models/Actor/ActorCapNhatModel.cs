namespace Sakila_B.Core.Models.Actor
{
    /// <summary>
    /// Cập nhật actor model
    /// </summary>
    public class ActorCapNhatModel : ActorTaoMoiModel
    {
        /// <summary>
        /// Kiểm tra version update
        /// </summary>
        public DateTime LastUpdate { get; set; }
    }
}
