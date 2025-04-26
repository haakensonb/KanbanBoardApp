using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.DTO;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardListController : ControllerBase
    {
        private readonly BackendContext _context;

        public BoardListController(BackendContext context)
        {
            _context = context;
        }

        // GET: api/BoardList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoardListDTO>>> GetBoardLists()
        {
            return await _context.BoardLists.Include("Cards").Select(bl => BoardListDTO.ToDTO(bl)).ToListAsync();
        }

        // GET: api/BoardList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoardListDTO>> GetBoardList(long id)
        {
            var boardList = await _context.BoardLists.FindAsync(id);

            if (boardList == null)
            {
                return NotFound();
            }

            return BoardListDTO.ToDTO(boardList);
        }

        // PUT: api/BoardList/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoardList(long id, BoardList boardList)
        {
            if (id != boardList.Id)
            {
                return BadRequest();
            }

            _context.Entry(boardList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardListExists(id))
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

        // POST: api/BoardList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BoardListDTO>> PostBoardList(BoardListDTO boardListDTO)
        {
            var newBoardList = new BoardList
            {
                Title = boardListDTO.Title,
                BoardId = boardListDTO.BoardId
            };
            _context.BoardLists.Add(newBoardList);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBoardList), new { id = newBoardList.Id }, BoardListDTO.ToDTO(newBoardList));
        }

        // DELETE: api/BoardList/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoardList(long id)
        {
            var boardList = await _context.BoardLists.FindAsync(id);
            if (boardList == null)
            {
                return NotFound();
            }

            _context.BoardLists.Remove(boardList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoardListExists(long id)
        {
            return _context.BoardLists.Any(e => e.Id == id);
        }

    }
}
