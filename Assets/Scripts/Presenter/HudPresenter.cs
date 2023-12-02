using UnityEngine;
using YG;

public class HudPresenter : MonoBehaviour
{
    [SerializeField] private Hud _hud;
    [SerializeField] private Shop _shop;
    [SerializeField] private BlockCreator _blockCreator;
    [SerializeField] private MoneyGenerator _moneyGenerator;
    [SerializeField] private BuildingCreator _buildingCreator;
    [SerializeField] private TutorialActivator _tutorialActivator;

    private void OnHudInitialized() => TryLaunchGameLoop();

    private void TryLaunchGameLoop()
    {
        YandexGame.LoadProgress();

        _tutorialActivator.RecoverTutorialStates();
        _tutorialActivator.TryActivateMainTutorial();

        _blockCreator.TryRecoverBlocks();
        _moneyGenerator.Activate();

        _shop.TryRecoverUpgrades();
        _buildingCreator.TryRecoverBuilding();
    }

    private void OnEnable() => _hud.HudInitialized += OnHudInitialized;
    private void OnDisable() => _hud.HudInitialized -= OnHudInitialized;
}
