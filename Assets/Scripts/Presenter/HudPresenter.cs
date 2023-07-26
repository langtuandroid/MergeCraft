using UnityEngine;

public class HudPresenter : MonoBehaviour
{
    [SerializeField] private Hud _hud;
    [SerializeField] private BlockCreator _blockCreator;
    [SerializeField] private MoneyGenerator _moneyGenerator;

    private void OnHudInitialized()
    {
        _blockCreator.Activate();
        _moneyGenerator.Activate();
    }

    private void OnEnable() => _hud.HudInitialized += OnHudInitialized;
    private void OnDisable() => _hud.HudInitialized -= OnHudInitialized;
}
