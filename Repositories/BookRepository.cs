using AutoMapper;
using BaseProjectAPI.Data;
using BaseProjectAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectAPI.Repositories
{
				public class BookRepository : IBookRepository
				{
				    private readonly BookStoreDbContext _dbContext;
								private readonly IMapper _mapper;
								public BookRepository(BookStoreDbContext context, IMapper mapper) 
								{
												_dbContext = context;
												_mapper = mapper;
								}
								public async Task<int> AddBookAsync(BookDTO bookDto)
								{
												var newBook = _mapper.Map<Book>(bookDto);
												_dbContext.Books?.Add(newBook);
												await _dbContext.SaveChangesAsync();
												return newBook.Id;
								}

								public async Task DeleteBookAsync(int id)
								{
												var deleteBook = _dbContext.Books!.SingleOrDefault(book => book.Id == id);
												if(deleteBook != null) 
												{
																_dbContext.Books!.Remove(deleteBook);
																await _dbContext.SaveChangesAsync();
												}
								}

								public async Task<List<BookDTO>> GetAllBooksAsync()
								{
												var books = await _dbContext.Books!.ToListAsync();
												return _mapper.Map<List<BookDTO>>(books);
								}

								public async Task<BookDTO> GetBookAsync(int id)
								{
												var books = await _dbContext.Books!.FindAsync(id);
												return _mapper.Map<BookDTO>(books);
								}

								public async Task UpdateBookAsync(int id, BookDTO bookDto)
								{
												if(id == bookDto.ID)
												{
																var updateBook = _mapper.Map<Book>(bookDto);
																_dbContext.Books!.Update(updateBook);
																await _dbContext.SaveChangesAsync();
												}
								}
				}
}
