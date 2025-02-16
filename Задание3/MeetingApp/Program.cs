using MeetingApp.ConsoleAppModules;
using MeetingApp.ConsoleCommands;
using MeetingApp.Models;
using MeetingApp.Services;
using System;

namespace MeetingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var repository = new MeetingCollection();
            var app = new ConsoleApp();

            app.AddService(new NotificationService(ref repository,5000));

            app.AddCommand(new ViewMeetingsCommand(ref repository));
            app.AddCommand(new AddMeetingCommand(ref repository));
            app.AddCommand(new EditMeetingCommand(ref repository));
            app.AddCommand(new DeleteMeetingCommand(ref repository));
            app.AddCommand(new ExportCommand(ref repository));
            app.AddCommand(new ExitCommand(() => Environment.Exit(0)));

            app.Start();
        }
    }
}
