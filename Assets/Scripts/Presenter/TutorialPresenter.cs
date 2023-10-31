using UnityEngine;
using UnityEngine.UI;
using YG;

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

        YandexGame.savesData.BuildTutorialFinished = _buildTutorialFinished;
        YandexGame.SaveProgress();
    }

    private void FinishMainTutorial()
    {
        _mainTutorialPanel.SetActive(false);
        _mainTutorialFinished = true;

        YandexGame.savesData.MainTutorialFinished = _mainTutorialFinished;
        YandexGame.SaveProgress();
    }

    private void TryActivateTutorial(GameObject tutorialPanel, bool tutorialFinished)
    {
        if (tutorialFinished == false)
            tutorialPanel.SetActive(true);
    }

    private void OnGetDataEvent()
    {
        SavesYG savesData = YandexGame.savesData;

        _buildTutorialFinished = savesData.BuildTutorialFinished;
        _mainTutorialFinished = savesData.MainTutorialFinished;

        TryActivateMainTutorial();
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += OnGetDataEvent;
        _translatesContainer.TranslateSelected += OnTranslateSelected;

        _openBuildButton.onClick.AddListener(() => TryActivateBuildTutorial());
        _finishMainTutorialButton.onClick.AddListener(() => FinishMainTutorial());
        _finishBuildTutorialButton.onClick.AddListener(() => FinishBuildTutorial());
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= OnGetDataEvent;
        _translatesContainer.TranslateSelected -= OnTranslateSelected;

        _openBuildButton.onClick.RemoveAllListeners();
        _finishMainTutorialButton.onClick.RemoveAllListeners();
        _finishBuildTutorialButton.onClick.RemoveAllListeners();
    }
}
