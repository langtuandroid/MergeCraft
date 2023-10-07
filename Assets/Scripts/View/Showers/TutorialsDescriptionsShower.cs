using TMPro;
using UnityEngine;

public class TutorialsDescriptionsShower : MonoBehaviour
{
    [SerializeField] private TMP_Text _mainTutorialDescriptionText;
    [SerializeField] private TMP_Text _buildTutorialDescriptionText;
    [SerializeField] private TMP_Text _mainTutorialContinueText;
    [SerializeField] private TMP_Text _buildTutorialContinueText;

    public void ShowDescriptions(Translate selectedTranslate)
    {
        _mainTutorialDescriptionText.text = selectedTranslate.MainTutorialDescription;
        _buildTutorialDescriptionText.text = selectedTranslate.BuildTutorialDescription;
        _mainTutorialContinueText.text = selectedTranslate.ContinueWord;
        _buildTutorialContinueText.text = selectedTranslate.ContinueWord;
    }
}
