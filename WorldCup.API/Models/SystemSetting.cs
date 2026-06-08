using System;

namespace WorldCup.API.Models
{
    public class SystemSetting
    {
        public int Id { get; set; }
        public bool IsResultPublished { get; set; } = false;
        public DateTime? PollClosingDate { get; set; }
        public DateTime LastModified { get; set; } = DateTime.UtcNow;
    }
}
