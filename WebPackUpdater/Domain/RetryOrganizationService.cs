using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using NLog;
using Polly;

namespace WebPackUpdater.Domain
{
	/// <summary>
	/// Орг сервис с повторным вызовом в случае ошибки
	/// </summary>
	public class RetryOrganizationService : IOrganizationService
	{
		/// <summary>
		/// Кол-во повторов
		/// </summary>
		protected int RetryCount = 3;

		/// <summary>
		/// Время задержки при выполнении повторов 
		/// </summary>
		protected int ExpValueTime = 2;

		/// <summary>
		/// Сервис организаций
		/// </summary>
		public CrmServiceClient OrgService { get; protected set; }

		/// <summary>
		/// Logger
		/// </summary>
		protected ILogger Logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// .ctor
		/// </summary>
		/// <param name="orgService">Сервис организации</param>
		public RetryOrganizationService(CrmServiceClient orgService)
		{
			OrgService = orgService;
		}

		private T UseRetry<T>(Func<T> action)
		{
			var result = Policy
				.Handle<ObjectDisposedException>()
				.WaitAndRetry(RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(ExpValueTime, retryAttempt)),
					(exception, timeSpan) => { Logger.Error(exception); })
				.Execute(action);

			return result;
		}

		private void UseRetry(Action action)
		{
			Policy
				.Handle<ObjectDisposedException>()
				.WaitAndRetry(RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(ExpValueTime, retryAttempt)),
					(exception, timeSpan) => { Logger.Error(exception); })
				.Execute(action);
		}

		/// <inheritdoc />
		public Guid Create(Entity entity)
		{
			return UseRetry(() => OrgService.Create(entity));
		}

		/// <inheritdoc />
		public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
		{
			return UseRetry(() => OrgService.Retrieve(entityName, id, columnSet));
		}

		/// <inheritdoc />
		public void Update(Entity entity)
		{
			UseRetry(() => OrgService.Update(entity));
		}

		/// <inheritdoc />
		public void Delete(string entityName, Guid id)
		{
			UseRetry(() => OrgService.Delete(entityName, id));
		}

		/// <inheritdoc />
		public OrganizationResponse Execute(OrganizationRequest request)
		{
			return UseRetry(() => OrgService.Execute(request));
		}

		/// <inheritdoc />
		public void Associate(string entityName, Guid entityId, Relationship relationship,
			EntityReferenceCollection relatedEntities)
		{
			UseRetry(() => OrgService.Associate(entityName, entityId, relationship, relatedEntities));
		}

		/// <inheritdoc />
		public void Disassociate(string entityName, Guid entityId, Relationship relationship,
			EntityReferenceCollection relatedEntities)
		{
			UseRetry(() => OrgService.Disassociate(entityName, entityId, relationship, relatedEntities));
		}

		/// <inheritdoc />
		public EntityCollection RetrieveMultiple(QueryBase query)
		{
			return UseRetry(() => OrgService.RetrieveMultiple(query));
		}
	}
}