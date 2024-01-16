using BaseProjectAPI.Data;
using BaseProjectAPI.Helper;
using BaseProjectAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BaseProjectAPI.Repositories
{
				public class AccountRepository : IAccountRepository
				{
								private readonly UserManager<ApplicationUser> userManager;
								private readonly SignInManager<ApplicationUser> signInManager;
								private readonly IConfiguration configuration;
								private readonly RoleManager<IdentityRole> roleManager;

								public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
								{
												this.userManager = userManager;
												this.signInManager = signInManager;
												this.configuration = configuration;
												this.roleManager = roleManager;
								}

								public async Task<string> SignInAsyn(UserSignInDTO userSignInDTO)
								{
												var user = await userManager.FindByEmailAsync(userSignInDTO.Email);
												var passwordValid = await userManager.CheckPasswordAsync(user, userSignInDTO.Password);

												if(user == null || !passwordValid) 
												{
																return string.Empty;
												}

												var authClaims = new List<Claim>
												{
																new Claim(ClaimTypes.Email, userSignInDTO.Email),
																new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
												};

												var userRoles = await userManager.GetRolesAsync(user);
												foreach(var role in userRoles)
												{
																authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
												}

												var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

												var token = new JwtSecurityToken(
																issuer: configuration["JWT:ValidIssuer"],
																audience: configuration["JWT:ValidAudience"],
																expires: DateTime.Now.AddMinutes(20),
																claims: authClaims,
																signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256Signature)
												);

												return new JwtSecurityTokenHandler().WriteToken(token);
								}

								public async Task<IdentityResult> SignUpAsyn(UserSignUpDTO userSignUpDTO)
								{
												var user = new ApplicationUser
												{
																FirstName = userSignUpDTO.FirstName,
																LastName = userSignUpDTO.LastName,
																Email = userSignUpDTO.Email,
																UserName = userSignUpDTO.Email
												};

												var result = await userManager.CreateAsync(user, userSignUpDTO.Password);

												if(result.Succeeded)
												{
																//Kiểm tra role Customer đã có hay chưa
																if(!await roleManager.RoleExistsAsync(ApplicationRole._CUSTOMER))
																{
																				//register new role
																				await roleManager.CreateAsync(new IdentityRole(ApplicationRole._CUSTOMER));
																}

																await userManager.AddToRoleAsync(user, ApplicationRole._CUSTOMER);
												}

												return result;
								}
				}
}
