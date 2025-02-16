namespace MeetingApp.ConsoleCommands
{
    public interface ICommand
    {
        string GetDescriprion();
        void Execute();
    }
}
