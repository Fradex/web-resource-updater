using WebPackUpdater.Enums;
using WebPackUpdater.Model;

namespace WebPackUpdater.Generators.Interface
{
	public interface IScriptsGenerator
	{
		BuildResult Build(Build build);
	}
}