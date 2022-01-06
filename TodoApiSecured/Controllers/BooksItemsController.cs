using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApi.Models;
using BooksApi.Attributes;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class BooksItemsController : ControllerBase
    {
        private readonly BooksContext _context;

        public BooksItemsController(BooksContext context)
        {
            _context = context;
        }



        // GET: api/BooksItems   notice: we don't use method name
        [HttpGet]
        [Produces("application/json")]
        [SwaggerOperation("Zwraca wszystkie zadania.", "Używa EF")]
        [SwaggerResponse(200, "Sukces", Type = typeof(List<BooksItem>))]        
        public async Task<ActionResult<IEnumerable<BooksItem>>> GetBooksItems()
        {
            return await _context.BooksItems.ToListAsync();  //http 200
        }



        // GET: api/BooksItems/5
        [HttpGet("{id}")]        
        [Produces("application/json")]
        [SwaggerOperation("Zwraca zadanie o podanym {id}.", "Używa EF FindAsync()")]
        [SwaggerResponse(200, "Znaleziono zadanie o podanym {id}", Type = typeof(BooksItem))]
        [SwaggerResponse(404, "Nie znaleziono zadania o podanym {id}")]
        public async Task<ActionResult<BooksItem>> GetBooksItem(
            [SwaggerParameter("Podaj nr zadnia które chcesz odczytać", Required = true)]
            int id)
        {
            var BooksItem = await _context.BooksItems.FindAsync(id);

            if (BooksItem == null)
            {
                return NotFound(); //http 404
            }

            return BooksItem;    //http 200
        }


        // PUT: api/BooksItems/5        
        [HttpPut("{id}")]
        [Produces("application/json")]
        [SwaggerOperation("Aktualizuje zadanie o podanym {id}.", "Używa EF")]
        [SwaggerResponse(204, "Zaktualizowano zadanie o podanym {id}")]
        [SwaggerResponse(400, "Nie rozpoznano danych wejściowych")]
        [SwaggerResponse(404, "Nie znaleziono zadania o podanym {id}")]
        public async Task<IActionResult> PutBooksItem(
            [SwaggerParameter("Podaj nr zadnia które chcesz zaktualizować", Required = true)]
            int id,
            [SwaggerParameter("Definicja zadania", Required = true)]
            BooksItem BooksItem)
        {
            if (id != BooksItem.Id)
            {
                return BadRequest(); //http 400
            }

            _context.Entry(BooksItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BooksItemExists(id))
                {
                    return NotFound();  //http 404
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); //http 204
        }


        // POST: api/BooksItems        
        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation("Tworzy nowe zadanie.", "Używa EF")]
        [SwaggerResponse(201, "Zapis z sukcesem.", Type = typeof(BooksItem))]
        public async Task<ActionResult<BooksItem>> PostBooksItem(
            [SwaggerParameter("Definicja zadania", Required = true)]
            BooksItem BooksItem)
        {
            _context.BooksItems.Add(BooksItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooksItem", new { id = BooksItem.Id }, BooksItem);  //http 201, add Location header
        }



        // DELETE: api/BooksItems/5
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [SwaggerOperation("Usuwa zadanie o podanym {id}.", "Używa EF")]
        [SwaggerResponse(204, "Usunięto zadanie o podanym {id}")]        
        [SwaggerResponse(404, "Nie znaleziono zadania o podanym {id}")]
        public async Task<IActionResult> DeleteBooksItem(
            [SwaggerParameter("Podaj nr zadnia które chcesz usunąć", Required = true)]
            int id)
        {
            var BooksItem = await _context.BooksItems.FindAsync(id);
            if (BooksItem == null)
            {
                return NotFound();  //http 404
            }

            _context.BooksItems.Remove(BooksItem);
            await _context.SaveChangesAsync();

            return NoContent(); //http 204
        }



        private bool BooksItemExists(int id)
        {
            return _context.BooksItems.Any(e => e.Id == id);
        }
    }
}
