using BaseProjectAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace BaseProjectAPI.Repositories
{
				public interface IAccountRepository
				{
								public Task<IdentityResult> SignUpAsyn(UserSignUpDTO userSignUpDTO);
								public Task<string> SignInAsyn(UserSignInDTO userSignInDTO);
				}
}
