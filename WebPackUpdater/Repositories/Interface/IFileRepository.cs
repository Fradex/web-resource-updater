using WebPackUpdater.Model;

namespace WebPackUpdater.Repositories.Interface
{
    public interface IFileRepository
    {
	    /// <summary>
		/// Автоматически сопоставить файлы из внешней системы и локальные файлы.
		/// </summary>
		/// <param name="build"></param>
        void AutoMapFiles(Build build);
    }
}
