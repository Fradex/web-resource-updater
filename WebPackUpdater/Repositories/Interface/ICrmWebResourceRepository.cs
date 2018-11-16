using System;
using Microsoft.Xrm.Sdk;
using WebPackUpdater.Enums;

namespace WebPackUpdater.Repositories.Interface
{
    /// <summary>
    /// Webresource repo - work only with CRM connection
    /// </summary>
    public interface ICrmWebResourceRepository
    {
        /// <summary>
        /// Получить веб-ресурс по наименованию
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <returns>Сущность CRM</returns>
        Entity RetrieveWebresource(string name);

        /// <summary>
        /// Создать запись
        /// </summary>
        /// <param name="name">Наименование веб-ресурса</param>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="type">Тип веб-ресурса</param>
        void Create(string name, string filePath, WebResourceType type = WebResourceType.Auto);

        /// <summary>
        /// Обновить запись
        /// </summary>
        /// <param name="id">Идентификатор записи в CRM</param>
        /// <param name="name">Наименование веб-ресурса</param>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="type">Тип веб-ресурса</param>
        void Update(Guid id, string name, string filePath, WebResourceType type = WebResourceType.Auto);

        /// <summary>
        /// Обновить или создать запись
        /// </summary>
        /// <param name="name">Наименование веб-ресурса</param>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="type">Тип веб-ресурса</param>
        Entity CreateOrUpdate(string name, string filePath, WebResourceType type = WebResourceType.Auto);

        /// <summary>
        /// Опубликовать веб-ресурсы
        /// </summary>
        /// <param name="webresourceIds">Идентификаторы веб-ресурсов</param>
        void Publish(Guid[] webresourceIds);
    }
}
