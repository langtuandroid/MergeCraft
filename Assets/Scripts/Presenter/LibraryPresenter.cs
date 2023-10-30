using UnityEngine;
using UnityEngine.UI;
using YG;

public class LibraryPresenter : MonoBehaviour
{
    [SerializeField] private TranslatesContainer _translateContainer;
    [SerializeField] private LibraryBlockSwitcher _libraryBlockSwitcher;
    [SerializeField] private GameObject _libraryPanel;
    [SerializeField] private GameObject _libraryTouchBlocker;
    [Space(10), SerializeField] private Button _nextBlockButton;
    [SerializeField] private Button _previouslyBlockButton;
    [SerializeField] private Button _closeLibraryButton;
    [SerializeField] private Button _openLibraryButton;

    private PanelAnimator _panelAnimator = new PanelAnimator();

    private void OnNextBlockButtonClicked() => _libraryBlockSwitcher.SwitchToNextBlock();
    private void OnPreviouslyBlockButtonClicked() => _libraryBlockSwitcher.SwitchToPreviouslyBlock();
    private void OnTranslateSelected() => _openLibraryButton.interactable = true;

    private void OnEnable()
    {
        _openLibraryButton.interactable = false;
        _translateContainer.TranslateSelected += OnTranslateSelected;

        _nextBlockButton.onClick.AddListener(() => OnNextBlockButtonClicked());
        _previouslyBlockButton.onClick.AddListener(() => OnPreviouslyBlockButtonClicked());

        _openLibraryButton.onClick.AddListener(() => _libraryTouchBlocker.gameObject.SetActive(true));
        _openLibraryButton.onClick.AddListener(() => _libraryBlockSwitcher.SwitchToFirstBlock());
        _openLibraryButton.onClick.AddListener(() => _panelAnimator.LaunchIncreaseAnimation(_libraryPanel));
        _closeLibraryButton.onClick.AddListener(() => _libraryTouchBlocker.gameObject.SetActive(false));

        _openLibraryButton.onClick.AddListener(() => YandexGame.FullscreenShow());
        _closeLibraryButton.onClick.AddListener(() => YandexGame.FullscreenShow());
    }

    private void OnDisable()
    {
        _translateContainer.TranslateSelected -= OnTranslateSelected;

        _nextBlockButton.onClick.RemoveAllListeners();
        _previouslyBlockButton.onClick.RemoveAllListeners();
        _openLibraryButton.onClick.RemoveAllListeners();
        _closeLibraryButton.onClick.RemoveAllListeners();
    }
}
