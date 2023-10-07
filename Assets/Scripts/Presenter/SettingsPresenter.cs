using UnityEngine;
using UnityEngine.UI;

public class SettingsPresenter : MonoBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _settingsTouchBlockator;
    [SerializeField] private TranslatesContainer _translatesContainer;
    [SerializeField] private SettingsDescriptionsShower _settingsDescriptionsShower;

    private PanelAnimator _panelAnimator = new PanelAnimator();

    private void OnTranslateSelected()
    {
        _settingsDescriptionsShower.Show(_translatesContainer.SelectedTranslate);
        _openButton.interactable = true;
    }

    private void OnEnable()
    {
        _openButton.interactable = false;
        _translatesContainer.TranslateSelected += OnTranslateSelected;

        _closeButton.onClick.AddListener(() => _settingsTouchBlockator.gameObject.SetActive(false));
        _openButton.onClick.AddListener(() => _settingsTouchBlockator.gameObject.SetActive(true));
        _openButton.onClick.AddListener(() => _panelAnimator.LaunchIncreaseAnimation(_settingsPanel));
    }

    private void OnDisable()
    {
        _translatesContainer.TranslateSelected -= OnTranslateSelected;

        _openButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
    }
}
