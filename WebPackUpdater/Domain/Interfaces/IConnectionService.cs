using System;
using Microsoft.Xrm.Sdk;
using WebPackUpdater.Enums;

namespace WebPackUpdater.Domain.Interfaces
{
	/// <summary>
	/// Интерфейс фабрики подключения
	/// </summary>
	public interface IConnectionService
    {
		/// <summary>
		/// Фабричный метод создания подключения.
		/// </summary>
		/// <param name="connectionType">Тип коннекшна</param>
		/// <param name="userId">ИД пользователя</param>
		/// <returns>Соединение с SDK CRM</returns>
	    IOrganizationService GetService(ConnectionType connectionType, Guid? userId = null);
    }
}
