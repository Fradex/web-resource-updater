using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using WebPackUpdater.Domain.Interfaces;
using WebPackUpdater.Enums;

namespace WebPackUpdater.Domain
{
	/// <summary>
	/// Сервис для работы с подключением к CRM
	/// </summary>
	public class ConnectionService : IConnectionService
	{
		private IConfiguration Configuration;
		private ILogger<ConnectionService> Logger;

		public ConnectionService(IConfiguration configuration, ILogger<ConnectionService> logger)
		{
			Configuration = configuration;
			Logger = logger;
		}

		/// <summary>
		/// Получить коннекшн к CRM
		/// </summary>
		/// <param name="connectionType">Тип коннекшна</param>
		/// <param name="userId">ИД пользователя</param>
		/// <returns>Коннекшн</returns>
		public IOrganizationService GetService(ConnectionType connectionType, Guid? userId = null)
		{
			Logger.LogInformation($"Получаем конфигурацию.");

			if (Configuration == null)
			{
				throw new ArgumentException("Не найдены конфигурации.");
			}

			var connectionString = Configuration.GetConnectionString("CrmConnection");

			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentException("Строка соединения пустая.");
			}

			Logger.LogInformation($"Строка соединения - {connectionString}");

			switch (connectionType)
			{
				case ConnectionType.Administrator:
					return new CrmServiceClient(connectionString);
				case ConnectionType.User:
				{
					if (userId == null)
					{
						throw new ArgumentException("Не найден идентификатор пользователя.");
					}

					var connection =
						new OrganizationServiceProxy(connectionString).CrmServiceClient?.OrganizationServiceProxy;

					if (connection == null)
					{
						throw new Exception("Произошла ошибка, не был создан коннекшн.");
					}

					//подменяем пользователя 
					connection.CallerId = userId.Value;

					return connection;
				}
				default:
					throw new ArgumentException("Неизвестный тип соединения.");
			}
		}
	}
}