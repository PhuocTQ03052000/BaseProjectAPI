using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseProjectAPI.Data;
using BaseProjectAPI.Models;

namespace BaseProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BooksController(BookStoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        [Route("GetAllBooks", Name = "GetAllBooks")]
        public ActionResult<IEnumerable<BookDTO>> GetAllBooks()
        {
            var BookDTOs = _context.Books?.Select(book => new Book()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                Quantity = book.Quantity
            });

            return Ok(BookDTOs);
        }

        // GET: api/BookByName
        [HttpGet]
								[Route("{title}/GetBookByTitle", Name = "GetBookByTitle")]
								public ActionResult<BookDTO> GetBookByTitle(string title)
								{
												if (title.Length <= 0)
												{
																return BadRequest();
												}
												var book = _context.Books?.Where(n => n.Title == title).FirstOrDefault();

												if (book == null)
												{
																return NotFound($"Book with title = {title} not found");
												}

												var BookDTOs = new Book()
												{
																Id = book.Id,
																Title = book.Title,
																Description = book.Description,
																Price = book.Price,
																Quantity = book.Quantity
												};

												return Ok(BookDTOs);
								}

								// GET: api/BookById
								[HttpGet]
        [Route("{id:int}/GetBookById", Name = "GetBookById")]
        public ActionResult<BookDTO> GetBookById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var book = _context.Books?.Where(n => n.Id == id).FirstOrDefault();

            if (book == null)
            {
                return NotFound($"Book with id = {id} not found");
            }

            var BookDTOs = new Book()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                Quantity = book.Quantity
            };

            return Ok(BookDTOs);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/UpdateBookById")]
        public async Task<IActionResult> UpdateBookById(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books/Create
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("CreateNewBook", Name = "CreateNewBook")]
        public ActionResult<BookDTO> CreateNewBook([FromBody] BookDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            Book book = new Book()
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                Quantity = model.Quantity
            };

            _context.Books?.Add(book);
            _context.SaveChanges();

            model.ID = book.Id;

            // trả về url/id vừa tạo
            return CreatedAtRoute("GetBookById", new { id = model.ID }, model);
        }

        // DELETE: api/Books/Delete
        [HttpDelete]
        [Route("{id}/DeleteBookById", Name = "DeleteBookById")]
        public ActionResult<bool> DeleteBookById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var book = _context.Books?.Where(n => n.Id == id).FirstOrDefault();
            if (book == null)
            {
                return NotFound($"Book with id = {id} not found");
            }

            _context.Books?.Remove(book);
            _context.SaveChanges();

            return true;
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
