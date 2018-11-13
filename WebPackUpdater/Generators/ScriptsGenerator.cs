using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebPackUpdater.Enums;
using WebPackUpdater.Generators.Interface;
using WebPackUpdater.Model;
using WebPackUpdater.Repositories.Interface;

namespace WebPackUpdater.Generators
{
	public class ScriptsGenerator : IScriptsGenerator
	{
		private IConfiguration Configuration { get; set; }
		private IBuildRepository BuildRepository { get; set; }

		private string ScriptsPath { get; set; }

		public ScriptsGenerator(IConfiguration configuration, IBuildRepository buildRepository)
		{
			Configuration = configuration;
			BuildRepository = buildRepository;

			ScriptsPath = Configuration.GetSection("AppSettings")["WebPackFolder"];
			if (string.IsNullOrEmpty(ScriptsPath))
			{
				throw new ArgumentException("Не задан путь до директории  со скриптами.");
			}
		}

		private StringBuilder errorsList = new StringBuilder();
		private StringBuilder successList = new StringBuilder();
		private bool isSuccess = true;

		public BuildResult Build(Build build)
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
					successList.Append(outputItem.BaseObject);
				}

				//update build log
				build.BuildLog = successList.ToString();
				BuildRepository.Update(build);

				return new BuildResult
				{
					ErrorText = errorsList.ToString(),
					SuccessText = successList.ToString(),
					IsSuccess = isSuccess
				};
			}

			void OutputCollectionDataAdded(object sender, DataAddedEventArgs e)
			{
				using (var dataList = (PSDataCollection<PSObject>) sender)
				{
					foreach (var data in dataList.ReadAll())
					{
						successList.Append(data);
					}
				}

				// do something when an object is written to the output stream
				Console.WriteLine("Object added to output.");
			}

			void ErrorDataAdded(object sender, DataAddedEventArgs e)
			{
				using (var errorDataList =
					(PSDataCollection<ErrorRecord>) sender)
				{
					foreach (var errorData in errorDataList.ReadAll())
					{
						errorsList.Append(errorData);
					}

					isSuccess = false;
				}
			}
		}
	}
}