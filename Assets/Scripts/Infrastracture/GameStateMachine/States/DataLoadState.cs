public class DataLoadState : IState
{
    private readonly UIFactory _uifactory;
    private readonly Wallet _wallet;

    public DataLoadState(UIFactory uifactory, Wallet wallet)
    {
        _uifactory = uifactory;
        _wallet = wallet;
    }

    public void Enter()
    {
        Hud hud = _uifactory.CreatedHud;
        hud.Initialize(_wallet);
    }
}
