using MeetingApp.Models;
using System;
using System.Linq;
using System.Timers;

namespace MeetingApp.Services
{
    public class NotificationService:IService
    {
        private readonly MeetingCollection repository;
        private Timer timer;

        public NotificationService(ref MeetingCollection repository, int timer)
        {
            this.repository = repository;
            this.timer = new Timer(timer);
            this.timer.Elapsed += CheckNotifications;
        }

        public void Start()
        {
            timer.Start();
        }

        private void CheckNotifications(object sender, ElapsedEventArgs e)
        {
            var now = DateTime.Now;
            var meetings = repository.GetAllMeetings()
                .Where(m => m.NotificationTime.HasValue &&
                            m.NotificationTime <= now &&
                            !m.Notified).ToList();

            foreach (var meeting in meetings)
            {
                Console.WriteLine($"\n!!! Напоминание !!! {meeting.Title} в {meeting.StartTime:t}");
                meeting.Notified = true;
                repository.Update(meeting);
            }
        }
    }
}
