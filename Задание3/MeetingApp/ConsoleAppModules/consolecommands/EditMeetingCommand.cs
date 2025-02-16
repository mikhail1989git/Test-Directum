using MeetingApp.Models;
using System;
using System.Globalization;

namespace MeetingApp.ConsoleCommands
{
    public class EditMeetingCommand : ICommand
    {
        private readonly MeetingCollection repository;
        public readonly string description = "Изменить встречу";

        public EditMeetingCommand(ref MeetingCollection repository)
        {
            this.repository = repository;
        }

        public void Execute()
        {
            Console.Write("Введите ID встречи: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID.");
                return;
            }

            var meeting = repository.GetById(id);
            if (meeting == null)
            {
                Console.WriteLine("Встреча не найдена.");
                return;
            }

            var newMeeting = new Meeting();
            newMeeting.Id = meeting.Id;
            newMeeting.StartTime = ReadDateTime("Дата и время начала (dd.MM.yyyy HH:mm): ");
            newMeeting.EndTime = ReadDateTime("Дата и время окончания (dd.MM.yyyy HH:mm): ");
            Console.Write("Название: ");
            newMeeting.Title = Console.ReadLine();
            newMeeting.NotificationTime = ReadNullableDateTime("Время напоминания (dd.MM.yyyy HH:mm): ");
            newMeeting.Notified = meeting.NotificationTime < DateTime.Now;

            if (repository.IsDuplicated(newMeeting))
            {
                Console.WriteLine("Пересечение с другой встречей.");
                return;
            }

            repository.Update(newMeeting);
            Console.WriteLine("Встреча обновлена.");
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
