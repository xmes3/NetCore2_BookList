﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookList.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Books.ToList());
        }

        //GET : Book/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Add(book);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        //Details : Books/Details/3
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        //Edit : Books/Edit/3
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        //POST : Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(book);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Edit));
            }

            return View(book);
        }

        //Delete : Books/Delete/3
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        //POST : Book/Delete/6
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveBook(int id)
        {
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }
    }
}