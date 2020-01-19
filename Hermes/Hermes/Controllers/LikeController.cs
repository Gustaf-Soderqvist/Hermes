using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hermes.Data;
using Hermes.Models;
using Hermes.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hermes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IDataRepository<Like> _repo;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public LikeController(
            ApplicationDbContext context,
            IDataRepository<Like> repo,
            IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _context = context;
            _repo = repo;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Like like)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!UserHasLikedComment(like.UserId, like.CommentId))
            {
                _repo.Add(like);
                await _repo.SaveAsync(like);
            } else
            {
                var l = await _context.Likes.FindAsync(like.Id);
                if (l == null)
                {
                    return NotFound();
                }
                _repo.Delete(l);
                await _repo.SaveAsync(l);
            }

            await _hubContext.Clients.All.BroadcastLike();

            return Ok();
        }


        private bool UserHasLikedComment(string userId, int commentId)
        {
            return _context.Likes.Any(l => l.CommentId == commentId && l.UserId == userId);
        }
    }
}