namespace JF.OrdemServico.Domain.Interfaces.Messages;

public interface IMessageBus
{
    Task PublishAsync(string queue, object message);
}
