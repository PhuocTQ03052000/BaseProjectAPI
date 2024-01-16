using AutoMapper;
using BaseProjectAPI.Data;
using BaseProjectAPI.Models;

namespace BaseProjectAPI.Helper
{
				public class ApplicationMapper : Profile
				{
								public ApplicationMapper() 
								{
												//Mapper 2 chiều
												CreateMap<Book, BookDTO>().ReverseMap();
								}
				}
}
