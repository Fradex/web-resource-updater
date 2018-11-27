using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebPackUpdater.Authentication;
using WebPackUpdater.Model;

namespace WebPackUpdater.Controllers
{
	[Route("api/[controller]")]
	public class AuthController : Controller
	{
		[HttpPost, Route("Token")]
		public async Task Token([FromBody] AuthRequestModel requestModel)
		{
			if (!ModelState.IsValid)
			{
				Response.StatusCode = 400;
				await Response.WriteAsync("Invalid username or password.");
			}

			var identity = GetIdentity(requestModel.UserName, requestModel.Password);
			if (identity == null)
			{
				Response.StatusCode = 400;
				await Response.WriteAsync("Invalid username or password.");
			}

			var now = DateTime.UtcNow;
			// создаем JWT-токен
			var jwt = new JwtSecurityToken(
				issuer: AuthOptions.ISSUER,
				audience: MyHttpContext.AppBaseUrl,
				notBefore: now,
				claims: identity.Claims,
				expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
				signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
					SecurityAlgorithms.HmacSha256));
			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			var response = new
			{
				access_token = encodedJwt,
				username = identity.Name
			};

			// сериализация ответа
			Response.ContentType = "application/json";
			await Response.WriteAsync(JsonConvert.SerializeObject(response,
				new JsonSerializerSettings {Formatting = Formatting.Indented}));
		}

		private ClaimsIdentity GetIdentity(string username, string password)
		{
			//Person person = people.FirstOrDefault(x => x.Login == username && x.Password == password);
			//if (person != null)
			//{
			var claims = new List<Claim>
			{
				new Claim(type: ClaimsIdentity.DefaultNameClaimType, value: username),
				new Claim(type: ClaimsIdentity.DefaultRoleClaimType, value: password)
			};
			ClaimsIdentity claimsIdentity =
				new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
					ClaimsIdentity.DefaultRoleClaimType);
			return claimsIdentity;
			//}

			// если пользователя не найдено
			return null;
		}
	}
}