using System;

namespace MeetingApp.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Title { get; set; }
        public DateTime? NotificationTime { get; set; }
        public bool Notified { get; set; }
    }
}
