using BaseProjectAPI.Data;
using BaseProjectAPI.Models;

namespace BaseProjectAPI.Repositories
{
				public interface IBookRepository
				{
								public Task<List<BookDTO>> GetAllBooksAsync();
								public Task<BookDTO> GetBookAsync(int id);
								public Task<int> AddBookAsync(BookDTO bookDto);
								public Task UpdateBookAsync(int id, BookDTO bookDto);
								public Task DeleteBookAsync(int id);
				}
}
