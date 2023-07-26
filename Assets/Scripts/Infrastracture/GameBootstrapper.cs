using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private FactoriesPrefabs _factoriesPrefabs;

    private Game _game;

    private void Awake()
    {
        _game = new Game(_factoriesPrefabs);
        _game.ActivateBootstrapState();
        DontDestroyOnLoad(this);
    }
}
