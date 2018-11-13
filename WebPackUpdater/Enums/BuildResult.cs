using System.Collections.Generic;

namespace WebPackUpdater.Enums
{
	public class BuildResult
	{
		public bool IsSuccess { get; set; }
		public string SuccessText { get; set; }
		public string ErrorText { get; set; }
	}
}