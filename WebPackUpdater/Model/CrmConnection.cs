using System;

namespace WebPackUpdater.Model
{
	public class CrmConnection
	{
		public Guid Id { get; set; }

		public User User { get; set; }

		public string ConnectionString { get; set; }

		public string OrganizationName { get; set; }

		public string ScriptsPath { get; set; }
	}
}