using UnityEngine;
using YG;

public class TutorialActivator : MonoBehaviour
{
    [SerializeField] private GameObject _mainTutorialPanel;
    [SerializeField] private GameObject _buildTutorialPanel;

    private bool _buildTutorialFinished;
    private bool _mainTutorialFinished;

    public void TryActivateBuildTutorial() => TryActivateTutorial(_buildTutorialPanel, _buildTutorialFinished);
    public void TryActivateMainTutorial() => TryActivateTutorial(_mainTutorialPanel, _mainTutorialFinished);

    public void RecoverTutorialStates()
    {
        SavesYG savesData = YandexGame.savesData;

        _buildTutorialFinished = savesData.BuildTutorialFinished;
        _mainTutorialFinished = savesData.MainTutorialFinished;
    }

    public void FinishBuildTutorial()
    {
        _buildTutorialPanel.SetActive(false);
        _buildTutorialFinished = true;

        YandexGame.savesData.BuildTutorialFinished = _buildTutorialFinished;
        YandexGame.SaveProgress();
    }

    public void FinishMainTutorial()
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
}
