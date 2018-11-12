using System;

namespace WebPackUpdater.Model
{
    public class ChangedWebResource
    {
        public Guid Id { get; set; }

        public DateTime ChangedDate { get; set; }

        public WebResourceMap WebResourceMap { get; set; }
    }
}
