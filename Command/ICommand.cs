namespace Hospital.Command
{
    public interface ICommand
    {
        Task ExecuteAsync();
    }
}
