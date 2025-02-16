using MeetingApp.Models;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MeetingApp.ConsoleCommands
{
    public class ExportCommand : ICommand
    {
        private readonly MeetingCollection repository;
        public readonly string description = "Экспортировать в файл";

        public ExportCommand(ref MeetingCollection repository)
        {
            this.repository = repository;
        }

        public void Execute()
        {
            Console.Write("Введите дату (dd.MM.yyyy): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine("Неверная дата.");
                return;
            }

            Console.Write("Путь к файлу: ");
            string path = Console.ReadLine();
            ExportToFile(date, path);
            Console.WriteLine("Экспорт завершен.");
        }

        public void ExportToFile(DateTime date, string filePath)
        {
            var now = DateTime.Now;
            var meetings = repository.GetMeetingsByDate(date)
                .Where(meeting=>meeting.EndTime>=now)
                .Select(meeting=> $"{meeting.StartTime:HH:mm} - {meeting.EndTime:HH:mm}: {meeting.Title}")
                .ToList();

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }

            File.AppendAllLines(filePath, meetings);
        }

        public string GetDescriprion()
        {
            return description;
        }
    }
}
