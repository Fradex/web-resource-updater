using System.Collections.Generic;

namespace WebPackUpdater.Enums
{
	public class BuildResult
	{
		public bool IsSuccess { get; set; }
		public List<string> SuccessList { get; set; }
		public List<string> ErrorList { get; set; }
	}
}