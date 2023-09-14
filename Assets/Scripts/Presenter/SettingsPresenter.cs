using UnityEngine;
using UnityEngine.UI;

public class SettingsPresenter : MonoBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private GameObject _settingsPanel;

    private void OnEnable()
    {
        _openButton.onClick.AddListener(() => _settingsPanel.gameObject.SetActive(true));
        _closeButton.onClick.AddListener(() => _settingsPanel.gameObject.SetActive(false));
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
    }
}
