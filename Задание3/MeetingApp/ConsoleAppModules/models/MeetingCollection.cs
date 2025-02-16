using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingApp.Models
{
    public class MeetingCollection
    {
        private List<Meeting> meetings = new List<Meeting>();
        private int nextId = 1;

        public void Add(Meeting meeting)
        {
            meeting.Id = nextId++;
            meetings.Add(meeting);
        }

        public void Update(Meeting meeting)
        {
            var index = meetings.FindIndex(m => m.Id == meeting.Id);
            if (index != -1)
            {
                meetings[index] = meeting;
            }
        }

        public void Delete(int id)
        {
            meetings.RemoveAll(m => m.Id == id);
        }

        public IEnumerable<Meeting> GetMeetingsByDate(DateTime date)
        {
            return meetings.Where(m => m.StartTime.Date == date.Date)
                .OrderBy(m => m.StartTime).ToList();
        }

        public Meeting GetById(int id)
        {
            return meetings.FirstOrDefault(m => m.Id == id);
        }

        public bool IsDuplicated(Meeting meeting)
        {
            return meetings.Any(m => m.Id != meeting.Id &&
                meeting.StartTime < m.EndTime && 
                meeting.EndTime > m.StartTime);
        }

        public IEnumerable<Meeting> GetAllMeetings()
        {
            return meetings;
        }
    }
}
