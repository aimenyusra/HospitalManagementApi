namespace Hospital.Command
{
    public class CommandInvoker
    {
        public async Task ExecuteCommandAsync(ICommand command)
        {
            await command.ExecuteAsync();
        }
    }
}
