using System;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using UrlEncoder = System.Text.Encodings.Web.UrlEncoder;

public class JwtAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
	public JwtAuthenticationHandler(
		IOptionsMonitor<AuthenticationSchemeOptions> authOptions,
		ILoggerFactory logger,
		UrlEncoder encoder,
		ISystemClock clock)
		: base(authOptions, logger, encoder, clock)
	{
	}

	protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
	{
		
		return AuthenticateResult.Success(null);
	}
}