﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReversiMvcApp.Data;
using ReversiMvcApp.Models;
using ReversiMvcApp.Services;

namespace ReversiMvcApp.Controllers
{
    //[Authorize (Policy = "SpelerNietInSpelPolicy")]
    public class SpellenController : Controller
    {
        private readonly IService<Spel> _service;
        private readonly UserManager<IdentityUser> _userManager;

        public SpellenController(IService<Spel> service, UserManager<IdentityUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        // GET: Spellen
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAsync(""));
        }

        public async Task<IActionResult> Join(Spel spel)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var request = new SpelViewModel() { SpelerToken = currentUserID, SpelToken = spel.Token };

            var response = await _service.JoinAsync(request, "/api/Speler/");

            if (response != "") return RedirectToAction(nameof(Play), new { id = response });
            return RedirectToAction(nameof(Index));
        }

        // GET: Spellen/Play/5
        public async Task<IActionResult> Play(string id)
        {
            var spel = await _service.GetAsync(id, "/api/Spel");

            if (spel == null)
            {
                return NotFound();
            }

            return View(spel);
        }

        // GET: Spellen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Spellen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Omschrijving")] Spel spel)
        {
            if (ModelState.IsValid)
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var spelModel = new SpelViewModel() { SpelerToken = currentUserID, Omschrijving = spel.Omschrijving };

                //IdentityUser user = await _userManager.FindByIdAsync(currentUserID);
                //var claimNew = new Claim(ClaimTypes.Role, "SpelerInSpel");
                //var claimOld = new Claim(ClaimTypes.Role, "SpelerNietInSpel");
                //var result = await _userManager.ReplaceClaimAsync(user, claimOld, claimNew);
                //if(result.Succeeded) Debug.WriteLine("SpelerInSpel claim added!!!");

                var response = await _service.AddAsync(spelModel, "");

                if (response != "") return RedirectToAction(nameof(Play), new { id = response });
            }
            return View();
        }

        public async Task<IActionResult> Forfeit()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _service.DeleteAsync(currentUserID, "/api/Speler");

            if (response) return RedirectToAction(nameof(Index));

            return RedirectToAction(nameof(Play));
        }

        // GET: Spellen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var spel = await _service.Spellen.FindAsync(id);
            //if (spel == null)
            //{
            //    return NotFound();
            //}
            return View(/*spel*/);
        }

        // POST: Spellen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Omschrijving,Token,Speler1Token,Speler2Token,AandeBeurt")] Spel spel)
        {
            //if (id != spel.ID)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _service.Update(spel);
            //        await _service.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!SpelExists(spel.ID))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            return View(/*spel*/);
        }

        // GET: Spellen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var spel = await _service.Spellen
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (spel == null)
            //{
            //    return NotFound();
            //}

            return View(/*spel*/);
        }

        // POST: Spellen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var spel = await _service.Spellen.FindAsync(id);
            //_service.Spellen.Remove(spel);
            //await _service.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool SpelExists(int id)
        //{
        //    return _service.Spellen.Any(e => e.ID == id);
        //}
    }
}
