using WebPackUpdater.Model;

namespace WebPackUpdater.Repositories.Interface
{
    public interface IFileRepository
    {
        /// <summary>
        /// Автоматически сопоставить файлы из внешней системы и локальные файлы.
        /// </summary>
        /// <param name="build">Билд</param>
        /// <param name="isStartMapping">Билд при запуске приложения, не отслеживает изменения файлов.</param>
        void AutoMapFiles(Build build, bool isStartMapping = false);
    }
}
