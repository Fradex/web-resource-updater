using WebPackUpdater.Model;

namespace WebPackUpdater.Repositories.Interface
{
    public interface IFileRepository
    {
        void AutoMapFiles(Build build);
    }
}
