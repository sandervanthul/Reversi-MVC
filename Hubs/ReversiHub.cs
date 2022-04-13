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
                new SpelViewModel { SpelToken = currentGame.Token, SpelerToken = userId },
                $"Spel/Zet/{rij}/{kolom}");
            if (newGameState.Bord == null) return;

            await Clients.All.SendAsync("UpdateBord", newGameState.Bord);
            await Clients.All.SendAsync("UpdateScore", newGameState.AantalWit, newGameState.AantalZwart);

            await Clients.Others.SendAsync("AddBoardFunctionality");

            await Clients.Caller.SendAsync("DisableForfeitButton");
            await Clients.Others.SendAsync("EnableForfeitButton");

            await Clients.Caller.SendAsync("DisablePassButton");
            await Clients.Others.SendAsync("EnablePassButton");

            await Clients.All.SendAsync("UpdateStatistieken");


            // todo fix lelijke code: maak aparte functie voor controle afgelopen game

            var afgelopen = await _service.GetSpecialAsync(newGameState.Token, "Spel/Afgelopen");
            if (afgelopen)
            {
                var kleurCaller = newGameState.Speler1Token == userId ? Kleur.Wit : Kleur.Zwart;
                if (newGameState.AantalWit > newGameState.AantalZwart)
                {
                    switch (kleurCaller)
                    {
                        case Kleur.Wit:
                            await Clients.Caller.SendAsync("Gewonnen");
                            await Clients.Others.SendAsync("Verloren");
                            break;
                        case Kleur.Zwart:
                            await Clients.Caller.SendAsync("Verloren");
                            await Clients.Others.SendAsync("Gewonnen");
                            break;
                    }
                }
                else
                {
                    switch (kleurCaller)
                    {
                        case Kleur.Wit:
                            await Clients.Caller.SendAsync("Gewonnen");
                            await Clients.Others.SendAsync("Verloren");
                            break;
                        case Kleur.Zwart:
                            await Clients.Caller.SendAsync("Verloren");
                            await Clients.Others.SendAsync("Gewonnen");
                            break;
                    }
                }
                await _service.DeleteAsync(newGameState.Token, "/api/Spel");
            }
        }

        public async Task Passen()
        {
            string userId = "";
            if (_httpContextAccessor.HttpContext != null)
            {
                userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            var currentGame = await _service.GetAsync(userId, "/api/Speler");
            if (currentGame == null) return;

            var newGameState = await _service.UpdateSpecialAsync("",
                new SpelViewModel() { SpelToken = currentGame.Token, SpelerToken = userId }, "Spel/Passen");
            if (newGameState.Bord == null) return;

            await Clients.All.SendAsync("UpdateBord", newGameState.Bord);
            await Clients.Others.SendAsync("AddBoardFunctionality");

            await Clients.Caller.SendAsync("DisableForfeitButton");
            await Clients.Others.SendAsync("EnableForfeitButton");

            await Clients.Caller.SendAsync("DisablePassButton");
            await Clients.Others.SendAsync("EnablePassButton");
        }

        public async Task Opgeven()
        {
            string userId = "";
            if (_httpContextAccessor.HttpContext != null)
            {
                userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            var response = await _service.DeleteAsync(userId, "/api/Speler");

            await Clients.Caller.SendAsync("Verloren");
            await Clients.Others.SendAsync("Gewonnen");
        }

        public async Task Statistieken()
        {
            await Clients.Caller.SendAsync("ShowStatistieken");
        }
    }
}
