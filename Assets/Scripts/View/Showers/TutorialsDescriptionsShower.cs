using TMPro;
using UnityEngine;

public class TutorialsDescriptionsShower : MonoBehaviour
{
    [SerializeField] private TMP_Text _mainTutorialDescriptionText;
    [SerializeField] private TMP_Text _buildTutorialDescriptionText;
    [SerializeField] private TMP_Text _mainTutorialContinueText;
    [SerializeField] private TMP_Text _buildTutorialContinueText;
    [SerializeField] private TMP_Text _buildLimitTutorialText;

    public void ShowDescriptions(Translate selectedTranslate)
    {
        _buildLimitTutorialText.text = selectedTranslate.BuildLimitDescription;
        _mainTutorialDescriptionText.text = selectedTranslate.MainTutorialDescription;
        _buildTutorialDescriptionText.text = selectedTranslate.BuildTutorialDescription;
        _buildTutorialContinueText.text = selectedTranslate.ContinueWord;
        _mainTutorialContinueText.text = selectedTranslate.ContinueWord;
    }
}
