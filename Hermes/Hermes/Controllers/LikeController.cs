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
        private UserManager<ApplicationUser> _userManager;
        private readonly IDataRepository<Like> _repo;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public LikeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            IDataRepository<Like> repo,
            IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _context = context;
            _userManager = userManager;
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

            _repo.Add(like);
            await _repo.SaveAsync(like);
            await _hubContext.Clients.All.BroadcastLike();

            return Ok();
        }

    }
}