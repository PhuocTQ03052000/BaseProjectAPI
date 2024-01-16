using BaseProjectAPI.Models;
using BaseProjectAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseProjectAPI.Controllers
{
				[Route("api/[controller]")]
				[ApiController]
				public class AccountController : ControllerBase
				{
								private readonly IAccountRepository accountRepo;

								public AccountController(IAccountRepository accountRepository)
								{
												accountRepo = accountRepository;
								}

								[HttpPost("SignUp")]
								public async Task<IActionResult> SignUp(UserSignUpDTO userSignUpDTO)
								{
												var result = await accountRepo.SignUpAsyn(userSignUpDTO);

												if(result.Succeeded) 
												{
																return Ok(result.Succeeded);
												}

												return StatusCode(500);
								}

								[HttpPost("SignIn")]
								public async Task<IActionResult> SignIn(UserSignInDTO userSignInDTO)
								{
												var result = await accountRepo.SignInAsyn(userSignInDTO);

												if(string.IsNullOrEmpty(result))
												{
																return Unauthorized();
												}

												return Ok(result);
								}
				}
}
