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
    public class BoardController : ControllerBase
    {
        private readonly BackendContext _context;

        public BoardController(BackendContext context)
        {
            _context = context;
        }

        // GET: api/Board
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoardDTO>>> GetBoards()
        {
            return await _context.Boards
                .Include(board => board.BoardLists)
                .ThenInclude(boardList => boardList.Cards)
                .Select(x => BoardDTO.ToDTO(x)).ToListAsync();
        }

        // GET: api/Board/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoardDTO>> GetBoard(long id)
        {
            var board = await _context.Boards.FindAsync(id);

            if (board == null)
            {
                return NotFound();
            }

            return BoardDTO.ToDTO(board);
        }

        // PUT: api/Board/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoard(long id, BoardDTO boardDTO)
        {
            if (id != boardDTO.Id)
            {
                return BadRequest();
            }

            var existingBoard = await _context.Boards.FindAsync(id);
            if (existingBoard == null)
            {
                return NotFound();
            }

            existingBoard.Name = boardDTO.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BoardExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Board
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BoardDTO>> PostBoard(BoardDTO boardDTO)
        {
            var newBoard = new Board
            {
                Name = boardDTO.Name,
                Id = boardDTO.Id
            };
            _context.Boards.Add(newBoard);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBoard), new { id = boardDTO.Id }, BoardDTO.ToDTO(newBoard));
        }

        // DELETE: api/Board/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(long id)
        {
            var board = await _context.Boards.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }

            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoardExists(long id)
        {
            return _context.Boards.Any(e => e.Id == id);
        }

    }
}
