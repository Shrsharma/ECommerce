﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using ModelLayer;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Controllers
{
    public class LaptopController : Controller
    {
        private readonly DataLayer.AppContext _context;

        public LaptopController(DataLayer.AppContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        // GET: Laptop
        public async Task<IActionResult> Index()
        {
            return View(await _context.LaptopModel.ToListAsync());
        }

        // GET: Laptop/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptopModel = await _context.LaptopModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (laptopModel == null)
            {
                return NotFound();
            }

            return View(laptopModel);
        }

        // GET: Laptop/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Laptop/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Product_Name,ImgUrl,Description,Quantity")] LaptopModel laptopModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(laptopModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(laptopModel);
        }

        // GET: Laptop/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptopModel = await _context.LaptopModel.FindAsync(id);
            if (laptopModel == null)
            {
                return NotFound();
            }
            return View(laptopModel);
        }

        // POST: Laptop/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Product_Name,ImgUrl,Description,Quantity")] LaptopModel laptopModel)
        {
            if (id != laptopModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laptopModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaptopModelExists(laptopModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(laptopModel);
        }

        // GET: Laptop/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptopModel = await _context.LaptopModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (laptopModel == null)
            {
                return NotFound();
            }

            return View(laptopModel);
        }

        // POST: Laptop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var laptopModel = await _context.LaptopModel.FindAsync(id);
            _context.LaptopModel.Remove(laptopModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaptopModelExists(string id)
        {
            return _context.LaptopModel.Any(e => e.Id == id);
        }
    }
}
