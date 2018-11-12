using System;

namespace WebPackUpdater.Model
{
    public class WebResourceMap
    {
        public Guid Id { get; set; }
        public DateTime ChangedOn { get; set; }
        public Guid CrmWebResourceId { get; set; }
        public string FileSystemPath { get; set; }
        public string CrmFileName { get; set; }
        public string LocalFileMd5Hash { get; set; }
	    public bool IsAutoUpdate { get; set; }
    }
}
