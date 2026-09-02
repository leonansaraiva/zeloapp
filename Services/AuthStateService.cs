namespace ZeloApp.Services;

public class AuthStateService
{
    public string UsuarioNome { get; private set; } = string.Empty;
    public string TipoUsuario { get; private set; } = string.Empty; // "SuperAdmin", "Diretora", "Pai"
    public int? EscolaId { get; private set; }
    public bool EstaAutenticado => !string.IsNullOrEmpty(UsuarioNome);

    public event Action? OnChange;

    public void FazerLoginAdminMaster(string nome)
    {
        UsuarioNome = nome;
        TipoUsuario = "SuperAdmin";
        EscolaId = null;
        NotifyStateChanged();
    }

    public void FazerLoginDiretora(int escolaId, string nomeEscola, string nomeGestor)
    {
        UsuarioNome = nomeGestor;
        TipoUsuario = "Diretora";
        EscolaId = escolaId;
        NotifyStateChanged();
    }

    public void FazerLoginPai(int responsavelId, string nomePai)
    {
        UsuarioNome = nomePai;
        TipoUsuario = "Pai";
        EscolaId = null;
        NotifyStateChanged();
    }

    public void FazerLogout()
    {
        UsuarioNome = string.Empty;
        TipoUsuario = string.Empty;
        EscolaId = null;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}