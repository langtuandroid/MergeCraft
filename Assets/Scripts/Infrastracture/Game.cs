public class Game
{
    private readonly GameStateMachine _gameStateMachine;

    public void ActivateBootstrapState() => _gameStateMachine.Enter<BootstrapState>();

    public Game(FactoriesPrefabs factoriesPrefabs)
    {
        UIFactory UIfactory = new UIFactory(factoriesPrefabs.HudPrefab);
        Wallet wallet = new Wallet();

        _gameStateMachine = new GameStateMachine(UIfactory, wallet);
    }
}
