using MeetingApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MeetingApp.ConsoleCommands
{
    public class ViewMeetingsCommand : ICommand
    {
        private readonly MeetingCollection repository;
        public readonly string description = "Просмотреть встречи";

        public ViewMeetingsCommand(ref MeetingCollection repository)
        {
            this.repository = repository;
        }

        public void Execute()
        {
            Console.Write("Введите дату (dd.MM.yyyy): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, DateTimeStyles.None, out DateTime date))
            {
                var now = DateTime.Now;
                var meetings = repository.GetMeetingsByDate(date).Where(meeting=> meeting.EndTime>=now);
                foreach (var m in meetings)
                {
                    Console.WriteLine($"{m.Id}: {m.Title} ({m.StartTime:HH:mm} - {m.EndTime:HH:mm})");
                }
            }
            else
            {
                Console.WriteLine("Неверный формат даты.");
            }
        }

        public string GetDescriprion()
        {
            return description;
        }
    }
}
