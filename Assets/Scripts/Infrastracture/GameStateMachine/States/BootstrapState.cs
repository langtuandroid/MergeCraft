public class BootstrapState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly IUIFactory _uifactory;

    public BootstrapState(GameStateMachine gameStateMachine, IUIFactory uifactory)
    {
        _gameStateMachine = gameStateMachine;
        _uifactory = uifactory;
    }

    public void Enter()
    {
        _uifactory.CreateHud();
        _gameStateMachine.Enter<DataLoadState>();
    }
}
