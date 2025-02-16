using MeetingApp.ConsoleCommands;
using MeetingApp.Services;
using System;
using System.Collections.Generic;

namespace MeetingApp.ConsoleAppModules
{
    public class ConsoleApp
    {
        private List<IService> services = new List<IService>();
        private int commandsId = 0;
        private Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

        public void AddService(IService service) 
        { 
            services.Add(service);
        }

        public void AddCommand(ICommand command)
        {
            commandsId++;
            commands.Add(commandsId.ToString(), command);
        }

        public void Start()
        {
            services.ForEach(service => service.Start());

            while (true)
            {
                foreach (var command in commands)
                {
                    Console.WriteLine($"{command.Key}. {command.Value.GetDescriprion()}");
                }
                Console.Write("Выберите действие: ");

                var input = Console.ReadLine();
                if (commands.TryGetValue(input, out var commandOut))
                {
                    commandOut.Execute();
                }

                else
                {
                    Console.WriteLine("Такой команды не существует.");
                }

                Console.WriteLine();
            }
        }
    }
}
