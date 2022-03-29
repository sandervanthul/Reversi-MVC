using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using ReversiMvcApp.Models;
using ReversiMvcApp.Services;

namespace ReversiMvcApp.Hubs
{
    public class ReversiHub : Hub
    {
        private readonly IService<Spel> _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReversiHub(IService<Spel> service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task DoeZet(string rij, string kolom)
        {
            string userId = "";
            if (_httpContextAccessor.HttpContext != null)
            {
                userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            var currentGame = await _service.GetAsync(userId, "/api/Speler");
            if (currentGame == null) return;

            var newGameState = await _service.UpdateSpecialAsync("",
                new SpelViewModel() { SpelToken = currentGame.Token, SpelerToken = userId },
                $"Spel/Zet/{rij}/{kolom}");
            if (newGameState.Bord == null) return;

            await Clients.All.SendAsync("UpdateBord", newGameState.Bord);
            await Clients.Others.SendAsync("AddBoardFunctionality");
        }
    }
}
