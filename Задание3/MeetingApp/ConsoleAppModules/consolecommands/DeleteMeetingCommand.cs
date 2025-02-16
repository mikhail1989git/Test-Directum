using MeetingApp.Models;
using System;

namespace MeetingApp.ConsoleCommands
{
    public class DeleteMeetingCommand : ICommand
    {
        private readonly MeetingCollection repository;
        public readonly string description = "Удалить встречу";

        public DeleteMeetingCommand(ref MeetingCollection repository)
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

            repository.Delete(id);
            Console.WriteLine("Встреча удалена.");
        }

        public string GetDescriprion()
        {
            return description;
        }
    }
}
