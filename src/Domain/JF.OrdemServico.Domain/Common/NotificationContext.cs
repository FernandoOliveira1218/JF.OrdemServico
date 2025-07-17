namespace JF.OrdemServico.Domain.Common;

public class NotificationContext
{
    private readonly List<Notification> _notifications = [];

    public IReadOnlyCollection<Notification> Notifications => _notifications;

    public bool HasNotifications => _notifications.Any();

    public void AddNotification(string campo, string mensagem)
    {
        _notifications.Add(new Notification(campo, mensagem));
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }
}
