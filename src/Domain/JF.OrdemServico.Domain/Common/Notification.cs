namespace JF.OrdemServico.Domain.Common;

public class Notification
{
    public string Campo { get; }
    public string Mensagem { get; }

    public Notification(string campo, string mensagem)
    {
        Campo = campo;
        Mensagem = mensagem;
    }
}