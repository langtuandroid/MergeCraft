using UnityEngine;
using UnityEngine.UI;

public class TutorialPresenter : MonoBehaviour
{
    [SerializeField] private TranslatesContainer _translatesContainer;
    [Space(10), SerializeField] private Button _openBuildButton;
    [SerializeField] private Button _finishMainTutorialButton;
    [SerializeField] private Button _finishBuildTutorialButton;
    [Space(10), SerializeField] private GameObject _mainTutorialPanel;
    [SerializeField] private GameObject _buildTutorialPanel;
    [Space(10), SerializeField] private TutorialsDescriptionsShower _tutorialsDescriptionShower;

    private bool _buildTutorialFinished;
    private bool _mainTutorialFinished;

    private void TryActivateBuildTutorial() => TryActivateTutorial(_buildTutorialPanel, _buildTutorialFinished);
    private void TryActivateMainTutorial() => TryActivateTutorial(_mainTutorialPanel, _mainTutorialFinished);

    private void OnTranslateSelected() =>
    _tutorialsDescriptionShower.ShowDescriptions(_translatesContainer.SelectedTranslate);

    private void FinishBuildTutorial()
    {
        _buildTutorialPanel.SetActive(false);
        _buildTutorialFinished = true;
    }

    private void FinishMainTutorial()
    {
        _mainTutorialPanel.SetActive(false);
        _mainTutorialFinished = true;
    }

    private void TryActivateTutorial(GameObject tutorialPanel, bool tutorialFinished)
    {
        if (tutorialFinished == false)
            tutorialPanel.SetActive(true);
    }

    private void OnEnable()
    {
        _translatesContainer.TranslateSelected += OnTranslateSelected;

        _openBuildButton.onClick.AddListener(() => TryActivateBuildTutorial());
        _finishMainTutorialButton.onClick.AddListener(() => FinishMainTutorial());
        _finishBuildTutorialButton.onClick.AddListener(() => FinishBuildTutorial());
    }

    private void OnDisable()
    {
        _translatesContainer.TranslateSelected -= OnTranslateSelected;

        _openBuildButton.onClick.RemoveAllListeners();
        _finishMainTutorialButton.onClick.RemoveAllListeners();
        _finishBuildTutorialButton.onClick.RemoveAllListeners();
    }
}
