using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly BackendContext _context;

        public CardController(BackendContext context)
        {
            _context = context;
        }

        // GET: api/Card
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardDTO>>> GetCards()
        {
            return await _context.Cards.Select(c => CardDTO.ToDTO(c)).ToListAsync();
        }

        // GET: api/Card/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardDTO>> GetCard(long id)
        {
            var card = await _context.Cards.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return CardDTO.ToDTO(card);
        }

        // PUT: api/Card/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(long id, Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

        // POST: api/Card
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CardDTO>> PostCard(CardDTO cardDTO)
        {
            var newCard = new Card { Title = cardDTO.Title, Description = cardDTO.Description, BoardListId = cardDTO.BoardListId };
            _context.Cards.Add(newCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCard", new { id = newCard.Id }, CardDTO.ToDTO(newCard));
        }

        // DELETE: api/Card/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(long id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(long id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }

    }
}
