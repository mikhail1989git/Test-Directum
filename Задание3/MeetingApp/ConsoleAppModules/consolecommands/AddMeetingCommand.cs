using MeetingApp.Models;
using System;
using System.Globalization;

namespace MeetingApp.ConsoleCommands
{
    public class AddMeetingCommand : ICommand
    {
        private readonly MeetingCollection repository;
        public readonly string description = "Добавить встречу";

        public AddMeetingCommand(ref MeetingCollection repository)
        {
            this.repository = repository;
        }

        public void Execute()
        {
            var meeting = new Meeting();
            meeting.StartTime = ReadDateTime("Дата и время начала (dd.MM.yyyy HH:mm): ");
            meeting.EndTime = ReadDateTime("Дата и время окончания (dd.MM.yyyy HH:mm): ");
            Console.Write("Название: ");
            meeting.Title = Console.ReadLine();
            meeting.NotificationTime = ReadNullableDateTime("Время напоминания (dd.MM.yyyy HH:mm): ");

            if (meeting.StartTime < DateTime.Now)
            {
                Console.WriteLine("Встреча должна быть в будущем.");
                return;
            }

            if (meeting.EndTime <= meeting.StartTime)
            {
                Console.WriteLine("Окончание должно быть позже начала.");
                return;
            }

            if (repository.IsDuplicated(meeting))
            {
                Console.WriteLine("Пересечение с другой встречей.");
                return;
            }

            repository.Add(meeting);
            Console.WriteLine("Встреча добавлена.");
        }

        private DateTime ReadDateTime(string prompt)
        {
            DateTime date;
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy HH:mm", null, DateTimeStyles.None, out date))
                    return date;
                Console.WriteLine("Неверный формат.");
            }
        }

        private DateTime? ReadNullableDateTime(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                return null;

            DateTime date;
            if (DateTime.TryParseExact(input, "dd.MM.yyyy HH:mm", null, DateTimeStyles.None, out date))
                return date;
            Console.WriteLine("Неверный формат, напоминание не установлено.");
            return null;
        }

        public string GetDescriprion()
        {
            return description;
        }
    }
}
