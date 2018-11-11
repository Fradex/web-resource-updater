using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;
using Microsoft.Extensions.Configuration;
using WebPackUpdater.Enums;
using WebPackUpdater.Generators.Interface;

namespace WebPackUpdater.Generators
{
	public class ScriptsGenerator : IScriptsGenerator
	{
	    private IConfiguration Configuration { get; set; }

	    private string ScriptsPath { get; set; } 

	    public ScriptsGenerator(IConfiguration configuration)
	    {
	        Configuration = configuration;
	        ScriptsPath = Configuration.GetSection("AppSettings")["WebPackFolder"];
	        if (string.IsNullOrEmpty(ScriptsPath))
	        {
                throw new ArgumentException("Не задан путь до директории  со скриптами.");
	        }
	    }

	    private List<string> errorsList = new List<string>();
		private List<string> successList = new List<string>();
		private bool isSuccess = true;

		public BuildResult Build()
		{
			using (var powerShellInstance = PowerShell.Create())
			{
                powerShellInstance.AddScript($"cd \"{ScriptsPath}\";npm run build;");
				PSDataCollection<PSObject> outputCollection = new PSDataCollection<PSObject>();


				// the streams (Error, Debug, Progress, etc) are available on the PowerShell instance.
				// we can review them during or after execution.
				// we can also be notified when a new item is written to the stream (like this):
				powerShellInstance.Streams.Error.DataAdded += ErrorDataAdded;
				powerShellInstance.Streams.Debug.DataAdded += OutputCollectionDataAdded;
				powerShellInstance.Streams.Information.DataAdded += OutputCollectionDataAdded;
				powerShellInstance.Streams.Progress.DataAdded += OutputCollectionDataAdded;
				powerShellInstance.Streams.Verbose.DataAdded += OutputCollectionDataAdded;
				powerShellInstance.Streams.Warning.DataAdded += OutputCollectionDataAdded;

				// begin invoke execution on the pipeline
				// use this overload to specify an output stream buffer
				IAsyncResult result = powerShellInstance.BeginInvoke<PSObject, PSObject>(null, outputCollection);

				while (!result.IsCompleted)
				{
					// might want to place a timeout here...
				}

				foreach (var outputItem in outputCollection.ReadAll())
				{
					//TODO: handle/process the output items if required
					successList.Add(outputItem.BaseObject.ToString());
				}

				return new BuildResult() {ErrorList = errorsList,SuccessList = successList, IsSuccess = isSuccess};
			}

			void OutputCollectionDataAdded(object sender, DataAddedEventArgs e)
			{
				using (var dataList = (PSDataCollection<PSObject>)sender)
				{
					foreach (var data in dataList.ReadAll())
					{
						successList.Add(data.ToString());
					}
				}

				// do something when an object is written to the output stream
				Console.WriteLine("Object added to output.");
			}

			void ErrorDataAdded(object sender, DataAddedEventArgs e)
			{
				using (var errorDataList =
					(PSDataCollection<ErrorRecord>)sender)
				{
					foreach (var errorData in errorDataList.ReadAll())
					{
						errorsList.Add(errorData.ToString());
					}

					isSuccess = false;
				}
			}
		}
	}
}