using BaseProjectAPI.Helper;
using BaseProjectAPI.Models;
using BaseProjectAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseProjectAPI.Controllers
{
				[Route("api/[controller]")]
				[ApiController]
				public class ProductsController : ControllerBase
				{
								private IBookRepository _bookRepo;

								public ProductsController(IBookRepository iBookRepo) 
								{
												_bookRepo = iBookRepo;
								}
								[HttpGet("/GetAllBooks")]
								[Authorize(Roles = ApplicationRole._CUSTOMER)]
								public async Task<IActionResult> GetAllBooks()
								{
												try
												{
																return Ok(await _bookRepo.GetAllBooksAsync());
												}
												catch (Exception ex) {
																return BadRequest(ex.Message);
												}
								}
								[HttpGet("{id}/GetBookById")]
								[Authorize(Roles = ApplicationRole._ADMIN)]
								public async Task<IActionResult> GetBookById(int id)
								{
												try
												{
																var book = await _bookRepo.GetBookAsync(id);
																return book == null ? NotFound() : Ok(book); // nếu book null => not found
												}
												catch(Exception ex)
												{
																return BadRequest(ex.Message);
												}
								}
								[HttpPost("/AddNewBook")]
								[Authorize(Roles = ApplicationRole._ADMIN)]
								public async Task<IActionResult> AddNewBook(BookDTO bookDTO)
								{
												try
												{
																var newBookId = await _bookRepo.AddBookAsync(bookDTO);
																var book = await _bookRepo.GetBookAsync(newBookId);
																return book == null ? NotFound() : Ok(book); // nếu book null => not found
												}
												catch(Exception ex)
												{
																return BadRequest(ex.Message);
												}
								}

								[HttpPut("{id}/UpdateBook")]
								[Authorize(Roles = ApplicationRole._ADMIN)]
								public async Task<IActionResult> UpdateBook(int id, [FromBody]BookDTO bookDTO)
								{
												try
												{
																if(id != bookDTO.ID) 
																{
																				return NotFound();
																}
																await _bookRepo.UpdateBookAsync(id, bookDTO);
																return Ok();
												}
												catch(Exception ex)
												{
																return BadRequest(ex.Message);
												}
								}

								[HttpDelete("{id}/DeleteBook")]
								[Authorize(Roles = ApplicationRole._ADMIN)]
								public async Task<IActionResult> DeleteBook([FromRoute] int id)
								{
												try
												{
																await _bookRepo.DeleteBookAsync(id);
																return Ok();
												
												}
												catch(Exception ex) 
												{
																return BadRequest(ex.Message);
												}
								}
				}
}
