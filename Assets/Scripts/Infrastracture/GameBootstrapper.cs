using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private AssetProvider _assetProvider;

    private Game _game;

    private void Awake()
    {
        _game = new Game(_assetProvider);
        _game.ActivateBootstrapState();
        DontDestroyOnLoad(this);
    }
}
