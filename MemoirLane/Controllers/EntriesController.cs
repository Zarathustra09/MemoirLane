using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemoirLane.DataConnection;
using MemoirLane.Models;

namespace MemoirLane.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {
        private readonly DbContextClass _context;

        public EntriesController(DbContextClass context)
        {
            _context = context;
        }

        // GET: api/entries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntryDto>>> GetEntries()
        {
            var entries = await _context.Entries.ToListAsync();
            var entryDtos = entries.Select(e => new EntryDto
            {
                Id = e.Id,
                User_Id = e.User_Id,
                Title = e.Title,
                Content = e.Content,
                Created_At = e.Created_At,
                Updated_At = e.Updated_At
            }).ToList();

            return entryDtos;
        }

        // GET: api/entries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EntryDto>> GetEntry(int id)
        {
            var entry = await _context.Entries.FindAsync(id);

            if (entry == null)
            {
                return NotFound();
            }

            var entryDto = new EntryDto
            {
                Id = entry.Id,
                User_Id = entry.User_Id,
                Title = entry.Title,
                Content = entry.Content,
                Created_At = entry.Created_At,
                Updated_At = entry.Updated_At
            };

            return entryDto;
        }

        // PUT: api/entries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntry(int id, EntryDto entryDto)
        {
            if (id != entryDto.Id)
            {
                return BadRequest();
            }

            var entry = await _context.Entries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            entry.User_Id = entryDto.User_Id;
            entry.Title = entryDto.Title;
            entry.Content = entryDto.Content;
            entry.Updated_At = DateTime.UtcNow;

            _context.Entry(entry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id))
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

        // POST: api/entries
        [HttpPost]
        public async Task<ActionResult<EntryDto>> PostEntry(EntryDto entryDto)
        {
            var entry = new Entry
            {
                User_Id = entryDto.User_Id,
                Title = entryDto.Title,
                Content = entryDto.Content,
                Created_At = DateTime.UtcNow,
                Updated_At = DateTime.UtcNow
            };

            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();

            entryDto.Id = entry.Id;
            entryDto.Created_At = entry.Created_At;
            entryDto.Updated_At = entry.Updated_At;

            return CreatedAtAction("GetEntry", new { id = entryDto.Id }, entryDto);
        }

        // DELETE: api/entries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            var entry = await _context.Entries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            _context.Entries.Remove(entry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntryExists(int id)
        {
            return _context.Entries.Any(e => e.Id == id);
        }
    }
}
