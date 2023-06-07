using UnityEngine;
using UnityEngine.UI;

public class LibraryPresenter : MonoBehaviour
{
    [SerializeField] private LibraryBlockSwitcher _libraryBlockSwitcher;
    [SerializeField] private GameObject _libraryPanel;
    [Space(10), SerializeField] private Button _nextBlockButton;
    [SerializeField] private Button _previouslyBlockButton;
    [SerializeField] private Button _closeLibraryButton;
    [SerializeField] private Button _openLibraryButton;

    private void OnNextBlockButtonClicked() => _libraryBlockSwitcher.SwitchToNextBlock();
    private void OnPreviouslyBlockButtonClicked() => _libraryBlockSwitcher.SwitchToPreviouslyBlock();

    private void OnCloseLibraryButtonClicked()
    {
        _libraryPanel.SetActive(false);
    }

    private void OnOpenLibraryButtonClicked()
    {
        _libraryPanel.SetActive(true);
    }

    private void OnEnable()
    {
        _nextBlockButton.onClick.AddListener(() => OnNextBlockButtonClicked());
        _previouslyBlockButton.onClick.AddListener(() => OnPreviouslyBlockButtonClicked());
        _openLibraryButton.onClick.AddListener(() => OnOpenLibraryButtonClicked());
        _closeLibraryButton.onClick.AddListener(() => OnCloseLibraryButtonClicked());
    }

    private void OnDisable()
    {
        _nextBlockButton.onClick.RemoveAllListeners();
        _previouslyBlockButton.onClick.RemoveAllListeners();
        _openLibraryButton.onClick.RemoveAllListeners();
        _closeLibraryButton.onClick.RemoveAllListeners();
    }
}
