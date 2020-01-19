using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Hermes.Data;
using Hermes.Models;
using Hermes.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Hermes.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly IDataRepository<Comment> _repo;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public CommentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, 
            IDataRepository<Comment> repo, 
            IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _context = context;
            _userManager = userManager;
            _repo = repo;
            _hubContext = hubContext;
        }

        [HttpGet]
        public IEnumerable<Comment> GetComments()
        {

            var result = _context.Comments.Include(c => c.Likes).OrderByDescending(r => r.CreatedDate).ToList();
            
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            comment.User = _context.Users.FirstOrDefault(u => u.Id.Equals(comment.UserId));
            comment.UserName = _context.Users.FirstOrDefault(u => u.Id.Equals(comment.UserId))?.UserName;
            _repo.Add(comment);
            await _repo.SaveAsync(comment);
            await _hubContext.Clients.All.BroadcastMessage();

            return Ok();
        }

        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentPost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _repo.Delete(comment);
            var save = await _repo.SaveAsync(comment);

            return Ok(comment);
        }

        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment([FromRoute] int id, [FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                _repo.Update(comment);
                var save = await _repo.SaveAsync(comment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            return Ok("Hello");
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
