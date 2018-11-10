using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;

namespace WebPackUpdater.Domain
{
	/// <summary>
	/// Прокси подключения к CRM.
	/// </summary>
	internal class OrganizationServiceProxy : IOrganizationService
	{
		/// <summary>
		/// Сервис подключения к CRM.
		/// </summary>
		protected RetryOrganizationService _сrmServiceClient;

		public CrmServiceClient CrmServiceClient
		{
			get
			{
				lock (_lockObject)
				{
					if (_сrmServiceClient == null)
					{
						RecreateConnection();
					}

					return _сrmServiceClient?.OrgService;
				}
			}
		}

		/// <summary>
		/// Объект для лока.
		/// </summary>
		private object _lockObject = new object();

		/// <summary>
		/// Название подключения.
		/// </summary>
		private string _connectionStringName;

		/// <summary>
		/// Дата последнего обновления.
		/// </summary>
		private DateTime _lastUpdatedOn = DateTime.MinValue;

		/// <summary>
		/// Таймаут.
		/// </summary>
		private int _timeout = 1000 * 60;

		/// <summary>
		/// Прокси подключения к CRM.
		/// </summary>
		public OrganizationServiceProxy()
			: this("CrmConnection")
		{
		}

		/// <summary>
		/// Прокси подключения к CRM.
		/// </summary>
		/// <param name="connectionStringName">Название подключения.</param>
		public OrganizationServiceProxy(string connectionStringName)
		{
			_connectionStringName = connectionStringName;
		}

		/// <summary>
		/// Пересоздать подключение через 1 минуту после последнего обновления.
		/// </summary>
		private void RecreateConnection()
		{
			if (_lastUpdatedOn.AddMilliseconds(_timeout) < DateTime.Now)
			{
				_сrmServiceClient = new RetryOrganizationService(new CrmServiceClient(_connectionStringName));
				_lastUpdatedOn = DateTime.Now;
			}
		}

		#region методы IOrganizationService
		public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
		{
			lock (_lockObject)
			{
				RecreateConnection();
				_сrmServiceClient.Associate(entityName, entityId, relationship, relatedEntities);
			}
		}

		public Guid Create(Entity entity)
		{
			lock (_lockObject)
			{
				RecreateConnection();
				return _сrmServiceClient.Create(entity);
			}
		}

		public void Delete(string entityName, Guid id)
		{
			lock (_lockObject)
			{
				RecreateConnection();
				_сrmServiceClient.Delete(entityName, id);
			}
		}

		public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
		{
			lock (_lockObject)
			{
				RecreateConnection();
				_сrmServiceClient.Disassociate(entityName, entityId, relationship, relatedEntities);
			}
		}

		public OrganizationResponse Execute(OrganizationRequest request)
		{
			lock (_lockObject)
			{
				RecreateConnection();
				return _сrmServiceClient.Execute(request);
			}
		}

		public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
		{
			lock (_lockObject)
			{
				RecreateConnection();
				return _сrmServiceClient.Retrieve(entityName, id, columnSet);
			}
		}

		public EntityCollection RetrieveMultiple(QueryBase query)
		{
			lock (_lockObject)
			{
				RecreateConnection();
				return _сrmServiceClient.RetrieveMultiple((QueryBase) query);
			}
		}

		public void Update(Entity entity)
		{
			lock (_lockObject)
			{
				RecreateConnection();
				_сrmServiceClient.Update(entity);
			}
		}
		#endregion
	}
}
