using UnityEngine;
using UnityEngine.UI;

public class TutorialPresenter : MonoBehaviour
{
    [SerializeField] private TutorialActivator _tutorialActivator;
    [SerializeField] private TranslatesContainer _translatesContainer;
    [Space(10), SerializeField] private Button _openBuildButton;
    [SerializeField] private Button _finishMainTutorialButton;
    [SerializeField] private Button _finishBuildTutorialButton;
    [Space(10), SerializeField] private TutorialsDescriptionsShower _tutorialsDescriptionShower;

    private void OnTranslateSelected() => 
        _tutorialsDescriptionShower.ShowDescriptions(_translatesContainer.SelectedTranslate);

    private void OnEnable()
    {
        _translatesContainer.TranslateSelected += OnTranslateSelected;

        _openBuildButton.onClick.AddListener(() => _tutorialActivator.TryActivateBuildTutorial());
        _finishMainTutorialButton.onClick.AddListener(() => _tutorialActivator.FinishMainTutorial());
        _finishBuildTutorialButton.onClick.AddListener(() => _tutorialActivator.FinishBuildTutorial());
    }

    private void OnDisable()
    {
        _translatesContainer.TranslateSelected -= OnTranslateSelected;

        _openBuildButton.onClick.RemoveAllListeners();
        _finishMainTutorialButton.onClick.RemoveAllListeners();
        _finishBuildTutorialButton.onClick.RemoveAllListeners();
    }
}
