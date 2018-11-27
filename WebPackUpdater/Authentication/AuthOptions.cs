using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebPackUpdater.Authentication
{
	public class AuthOptions
	{
		public const string ISSUER = "MyAuthServer"; // издатель токена
		const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
		public const int LIFETIME = 100; // время жизни токена - 100 минут
		public static SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
		}
	}
}
