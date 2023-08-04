public class Game
{
    private readonly GameStateMachine _gameStateMachine;

    public void ActivateBootstrapState() => _gameStateMachine.Enter<BootstrapState>();

    public Game(AssetProvider assetProvider)
    {
        UIFactory UIfactory = new UIFactory(assetProvider.HudPrefab);
        Wallet wallet = new Wallet();

        _gameStateMachine = new GameStateMachine(UIfactory, wallet);
    }
}
