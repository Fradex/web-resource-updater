using System;
using WebPackUpdater.Enums;

namespace WebPackUpdater.Model
{
	public class Build
	{
		public Guid Id { get; set; }
		public string BuildName { get; set; }
		public string BuildDescription { get; set; }
		public string BuildLog { get; set; }
		public DateTime CreatedOn { get; set; } = DateTime.Now;
		public BuildStatusType BuildStatusType { get; set; } = BuildStatusType.Processing;
	}
}