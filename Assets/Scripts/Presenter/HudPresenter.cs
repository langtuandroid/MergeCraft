using UnityEngine;

public class HudPresenter : MonoBehaviour
{
    [SerializeField] private Hud _hud;
    [SerializeField] private BlockCreator _blockCreator;
    [SerializeField] private MoneyGenerator _moneyGenerator;
    [SerializeField] private BuildingCreator _buildingCreator;

    private void OnHudInitialized()
    {
        _blockCreator.TryCreateAllBlocks();
        _blockCreator.Activate();
        _moneyGenerator.Activate();
        _buildingCreator.Activate();
    }

    private void OnEnable() => _hud.HudInitialized += OnHudInitialized;
    private void OnDisable() => _hud.HudInitialized -= OnHudInitialized;
}
