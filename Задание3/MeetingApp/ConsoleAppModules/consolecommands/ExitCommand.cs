using System;

namespace MeetingApp.ConsoleCommands
{
    public class ExitCommand : ICommand
    {
        private readonly Action exitAction;
        public readonly string description = "Выход";

        public ExitCommand(Action exitAction)
        {
            this.exitAction = exitAction;
        }

        public void Execute()
        {
            exitAction();
        }

        public string GetDescriprion()
        {
            return description;
        }
    }
}
